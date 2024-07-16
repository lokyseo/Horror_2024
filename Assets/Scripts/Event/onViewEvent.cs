using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class onViewEvent : MonoBehaviour
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
        if (IsObjectVisible(GetComponent<Renderer>(), mainCamera))
        {
            isExit = false;
            if (!isEnter)
            {
                isEnter = true;
                Debug.Log(gameObject.name + " µé¾î¿È");

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
                Debug.Log(gameObject.name + " ³ª°¨");

            }
            else
            {
                transform.position = Vector3.Lerp(transform.position,
                    new Vector3(player_Object.transform.position.x, transform.position.y, player_Object.transform.position.z), 1.2f * Time.deltaTime);
            }
        }
    }

    bool IsObjectVisible(Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
