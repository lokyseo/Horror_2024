using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float mouseSensitivity = 2f;

    private CharacterController characterController;
    private Transform playerCamera;
    private float cameraPitch = 0f;

    //중력
    private float gravity = 9.8f;
    private float verticalVelocity = 0.0f;
    private Vector3 moveDirection = Vector3.zero;

    public Slider sound_Slider;
    bool isMoving;

    public GameObject flashLight;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main.transform;
    }

    void Update()
    {
        Moved();

        if (Input.GetKeyDown(KeyCode.C))
        {
            flashLight.SetActive(!flashLight.activeSelf);

        }


            if (characterController.isGrounded)
        {
            verticalVelocity = 0.0f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        moveDirection.y = verticalVelocity;

        characterController.Move(moveDirection * Time.deltaTime);
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
        isMoving = moveDirection.x != 0 || moveDirection.z != 0;
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
                hit.transform.GetComponent<MapControll>().enabled = true;
                hit.transform.GetComponent<CarController>().enabled = true;

            }
        }

        if(hit.gameObject.tag == "Trap")
        {
            //애니메이션 재생
        }

        if(hit.gameObject.tag == "DangerZone")
        {
            if(isMoving)
            {
                sound_Slider.value += 0.005f;
            }
            else
            {
                sound_Slider.value -= 0.001f;

            }
            //사운드 재생
            Debug.Log("바스락바스락");
        }
    }
}

