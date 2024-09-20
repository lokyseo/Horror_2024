using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acquire_Item : MonoBehaviour
{
    public float detectionRange = 20f;
    float interactableDistance;

    public bool isKeyHave;

    private void Start()
    {
        isKeyHave = false;
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, detectionRange))
        {
            if (hit.transform.CompareTag("Key"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    isKeyHave = true;
                    Destroy(hit.transform.gameObject);
                }


            }
        }
        Debug.DrawRay(transform.position, transform.forward * detectionRange, Color.green);
    }
}
