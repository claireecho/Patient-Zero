using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class surgery : MonoBehaviour
{

    public GameObject canva;
    public static TextMeshProUGUI canvaText;

    // Start is called before the first frame update
    void Awake()
    {
        canvaText = canva.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        canva.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement._inSurgery) {
            StopCoroutine("showTitle");
            StartCoroutine("showTitle");
            // inventory to turn into surgeryTools
            Inventory.inventory = new GameObject[Inventory.globalSurgeryTools.Length];
            for (int i = 0; i < Inventory.globalSurgeryTools.Length; i++) {
                Inventory.inventory[i] = Inventory.globalSurgeryTools[i];
            }
            // player has to follow procedure

            // if player fails, patient dies

            PlayerMovement._inSurgery = false;

        }
    }

    /* Surgery Notes
    Monitor will show patient heart rate
        - Flat lines if you don't follow procedure correctly
        - Otherwise, will stay at a steady rate
    TV will show instructions with step-by-step procedure on how to do surgery
    Inventory:
        - Clipboard
        - Anesthesia
        - Scalpel
        - Scissors
        - Forceps
        - Suture
        - Needle
        - Syringe
        - Sponge
        - Bandage
        - Tape
        - Gauze
        - Tongs
        - Tweezer

    */

    IEnumerator showTitle() {

        canva.SetActive(true);
        canvaText.text = "Surgery: " + ClipboardScript.diagnosisText.text;
        CameraLook.isPaused = true;
        Inventory.inventory[Inventory.selection].SetActive(false);
        yield return new WaitForSeconds(3);
        canva.SetActive(false);
        CameraLook.isPaused = false;
        Inventory.selection = 0;
        Inventory.inventory[0].SetActive(true);

    }


}
