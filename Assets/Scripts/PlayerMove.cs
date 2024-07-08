using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float mouseSensitivity = 2f;

    private CharacterController characterController;
    private Transform playerCamera;
    private float cameraPitch = 0f;
    public bool isCarRide;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main.transform;
    }

    void Update()
    {
        Moved();


    }

    void Moved()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -70f, 70f);
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Car")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                transform.position = hit.transform.position;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);

                transform.SetParent(hit.transform);
                transform.GetComponent<CharacterController>().enabled = false;
                transform.GetComponent<PlayerMove>().enabled = false;
                hit.transform.GetComponent<CarController>().enabled = true;

            }
        }
    }
}

