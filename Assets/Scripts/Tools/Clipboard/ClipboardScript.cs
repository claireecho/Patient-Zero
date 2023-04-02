using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    public static TMPro.TMP_Dropdown dropdown;
    public GameObject clipboardCanvas;

    // Start is called before the first frame update
    void Awake()
    {
        objectiveText = GameObject.FindWithTag("clipboardText").GetComponent<TextMeshProUGUI>();
        titleText = GameObject.FindWithTag("clipboardTitle").GetComponent<TextMeshProUGUI>();
        dropdown = GameObject.FindWithTag("clipboardDropdown").GetComponent<TMPro.TMP_Dropdown>();
        hideClipboard();
    }

    void Start() {
        dropdown.ClearOptions();
        dropdown.AddOptions(new List<string>{"N/A"});
        dropdown.AddOptions(new List<string>(PatientGameplay.diagnoses));
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
        clipboardCanvas.SetActive(false);

        isBeingUsed = false;
        CameraLook.isPaused = false;
        
    }
    void showClipboard() {
        // changes position and rotation of clipboard
        clipboardCanvas.SetActive(true);
        transform.position = secondPosition.transform.position;
        transform.rotation = secondPosition.transform.rotation;
        CameraLook.isPaused = true;


        // show text on clipboard
        objectiveText.SetText(PatientGameplay.patient.toString());
        titleText.SetText("Patient Information");
        
        isBeingUsed = true;
    }
}
