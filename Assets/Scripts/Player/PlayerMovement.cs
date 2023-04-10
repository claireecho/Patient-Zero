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
    private bool canConfirmWithPatient = false; // checks if player is standing in confirm collider
    public static bool ConfirmedWithPatient = false; // checks if player has confirmed with patient
    public static TextMeshProUGUI EText;
    public GameObject postSurgerySpawn; // where PLAYER will spawn after interacting with exitSurgery collider
    private bool canExitSurgery = false; // checks if player is standing in exitSurgery collider
    public static bool canConfirmExit = false; // checks if player can confirm exit from surgery
    public static bool grabbedPatient = false; // checks if player has grabbed patient
    public static bool canUsePharmacy = false; // checks if player is standing in pharmacy collider
    public GameObject pharmacyWebsite; // website for pharmacy
    public static bool queueDialogue = false; // checks if player has summoned a patient
    public static bool isPillsOut = false; // checks if pills are out
    public static bool isTreatmentCollider = false; // checks if player is standing in treatment collider
    public static Collider waitingRoomCollider; // waiting room collider
    public GameObject defaultSpawn; // where PLAYER will spawn at default
    public static Collider surgeryCollider; // allows player to send patient into surgery

    public AudioSource audioSource;

    public AudioClip walkingSound;
    public AudioClip wrongSound;
    public AudioClip doorSound;
    public AudioClip pharmacySound;

    public static bool _inSurgery = false;

    int score;

    // Start is called before the first frame update
    void Awake()
    {
        score = 0;
        EText = GameObject.FindWithTag("text").GetComponent<TextMeshProUGUI>();
        EText.SetText("");
        pharmacyWebsite.SetActive(false);
        waitingRoomCollider = GameObject.FindGameObjectWithTag("waitingRoom").GetComponent<Collider>();
        surgeryCollider = GameObject.FindGameObjectWithTag("surgeryRoom").GetComponent<Collider>();
        surgeryCollider.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        // Gravity
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Movement
        if (CameraLook.isPaused == false) {
            float moveLR = Input.GetAxis("Horizontal");
            float moveFB = Input.GetAxis("Vertical");
            Vector3 move = transform.right * moveLR + transform.forward * moveFB;
            controller.Move(move * speed * Time.deltaTime);
            if (move != Vector3.zero) {
                if (!audioSource.isPlaying) {
                    audioSource.clip = walkingSound;
                    audioSource.loop = true;
                    audioSource.Play();
                } else {
                    if (audioSource.clip == walkingSound) {
                        audioSource.UnPause();
                    }
                }
            } else {
                if (audioSource.clip == walkingSound) {
                    audioSource.Pause();
                }
            }
        } else {
            audioSource.Pause();
        }

        // Gravity Pt. 2
        if (!isGrounded) {
            velocity.y += gravity * Time.deltaTime;
        } else {
            velocity.y = 0.2f;
        }
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E)) {
            if (canGrabPatient) {
                playSound(doorSound);
                grabbedPatient = true;
                canGrabPatient = false;
                Debug.Log("waitingRoom Collider disabled");
                waitingRoomCollider.enabled = false;
                gameObject.transform.position = officeSpawn.transform.position;
                transform.rotation = Quaternion.Euler(0, -133, 0);
                EText.SetText("");
                queueDialogue = true;
            } else if (canLeaveOffice) {
                playSound(doorSound);
                gameObject.transform.position = hallwaySpawn.transform.position;
                EText.SetText("");
            } else if (canEnterOffice) {
                playSound(doorSound);
                gameObject.transform.position = officeSpawn.transform.position;
                EText.SetText("");
            } else if (canConfirmWithPatient) {
                // double check if player wants to exit
                if (ClipboardScript.dropdown.captionText.text == "N/A") {
                    EText.SetText("You must choose a diagnosis before confirming with your patient");
                    playSound(wrongSound);
                    EText.color = TrashScript.red;
                    canConfirmWithPatient = false;
                } else {
                    EText.SetText("Are you sure [" + ClipboardScript.dropdown.captionText.text + "] is the current diagnosis for " + PatientGameplay.patient.getFirstName() + "? (Y/N)");
                    EText.color = TrashScript.red;
                }
            } else if (canUsePharmacy) {
                pharmacyWebsite.SetActive(true);
                Inventory.inventory[Inventory.selection].SetActive(false);
                EText.SetText("");
                playSound(pharmacySound);
            } else if (canExitSurgery) {
                EText.SetText("You will abandon this patient. Are you sure you want to leave? (Y/N)");
                EText.color = TrashScript.red;
                canConfirmExit = true;
                canExitSurgery = false;
            }
        }

        if (isTreatmentCollider && PatientGameplay.isCurrentPatient) {
            if (PatientGameplay.confirmCollider.activeSelf == false) {
                if (isPillsOut) {
                    EText.SetText("Press E to give " + Inventory.inventory[Inventory.selection].name + " to " + PatientGameplay.patient.getFirstName());
                } else if (isOrderOut) {
                    EText.SetText("Press E to send " + PatientGameplay.patient.getFirstName() + " into surgery");
                } else {
                    EText.SetText("");
                }
            }
        }

        if (Website.leftWebsite) {
            playSound(pharmacySound);
            pharmacyWebsite.SetActive(false);
            Website.leftWebsite = false;
        }
        // GIVING PATIENT MEDICATION ----------------------------
        if (Input.GetKeyDown(KeyCode.E) && isPillsOut && isTreatmentCollider && PatientGameplay.confirmCollider.activeSelf == false) {

            // Checks if patient got correct antibiotic
            if (Inventory.inventory[Inventory.selection].name.IndexOf(PatientGameplay.patient.getDiagnosis()) != -1) {
                PatientGameplay.patient.Success();
                gameObject.transform.position = officeSpawn.transform.position;
                transform.rotation = Quaternion.Euler(0, -133, 0);
            } else {
                PatientGameplay.patient.Failure();
                gameObject.transform.position = officeSpawn.transform.position;
                transform.rotation = Quaternion.Euler(0, -133, 0);
            }


            // Destroy the pill from the inventory
            Destroy(Inventory.inventory[Inventory.selection]);
            GameObject[] newTools = new GameObject[Inventory.inventory.Length-1];
            for (int i = 0; i < Inventory.inventory.Length; i++) {
                if (i < Inventory.selection) {
                    newTools[i] = Inventory.inventory[i];
                } else if (i > Inventory.selection) {
                    newTools[i-1] = Inventory.inventory[i];
                }
            }
            Inventory.inventory = new GameObject[newTools.Length];
            Inventory.inventory = newTools;
            Inventory.selection = 0;
            Inventory.inventory[Inventory.selection].SetActive(true);

        } 


        // SENDING PATIENT INTO SURGERY ----------------------------
        if (Input.GetKeyDown(KeyCode.E) && canEnterSurgery && isOrderOut) {
                gameObject.transform.position = surgerySpawn.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(0, -210.947f, 0);
                _inSurgery = true;
                EText.SetText("");
                EText.color = Color.black;
                playSound(doorSound);
        }

        // FOR LEAVING SURGERY ----------------------------
        if (Input.GetKeyDown(KeyCode.Y) && canConfirmExit) {
            gameObject.transform.position = postSurgerySpawn.transform.position;
            EText.SetText("");
            canConfirmExit = false;
            EText.color = Color.black;
            reset();
            playSound(doorSound);
        } else if (Input.GetKeyDown(KeyCode.N) && canConfirmExit) {
            EText.SetText("");
            canConfirmExit = false;
            EText.color = Color.black;
        }

        // CONFIRMING DIAGNOSIS WITH PATIENT ----------------------------
        if (Input.GetKeyDown(KeyCode.Y) && canConfirmWithPatient) {
            ConfirmedWithPatient = true;
            EText.SetText("");
            canConfirmWithPatient = false;
            EText.color = Color.black;
            ClipboardScript.diagnosisText.SetText(ClipboardScript.dropdown.captionText.text);
            ClipboardScript.dropDownObject.SetActive(false);
            surgeryCollider.enabled = true;
            Debug.Log("Surgery collider enabled");
        } else if (Input.GetKeyDown(KeyCode.N) && canConfirmWithPatient) {
            EText.SetText("");
            canConfirmWithPatient = false;
            EText.color = Color.black;
        }

        // DIALOGUE FOR CONFIRMING DIAGNOSIS WITH PATIENT ----------------------------
        if (ConfirmedWithPatient) {
            gameObject.transform.position = officeSpawn.transform.position;
            transform.rotation = Quaternion.Euler(0, -133, 0);
            CameraLook.isPaused = true;
        }


        if (PatientGameplay.isCompleted) {
            reset();
            score++;
            PatientGameplay.isCompleted = false;
            gameObject.transform.position = officeSpawn.transform.position;
            transform.rotation = Quaternion.Euler(0, -133, 0);
        }

    }

    public void playSound(AudioClip sound) {
        audioSource.Stop();
        audioSource.clip = sound;
        audioSource.Play();
        audioSource.loop = false;
    }

    // RESETS PLAYER
    public void reset() {
        EText.SetText("");
        Debug.Log("waitingRoom Collider enabled");
        waitingRoomCollider.enabled = true;
        surgeryCollider.enabled = false;

        Inventory.inventory = new GameObject[Inventory.globalTools.Length];
        Inventory.inventory = Inventory.globalTools;
        Inventory.selection = 0;
        Inventory.inventory[Inventory.selection].SetActive(true);

        ClipboardScript.diagnosisText.SetText("");
        ClipboardScript.dropDownObject.SetActive(true);
        ClipboardScript.dropdown.value = 0;

        queueDialogue = false;
        grabbedPatient = false;
    }

    // collider for entrances
    private void OnTriggerEnter(Collider other) {
        EText.color = Color.black;
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
        } else if (other.CompareTag("confirmWithPatient") && PatientGameplay.isCurrentPatient) {
            EText.SetText("Press E to confirm diagnosis with " + PatientGameplay.patient.getFirstName());
            canConfirmWithPatient = true;
        } else if (other.CompareTag("pharmacy")) {
            EText.SetText("Press E to access pharmacy supply");
            canUsePharmacy = true;
        } else if (other.CompareTag("treatment")) {
            isTreatmentCollider = true;
        } else if (other.CompareTag("postSurgery")) {
            EText.SetText("Press E to leave surgery room");
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
        } else if (other.CompareTag("confirmWithPatient")) {
            canConfirmWithPatient = false;
        } else if (other.CompareTag("pharmacy")) {
            canUsePharmacy = false;
        } else if (other.CompareTag("treatment")) {
            isTreatmentCollider = false;
        } else if (other.CompareTag("postSurgery")) {
            canExitSurgery = false;
        }
        EText.SetText("");
    }

}
