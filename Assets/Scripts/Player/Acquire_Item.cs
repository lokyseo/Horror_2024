using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Acquire_Item : MonoBehaviour
{
    public float detectionRange = 20f;
    float interactableDistance;

    public bool isKeyHave;
    public Image inven_Image;
    public Sprite key_Sprite;

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
                    inven_Image.sprite = key_Sprite;
                }
            }

            if(hit.transform.CompareTag("Door"))
            {
                if(hit.transform.GetComponent<DoorControl>().isLocked)
                {
                    if (Input.GetKeyDown(KeyCode.E) && isKeyHave)
                    {
                        hit.transform.GetComponent<DoorControl>().isLocked = false;
                        isKeyHave = false;
                        inven_Image.sprite = null;
                    }
                    else
                    {

                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        hit.transform.GetComponent<DoorControl>().isOpen = !hit.transform.GetComponent<DoorControl>().isOpen;
                    }
                }
                
            }
        }
        Debug.DrawRay(transform.position, transform.forward * detectionRange, Color.green);
    }
}
