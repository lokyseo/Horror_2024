using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControll : MonoBehaviour
{
    public GameObject maplight;

    public float blinkTime;
    public float randTime;
    public float onTime;
    public bool isLightOn;

    void Start()
    {
        randTime = Random.Range(0, 1.3f);
        blinkTime = 1.3f;

        onTime = 0.5f;
        isLightOn = false;
    }

    void Update()
    {
        if (maplight.gameObject.activeSelf)
        {
            onTime -= Time.deltaTime;

            if(onTime < 0)
            {
                onTime = 0.3f;
                maplight.gameObject.SetActive(false);
            }

        }
        else
        {
            blinkTime -= Time.deltaTime;

            if (blinkTime < 0)
            {
                randTime = Random.Range(0, 1.3f);

                blinkTime = randTime;
                maplight.gameObject.SetActive(true);
            }
        }


    }
}
