using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightEvent : MonoBehaviour
{
    public Light targetLight; // °¨ÁöÇÒ ±¤¿ø
    private Renderer objectRenderer;
    float targetTime;
    void Start()
    {
        targetTime = 3.0f;
        objectRenderer = GetComponent<Renderer>();

    }

    void Update()
    {
        if (!IsObjectLit() && targetLight.transform.parent.gameObject.activeSelf)
        {
            OnLightEnter();
        }
        else
        {
            OnLightExit();
        }
    }

    bool IsObjectLit()
    {
        Vector3 lightDirection = (transform.position - targetLight.transform.position).normalized;
        float dotProduct = Vector3.Dot(lightDirection, targetLight.transform.forward);


        return dotProduct < 0.9f;

    }

    void OnLightEnter()
    {
        targetTime -= Time.deltaTime;

        if(targetTime<0)
        {
            Debug.Log("sfafas");
            targetTime = 3.0f;
        }

        objectRenderer.material.color = Color.red;
    }

    void OnLightExit()
    {
        objectRenderer.material.color = Color.white;
    }
}
