using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashScript : MonoBehaviour
{

    public bool canDestroy = false;
    public static bool canConfirmTrash = false;
    public static Color red = new Color(0.5188679f, 0, 0 , 1);
    public bool itemSwitched = false;
    public static bool canTrashStill = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (canConfirmTrash) {
                canTrashStill = false;
                canDestroy = true;
                PlayerMovement.EText.color = red;
                PlayerMovement.EText.SetText("Are you sure you want to throw away " + Inventory.inventory[Inventory.selection].name + "? (Y/N)");
                canConfirmTrash = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Y) && canConfirmTrash) {
            Destroy(Inventory.inventory[Inventory.selection]);
            GameObject[] newTools = new GameObject[Inventory.inventory.Length-1];
            for (int i = 0; i < Inventory.inventory.Length; i++) {
                if (i < Inventory.selection) {
                    newTools[i] = Inventory.inventory[i];
                } else if (i > Inventory.selection) {
                    newTools[i-1] = Inventory.inventory[i];
                }
            }
            Inventory.inventory = new GameObject[newTools.Length];
            Inventory.inventory = newTools;
            Inventory.selection = 0;
            Inventory.inventory[Inventory.selection].SetActive(true);
            canDestroy = false;
            PlayerMovement.EText.SetText("");
            PlayerMovement.EText.color = Color.black;
            canConfirmTrash = false;
        } else if (Input.GetKeyDown(KeyCode.N) && canConfirmTrash) {
            PlayerMovement.EText.SetText("");
            PlayerMovement.EText.color = Color.black;
            canConfirmTrash = false;
        }


        if (canConfirmTrash) {
            if (Inventory.inventorySwitch) {
                canTrashStill = true;
                PlayerMovement.EText.SetText("");
                PlayerMovement.EText.color = Color.black;
                Inventory.inventorySwitch = false;
                canConfirmTrash = false;
            }
        }

    }

    

    void OnTriggerEnter(Collider other) {
        PlayerMovement.EText.color = Color.black;
        if (other.gameObject.tag == "trash") {
            canTrashStill = true;
            foreach (GameObject i in Inventory.globalTools) {
                canConfirmTrash = true;
                if (Inventory.inventory[Inventory.selection].name == i.name) {
                    canConfirmTrash = false;
                    break;
                }
            }
            if (canConfirmTrash)
                PlayerMovement.EText.SetText("Press E to throw away " + Inventory.inventory[Inventory.selection].name);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "trash") {
            canTrashStill = false;
            canConfirmTrash = false;
            PlayerMovement.EText.SetText("");
        }
    }

}
