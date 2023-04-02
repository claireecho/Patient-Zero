using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public Transform playerReference;
    public float mouseSensitivity = 5f;
    public float xRotation = 0f;
    public static bool isPaused = false;
    public GameObject dialogueCanva;

    // Start is called before the first frame update
    void Awake()
    {
        dialogueCanva = GameObject.FindGameObjectWithTag("dialogueCanva");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused) {

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            playerReference.Rotate(Vector3.up * mouseX);
        } else {
            if (Dialogue.DialogueIsPlaying)
                transform.rotation = Quaternion.Euler(20.0000057f,226f,0f);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            if (PlayerMovement.canUsePharmacy) {
                    isPaused = true;
            }
        }

    }
}
