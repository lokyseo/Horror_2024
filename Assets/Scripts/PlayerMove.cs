using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    float moveSpeed;
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

    public Slider stamina_Slider;
    bool isStaminaDown;
    float staminaCoolTime;

    //오디오 리소스
    public AudioSource footSound;
    public AudioSource leafFootSound;

    void Start()
    {
        moveSpeed = 10f;

        characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main.transform;
        isStaminaDown = false;

        staminaCoolTime = 4.0f;
    }

    void Update()
    {
        Moved();
        
        GravityAndFlashLight();
        


    }

    void GravityAndFlashLight()
    {
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

        if(Input.GetKey(KeyCode.LeftShift))
        {
            if(!isStaminaDown && isMoving)
            {
                moveSpeed = 15.0f;
                stamina_Slider.value -= 0.1f;
                footSound.pitch = 1.5f;

                if(stamina_Slider.value <= 0)
                {
                    isStaminaDown = true;
                    moveSpeed = 0;
                }
            }
            
        }
        else
        {
            if (!isStaminaDown && isMoving)
            {
                moveSpeed = 10.0f;
                footSound.pitch = 1.0f;
            }
            stamina_Slider.value += 0.02f;
        }

        if(isStaminaDown)
        {
            footSound.Stop();
            staminaCoolTime -= Time.deltaTime;
            if(staminaCoolTime <=0)
            {
                isStaminaDown = false;
                staminaCoolTime = 4.0f;
            }
        }

        if(isMoving)
        {
            if (!footSound.isPlaying)
            {
                footSound.Play();
            }
        }
        else
        {
            footSound.Stop();
        }
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
            footSound.Stop();

            if (isMoving)
            {
                sound_Slider.value += 0.005f;

                if(!leafFootSound.isPlaying)
                {
                    leafFootSound.Play();
                }
            }
            else
            {
                sound_Slider.value -= 0.001f;
                leafFootSound.Stop();
            }
        }
    }
}

