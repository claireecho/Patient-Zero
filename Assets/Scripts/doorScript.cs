using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    private Animator ani;
    public GameObject door;
    public bool isOpenTrigger;
    public bool isCloseTrigger;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (Input.GetKeyDown("E")) {
                isOpenTrigger = isCloseTrigger;
                isCloseTrigger = !isCloseTrigger;
            }
        }
    }

    void Start() {
        ani = door.GetComponent<Animator>();
        isOpenTrigger = false;
        isCloseTrigger = true;
    }

    // Update is called once per frame
    void Update() {
        ani.SetBool("isOpenTriggerPressed", isOpenTrigger);
        ani.SetBool("isCloseTriggerPressed", isCloseTrigger);
    }
}
