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
    public GameObject WRtext; // waiting room
    public float jump  = 3f;
    public GameObject officeSpawn;
    public static bool canGrabPatient = false;
    public BoxCollider hallwayRoomCollider;
    public BoxCollider officeExitColder;
    public GameObject OEtext;
    private bool canLeaveOffice = false;
    public GameObject hallwaySpawn;
    public GameObject Htext;
    private bool canEnterOffice = false;

    // Start is called before the first frame update
    void Start()
    {
        WRtext.SetActive(false);
        OEtext.SetActive(false);
        Htext.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.E)) {
            if (canGrabPatient) {
                Debug.Log("hi");
                gameObject.transform.position = officeSpawn.transform.position;
                transform.Rotate(0, 90f, 0);
                WRtext.SetActive(false);
            } else if (canLeaveOffice) {
                gameObject.transform.position = hallwaySpawn.transform.position;
                OEtext.SetActive(false);
            } else if (canEnterOffice) {
                gameObject.transform.position = officeSpawn.transform.position;
                Htext.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("waitingRoom")) {
            WRtext.SetActive(true);
            canGrabPatient = true;
        } else if (other.CompareTag("officeRoom")) {
            OEtext.SetActive(true);
            canLeaveOffice = true;
        } else if (other.CompareTag("hallwayRoom")) {
            Htext.SetActive(true);
            canEnterOffice = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("waitingRoom")) {
            WRtext.SetActive(false);
            canGrabPatient = false;
        } else if (other.CompareTag("officeRoom")) {
            OEtext.SetActive(false);
            canLeaveOffice = false;
        } else if (other.CompareTag("hallwayRoom")) {
            Htext.SetActive(false);
            canEnterOffice = false;
        }
        
    }

}
