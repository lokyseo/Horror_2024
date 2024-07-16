using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepOnEvent : MonoBehaviour
{
    Transform targetTransform;
    GameObject trapWall_Object;
    bool isTrapActivated;
    void Start()
    {
        isTrapActivated = false;
        trapWall_Object = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if(isTrapActivated)
        {
            trapWall_Object.transform.position += Vector3.left * 10.0f * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("asfafs");
            isTrapActivated = true;
        }
    }
}
