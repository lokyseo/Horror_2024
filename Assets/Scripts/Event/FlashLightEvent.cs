using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightEvent : MonoBehaviour
{
    public Light targetLight; // 감지할 광원
    Renderer enemy_Renderer;
    public float targetTime;
    float exitTime;

    Unit unit_Script;

    void Start()
    {
        unit_Script = transform.GetComponent<Unit>();

        targetTime = 3.0f;
        exitTime = 5.0f;
    }

    void Update()
    {
        if (IsObjectLit())
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
        enemy_Renderer = GetComponentInChildren<Renderer>();

        if(enemy_Renderer.isVisible && targetLight.transform.parent.gameObject.activeSelf)
        {
            float distanceToLight = Vector3.Distance(targetLight.transform.position, transform.position);

            if(distanceToLight <= targetLight.range)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    void OnLightEnter()
    {
        Debug.Log("비춰지는 중");

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

