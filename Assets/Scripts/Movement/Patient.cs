using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : MonoBehaviour
{
    public CharacterController patient1Controller;
    public GameObject officeSpawn;
    private bool patientMustMove = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (patientMustMove) {
                if (Input.GetKeyDown(KeyCode.E)) {
                    Debug.Log("hi");
                    gameObject.transform.position = officeSpawn.transform.position;
                    transform.Rotate(0, 90f, 0);
                }
        }
    }
}
