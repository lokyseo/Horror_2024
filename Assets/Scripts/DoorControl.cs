using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    public bool isLocked;

    public bool isOpen;
    public Transform targetRotation;

    void Start()
    {
        isLocked = true;
    }

    void Update()
    {
        if(isOpen)
        {
            targetRotation.localEulerAngles = new Vector3(0, 90, 0);
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, targetRotation.rotation, 500.0f * Time.deltaTime);
        }
        else
        {
            targetRotation.localEulerAngles = new Vector3(0, 0, 0);
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, targetRotation.rotation, 500.0f * Time.deltaTime);
        }
    }
}
