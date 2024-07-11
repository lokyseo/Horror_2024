using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehindLook : MonoBehaviour
{
    public Camera mainCamera;
    bool isEnter;
    bool isExit;
    GameObject player_Object;
    

    void Start()
    {
        isEnter = false;
        isExit = false;
        player_Object = GameObject.FindWithTag("Player");
       
    }

    void Update()
    {
        transform.position = player_Object.transform.position + new Vector3(0, -2, -20);
        if (IsObjectVisible(GetComponent<Renderer>(), mainCamera))
        {
            isExit = false;
            if (!isEnter)
            {
                isEnter = true;
                Debug.Log("Look Behind");
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
               
            }
        }

    }

    bool IsObjectVisible(Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
