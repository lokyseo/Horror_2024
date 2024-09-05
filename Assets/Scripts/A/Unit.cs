using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Unit : MonoBehaviour
{

    const float minPathUpdateTime = .2f;
    const float pathUpdateMoveThreshold = .5f;


    public GameObject totalWayPoint;
    public int currentWaypoint;
    Transform target;
    public float speed;
    public float turnSpeed = 3;
    public float turnDst = 5;
    public float stoppingDst = 10;

    public bool isChasing;
    float chaseCoolTime;
    

    Path path;


    public float rayDistance;  
    public LayerMask layerMask; 
    public Transform rayOrigin; 

    
    private void Awake()
    {
        chaseCoolTime = 3.0f;
        speed = 10.0f;
        rayDistance = 20.0f;
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
        Vector3 rayDirection = transform.forward;
        Debug.DrawRay(rayOrigin.position, rayDirection * rayDistance, Color.red);
        RaycastHit hit;

        if (!isChasing)
        {
            speed = 10.0f;
            transform.GetComponent<MeshRenderer>().material.color = Color.black;

            if (Physics.Raycast(rayOrigin.position, rayDirection, out hit, rayDistance, layerMask))
            {
                target = hit.transform;
                isChasing = true;
                chaseCoolTime = 4.0f;
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
            Debug.Log("추적중");
            speed = 20.0f;
            transform.GetComponent<MeshRenderer>().material.color = Color.red;

            chaseCoolTime -= Time.deltaTime;

            if(chaseCoolTime < 0)
            {
                target = totalWayPoint.transform.GetChild(currentWaypoint).transform;
                isChasing = false;
                Debug.Log("추적 실패");
            }


            if (Physics.Raycast(rayOrigin.position, rayDirection, out hit, rayDistance, layerMask))
            {
                target = hit.transform;
                chaseCoolTime = 4.0f;
            }

            if (Vector3.Distance(transform.position, target.position) < 6.0f)
            {
                target = totalWayPoint.transform.GetChild(currentWaypoint).transform;
                isChasing = false;
                Debug.Log("잡았고~");
            }
        }
        

       
  
    }

    void OnRaycastHit(RaycastHit hitInfo)
    {
        Debug.Log("Raycast hit: " + hitInfo.collider.name);
        // 추가 로직을 여기에 구현 (예: 충돌한 오브젝트에 특정 동작 수행 등)
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