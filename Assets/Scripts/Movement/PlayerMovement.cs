using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float gravity = -9.81f;
    public float groundDistance = 0.2f;
    private Vector3 velocity;
    private bool isGrounded;
    public BoxCollider waitingRoomCollider;
    public BoxCollider doctorsOfficeCollider;
    public GameObject WRtext; // waiting room
    public GameObject DOtext; // doctor's office
    public float jump  = 3f;
    public GameObject officeSpawn;
    public GameObject hallwaySpawn;
    private bool canGrabPatient = false;
    private bool canLeaveOffice = false;


    // Start is called before the first frame update
    void Start()
    {
        WRtext.SetActive(false);
        DOtext.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Gravity
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Movement
        float moveLR = Input.GetAxis("Horizontal");
        float moveFB = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveLR + transform.forward * moveFB;
        controller.Move(move * speed * Time.deltaTime);

        // Gravity Pt. 2
        if (!isGrounded) {
            velocity.y += gravity * Time.deltaTime;
        } else {
            velocity.y = 0;
        }
        controller.Move(velocity * Time.deltaTime);

        if (canGrabPatient) {
            if (Input.GetKeyDown(KeyCode.E)) {
                Debug.Log("hi");
                gameObject.transform.position = officeSpawn.transform.position;
                transform.Rotate(0, 90f, 0);
                WRtext.SetActive(false);
            }
        }
        if (canLeaveOffice) {
            if (Input.GetKeyDown(KeyCode.E)) {
                gameObject.transform.position = hallwaySpawn.transform.position;
                transform.Rotate(0, 0f, 0);
                DOtext.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("waitingRoom")) {
            WRtext.SetActive(true);
            canGrabPatient = true;
        }
        if (other.CompareTag("doctorOffice")) {
            DOtext.SetActive(true);
            canLeaveOffice = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("waitingRoom")) {
            WRtext.SetActive(false);
            canGrabPatient = false;
        }
        if (other.CompareTag("doctorOffice")) {
            DOtext.SetActive(false);
            canLeaveOffice = false;
        }
    }
}
