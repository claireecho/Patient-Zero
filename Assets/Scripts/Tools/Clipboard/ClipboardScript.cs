using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ClipboardScript : MonoBehaviour
{

    /* Clipboard Notes

    - After completing a task
        - check mark???
    - Spawn a patient
    - (tasks will move to TV in surgery room)
    - Finished with patient

    - Check boxes with tasks
    - Take temperature
    - Check blood pressure
    - give shot
    - check heart rate
    -

    */


    public GameObject restPosition;
    public GameObject secondPosition;
    public static bool isBeingUsed = false;
    public static TextMeshProUGUI objectiveText;
    private static TextMeshProUGUI titleText;

    // Start is called before the first frame update
    void Start()
    {
        objectiveText = GameObject.FindWithTag("clipboardText").GetComponent<TextMeshProUGUI>();
        titleText = GameObject.FindWithTag("clipboardTitle").GetComponent<TextMeshProUGUI>();
        hideClipboard();
    }

    void OnDisable() {
        hideClipboard();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!isBeingUsed) {
                showClipboard();
            } else {
                hideClipboard();
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            if (isBeingUsed) {
                hideClipboard();
            }
        }

        
    }


    void hideClipboard() {
        // changes position and rotation of clipboard
        transform.position = restPosition.transform.position;
        transform.rotation = restPosition.transform.rotation;

        // hide text on clipboard
        objectiveText.SetText("");
        titleText.SetText("");

        isBeingUsed = false;
        
    }
    void showClipboard() {
        // changes position and rotation of clipboard
        transform.position = secondPosition.transform.position;
        transform.rotation = secondPosition.transform.rotation;

        // show text on clipboard
        objectiveText.SetText(PatientGameplay.patient.toString());
        titleText.SetText("Patient Information");
        
        isBeingUsed = true;
    }
}
