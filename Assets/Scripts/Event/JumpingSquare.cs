using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingSquare : MonoBehaviour
{
    public Camera mainCamera;
    bool isEnter;
    bool isExit;
    GameObject player_Object;
    float countExitTime;
    AudioSource footSound;

    void Start()
    {
        isEnter = false;
        isExit = false;
        player_Object = GameObject.FindWithTag("Player");
        countExitTime = 3.0f;
        footSound = GetComponent<AudioSource>();
        footSound.pitch = 8.0f;
    }

    void Update()
    {
        if (IsObjectVisible(GetComponent<Renderer>(), mainCamera))
        {
            isExit = false;
            if (!isEnter)
            {
                isEnter = true;

            }
            else
            {
                
            }

        }
        else
        {
            isEnter = false;
            if (!isExit)
            {
                isExit = true;
            }
            else
            {
                countExitTime -= Time.deltaTime;

                if (countExitTime < 0)
                {
                    Debug.Log(countExitTime);
                    footSound.Play();
                    countExitTime = 500.0f;
                }
            }
        }
        
    }

    bool IsObjectVisible(Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
