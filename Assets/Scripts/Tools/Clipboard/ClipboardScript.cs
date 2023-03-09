using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool isBeingUsed = false;
    public GameObject objectiveText;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = restPosition.transform.position;
        transform.rotation = restPosition.transform.rotation;
        objectiveText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!isBeingUsed) {
                showClipboard();
                isBeingUsed = true;
            } else {
                hideClipboard();
                isBeingUsed = false;
            }
        }

    }


    void hideClipboard() {
        // changes position and rotation of clipboard
        transform.position = restPosition.transform.position;
        transform.rotation = restPosition.transform.rotation;

        // hide text on clipboard
        objectiveText.SetActive(false);
    }
    void showClipboard() {
        // changes position and rotation of clipboard
        transform.position = secondPosition.transform.position;
        transform.rotation = secondPosition.transform.rotation;

        // show text on clipboard
        objectiveText.SetActive(true);
    }
}
