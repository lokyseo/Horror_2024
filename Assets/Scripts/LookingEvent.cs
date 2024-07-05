using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LookingEvent : MonoBehaviour
{
    bool isLookingAtObject;

    void Start()
    {
        
    }

    void Update()
    {
        CheckIfLookingAtObject();
    }

    void CheckIfLookingAtObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform)
            {
                if (!isLookingAtObject)
                {
                    isLookingAtObject = true;
                    OnLookAtObject();
                }
            }
            else
            {
                if (isLookingAtObject)
                {
                    isLookingAtObject = false;
                    OnLookAwayFromObject();
                }
            }
        }
        else
        {
            if (isLookingAtObject)
            {
                isLookingAtObject = false;
                OnLookAwayFromObject();
            }
        }
    }

    void OnLookAtObject()
    {

    }

    void OnLookAwayFromObject()
    {

    }

}
