using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : MonoBehaviour
{
    public CharacterController patientController;
    public GameObject officeSpawn;
    private bool patientMustMove = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.canGrabPatient) {
                if (patientController.tag == "currentPatient") {
                     if (Input.GetKeyDown(KeyCode.E)) {
                        Debug.Log("hi");
                        gameObject.transform.position = officeSpawn.transform.position;
                        gameObject.transform.rotation = officeSpawn.transform.rotation;
                        // transform.Rotate(0, 10f, 0);
                    }
                }

        }
    }//386.76 293.83 371.62
}
