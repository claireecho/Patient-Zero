using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class surgery : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement._inSurgery) {



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


}
