using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
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
    public AudioSource footSound;
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
                this.GetComponentInChildren<Animator>().enabled = false;
            }
            else
            {
                footSound.Stop();

            }

        }
        else
        {
            isEnter = false;
            if (!isExit)
            {
                isExit = true;
                this.GetComponentInChildren<Animator>().enabled = true;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position,
                    new Vector3(player_Object.transform.position.x, transform.position.y, player_Object.transform.position.z), 0.7f * Time.deltaTime);
                transform.LookAt(player_Object.transform.position);
                if(Vector3.Distance(player_Object.transform.position, transform.position) < 20.0f)
                {
                    if(!footSound.isPlaying)
                    {
                        footSound.Play();
                    }
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
