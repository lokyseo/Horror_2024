using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;

    private CharacterController characterController;
    private Transform playerCamera;
    private float cameraPitch = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main.transform;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // 플레이어 회전
        transform.Rotate(Vector3.up * mouseX);

        // 카메라 피치 조정
        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        Moved();
    }

    void Moved()
    {
        // 키보드 입력 받기
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // 이동 방향 설정
        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}

