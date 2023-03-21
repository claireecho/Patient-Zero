using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : MonoBehaviour
{
    public CharacterController patientController;
    public GameObject officeSpawn;
    public GameObject surgerySpawn;
    public static bool isCurrentPatient = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (patientController.tag == "currentPatient") {
                if (Input.GetKeyDown(KeyCode.E)) {
                     if (PlayerMovement.canGrabPatient) {
                        isCurrentPatient = true;
                        gameObject.transform.position = officeSpawn.transform.position;
                        gameObject.transform.rotation = officeSpawn.transform.rotation;
                        // transform.Rotate(0, 10f, 0);
                    } else if (PlayerMovement.canEnterSurgery && PlayerMovement.isOrderOut && isCurrentPatient) {
                        gameObject.transform.position = surgerySpawn.transform.position;
                        gameObject.transform.rotation = surgerySpawn.transform.rotation;
                    }
                }

        }
    }//386.76 293.83 371.62
}
