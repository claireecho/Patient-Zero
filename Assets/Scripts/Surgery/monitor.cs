using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monitor : MonoBehaviour
{

    public GameObject canvas;
    private Animator anim;
    public static bool isInSurgery = false;

    void Awake() {
        anim = canvas.transform.GetChild(0).GetComponent<Animator>();
    }

    void Start(){
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isInSurgery) {
            canvas.SetActive(true);
        } else {
            canvas.SetActive(false);
        }
        if (PatientGameplay.patient.getStatus()) {
            anim.SetBool("isAlive", true);
        } else {
            anim.SetBool("isAlive", false);
        }
    }
}
