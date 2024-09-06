using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Unit : MonoBehaviour
{
    const float minPathUpdateTime = .2f;
    const float pathUpdateMoveThreshold = .5f;

    Path path;

    public GameObject totalWayPoint;
    public int currentWaypoint;
    Transform target;
    public float speed;
    public float turnSpeed = 3;
    public float turnDst = 5;
    public float stoppingDst = 10;

    public bool isChasing;
    float chaseCoolTime;
    

    float rayDistance;
    public LayerMask totalLayerMask;
    public LayerMask playerMask;
    public LayerMask blockMask;

    public Transform rayOrigin;
    public GameObject trap_Object;
    
    private void Awake()
    {
        chaseCoolTime = 3.0f;
        speed = 10.0f;
        rayDistance = 30.0f;
        isChasing = false;
        currentWaypoint = 0;
        target = totalWayPoint.transform.GetChild(currentWaypoint).transform;
    }
    void Start()
    {
        
        StartCoroutine(UpdatePath());
    }

    void Update()
    {
        if(trap_Object.GetComponent<TrapCollisionEvent>().isCollisionPlayer && !isChasing)
        {
            Debug.Log("트랩 활성화");
            target = trap_Object.transform;
            trap_Object.GetComponent<TrapCollisionEvent>().isCollisionPlayer = false;
        }

        Vector3 rayDirection = transform.forward;
        Debug.DrawRay(rayOrigin.position, rayDirection * rayDistance, Color.red);
        RaycastHit hit;

        if (!isChasing)
        {
            speed = 10.0f;
            transform.GetComponent<MeshRenderer>().material.color = Color.black;

            if (Physics.Raycast(rayOrigin.position, rayDirection, out hit, rayDistance, totalLayerMask))
            {
                int hitLayer = hit.collider.gameObject.layer;

                if (((1 << hitLayer) & playerMask) != 0)
                {
                    target = hit.transform;
                    isChasing = true;
                    chaseCoolTime = 4.0f;
                }
                else if (((1 << hitLayer) & blockMask) != 0)
                {

                }

            }

            if (Vector3.Distance(transform.position, target.position) < 5.0f)
            {
                if (currentWaypoint < 3)
                {
                    currentWaypoint++;
                }
                else
                {
                    currentWaypoint = 0;
                }
                target = totalWayPoint.transform.GetChild(currentWaypoint).transform;
            }
        }
        else
        {
            speed = 15.0f;
            transform.GetComponent<MeshRenderer>().material.color = Color.red;
            chaseCoolTime -= Time.deltaTime;

            if(chaseCoolTime < 0)
            {
                target = totalWayPoint.transform.GetChild(currentWaypoint).transform;
                isChasing = false;
                Debug.Log("추적 실패");
            }

            if (Physics.Raycast(rayOrigin.position, rayDirection, out hit, rayDistance, totalLayerMask))
            {
                int hitLayer = hit.collider.gameObject.layer;

                if (((1 << hitLayer) & playerMask) != 0)
                {
                    target = hit.transform;
                    chaseCoolTime = 4.0f;
                }
                else if (((1 << hitLayer) & blockMask) != 0)
                {

                }

            }

            if (Vector3.Distance(transform.position, target.position) < 5.0f)
            {
                target = totalWayPoint.transform.GetChild(currentWaypoint).transform;
                isChasing = false;
                Debug.Log("잡았고~");
            }
        }
    }

    public void TrapActivated()
    {

    }


    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = new Path(waypoints, transform.position, turnDst, stoppingDst);

            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }
   
    IEnumerator UpdatePath()
    {

        if (Time.timeSinceLevelLoad < .3f)
        {
            yield return new WaitForSeconds(.3f);
        }
        PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));

        float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector3 targetPosOld = target.position;

        while (true)
        {
            yield return new WaitForSeconds(minPathUpdateTime);
            //print(((target.position - targetPosOld).sqrMagnitude) + "    " + sqrMoveThreshold);
            if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
            {
                PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
                targetPosOld = target.position;
            }
        }
    }

    IEnumerator FollowPath()
    {

        bool followingPath = true;
        int pathIndex = 0;
        transform.LookAt(path.lookPoints[0]);

        float speedPercent = 1;

        while (followingPath)
        {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
            while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {
                if (pathIndex == path.finishLineIndex)
                {
                    followingPath = false;
                    break;
                }
                else
                {
                    pathIndex++;
                }
            }

            if (followingPath)
            {

                if (pathIndex >= path.slowDownIndex && stoppingDst > 0)
                {
                    speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D) / stoppingDst);
                    if (speedPercent < 0.01f)
                    {
                        followingPath = false;
                    }
                }

                Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                transform.Translate(Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);
            }

            yield return null;

        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            path.DrawWithGizmos();
        }
    }
}