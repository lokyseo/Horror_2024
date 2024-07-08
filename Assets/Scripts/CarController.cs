using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 50f;
    public float turnSpeed = 50f;

    private Rigidbody rb;
    GameObject player_Object;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            player_Object = transform.GetChild(0).gameObject;

            player_Object.transform.position = transform.position + new Vector3(5,0,0);
            player_Object.transform.SetParent(null);
            player_Object.transform.GetComponent<CharacterController>().enabled = true;
            player_Object.transform.GetComponent<PlayerMove>().enabled = true;
            transform.GetComponent<CarController>().enabled = false;
        }
        float moveHorizontal = Input.GetAxis("Horizontal"); 
        float moveVertical = Input.GetAxis("Vertical"); 

        Vector3 movement = transform.forward * moveVertical * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);

        if (moveVertical > 0)
        {
            float turn = moveHorizontal * turnSpeed * Time.deltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
        else if (moveVertical < 0)
        {
            float turn = -moveHorizontal * turnSpeed * Time.deltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }
}
