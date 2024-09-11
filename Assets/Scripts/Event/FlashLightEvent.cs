using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightEvent : MonoBehaviour
{
    public Light targetLight; // 감지할 광원
    private Renderer objectRenderer;
    public float targetTime;
    float exitTime;

    Unit unit_Script;

    void Start()
    {
        unit_Script = transform.GetComponent<Unit>();

        targetTime = 3.0f;
        objectRenderer = GetComponent<Renderer>();
        exitTime = 10.0f;
    }

    void Update()
    {
        if (IsObjectLit() && targetLight.transform.parent.gameObject.activeSelf)
        {
            OnLightEnter();
        }
        else
        {
            OnLightExit();
        }


    }
    public bool IsObjectLit()
    {
        Vector3 lightDirection = (transform.position - targetLight.transform.position).normalized;
        float dotProduct = Vector3.Dot(lightDirection, targetLight.transform.forward);

        return dotProduct < 0.9f;

        //if (dotProduct < 0.9f)
        //{
        //    RaycastHit hit;
        //    if (Physics.Raycast(targetLight.transform.position, targetLight.transform.forward, out hit))
        //    {
        //
        //        if (hit.collider.tag != "Map" && hit.collider != null)
        //        {
        //            return true;
        //        }
        //
        //    }
        //    return false;
        //}
        //else
        //{
        //    return false;
        //}


    }

    void OnLightEnter()
    {
        targetTime -= Time.deltaTime;

        if (targetTime < 0 && !unit_Script.isChasing)
        {
            Debug.Log("널 죽이러 가겠다");

            exitTime = 3.0f;
            unit_Script.isChasing = true;
            unit_Script.chaseCoolTime = 4.0f;
            unit_Script.target = GameObject.FindWithTag("Player").transform;
        }

    }

    void OnLightExit()
    {
        exitTime -= Time.deltaTime;
        if (exitTime < 0)
        {
            targetTime = 3.0f;
        }

    }
}

