using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller; // Determines what PLAYER is moving
    public float speed = 5f; // Determines how fast PLAYER moves
    public Transform groundCheck; // Determines where PLAYER's ground check is
    public LayerMask groundMask; // Determines what is considered ground
    public float gravity = -9.81f; // Determines how fast PLAYER falls (acceleration)
    public float groundDistance = 0.2f; // Determines how close PLAYER needs to be to the ground to be considered grounded
    private Vector3 velocity; // Determines how fast PLAYER is moving
    private bool isGrounded; // Determines if PLAYER is on the ground
    public GameObject officeSpawn; // where PLAYER will spawn after interacting with waiting room collider / hallway collider
    public static bool canGrabPatient = false; // checks if player is standing in waiting room collider
    private bool canLeaveOffice = false; // checks if player is standing in officeExit collider
    public GameObject hallwaySpawn; // where PLAYER will spawn after interacting with officeExit collider
    private bool canEnterOffice = false; // checks if player is standing in office collider
    public static bool canEnterSurgery = false; // checks if player is standing in surgery collider
    public GameObject surgerySpawn; // where PLAYER will spawn after interacting with surgery collider
    public static bool isOrderOut = false; // checks if order is out
    private bool canExitSurgery = false; // checks if player is standing in exitSurgery collider
    public static TextMeshProUGUI EText;
    public GameObject postSurgerySpawn; // where PLAYER will spawn after interacting with exitSurgery collider
    private bool canConfirmExit = false; // checks if player can confirm exit

    // Start is called before the first frame update
    void Start()
    {
        EText = GameObject.FindWithTag("text").GetComponent<TextMeshProUGUI>();
        EText.SetText("");
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
            velocity.y = 0.2f;
        }
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E)) {
            if (canGrabPatient) {
                gameObject.transform.position = officeSpawn.transform.position;
                transform.Rotate(0, 90f, 0);
                EText.SetText("");
            } else if (canLeaveOffice) {
                gameObject.transform.position = hallwaySpawn.transform.position;
                EText.SetText("");
            } else if (canEnterOffice) {
                gameObject.transform.position = officeSpawn.transform.position;
                EText.SetText("");
            } else if (canEnterSurgery && isOrderOut && Patient.isCurrentPatient) {
                gameObject.transform.position = surgerySpawn.transform.position;
                EText.SetText("");
            } else if (canExitSurgery) {
                // double check if player wants to exit
                EText.SetText("Are you sure you want to exit surgery? Your progress will be lost if surgery has not been completed. (Y/N)");
                canConfirmExit = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Y) && canConfirmExit) { // for when you want to leave surgery
            gameObject.transform.position = postSurgerySpawn.transform.position;
            EText.SetText("");
        } else if (Input.GetKeyDown(KeyCode.N) && canConfirmExit) {
            EText.SetText("");
        }

        if (canEnterSurgery && isOrderOut && Patient.isCurrentPatient) {
            EText.SetText("Press E to send patient into surgery");
        } else if (canEnterSurgery && !isOrderOut) {
            EText.SetText("");
        }

    }


    // collider for entrances
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("waitingRoom")) {
            EText.SetText("Press E to grab patient");
            canGrabPatient = true;
        } else if (other.CompareTag("officeRoom")) {
            EText.SetText("Press E to enter hallway");
            canLeaveOffice = true;
        } else if (other.CompareTag("hallwayRoom")) {
            EText.SetText("Press E to enter office");
            canEnterOffice = true;
        } else if (other.CompareTag("surgeryRoom")) {
            canEnterSurgery = true;
        } else if (other.CompareTag("postSurgery")) {
            EText.SetText("Press E to exit surgery");
            canExitSurgery = true;
        }
    }

    // collider for exits
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("waitingRoom")) {
            canGrabPatient = false;
        } else if (other.CompareTag("officeRoom")) {
            canLeaveOffice = false;
        } else if (other.CompareTag("hallwayRoom")) {
            canEnterOffice = false;
        } else if (other.CompareTag("surgeryRoom")) {
            canEnterSurgery = false;
        } else if (other.CompareTag("postSurgery")) {
            canExitSurgery = false;
        }
        EText.SetText("");
    }

}
