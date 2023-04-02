using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject[] tools = new GameObject[0];
    [SerializeField] private GameObject[] surgeryTools = new GameObject[0];
    public static GameObject[] globalTools;
    public static GameObject[] inventory;
    public static int selection = 0;
    public TextMeshProUGUI description; 
    public static bool inventorySwitch = false;
    public static bool isShowingDescription = false;
    // Start is called before the first frame update
    void Awake()
    {
        inventory = new GameObject[tools.Length];
        for (int i = 0; i < tools.Length; i++) {
            inventory[i] = tools[i];
            inventory[i].SetActive(false);
        }
        inventory[selection].SetActive(true);
        StartCoroutine("showDescription");
        globalTools = tools;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CameraLook.isPaused) {
            if (Input.mouseScrollDelta.y < -0.99f || Input.GetKeyDown(KeyCode.RightBracket)) {
                if (inventory.Length > 1 && selection < inventory.Length-1) {
                    
                    StopCoroutine("showDescription");
                    inventory[selection].SetActive(false);
                    
                    selection++;
                    inventory[selection].SetActive(true);
                    StartCoroutine("showDescription");

                    if (TrashScript.canConfirmTrash) {
                        inventorySwitch = true;
                    }


                }
            } else if (Input.mouseScrollDelta.y > 0.99f || Input.GetKeyDown(KeyCode.LeftBracket)) {
                if (selection != 0) {
                    StopCoroutine("showDescription");
                    inventory[selection].SetActive(false);
                    selection--;
                    inventory[selection].SetActive(true);
                    StartCoroutine("showDescription");
                    if (TrashScript.canConfirmTrash) {
                        inventorySwitch = true;
                    }
                }
            }
        }
        if (isShowingDescription) {
            StopCoroutine("showDescription");
            StartCoroutine("showDescription");
            isShowingDescription = false;
        }

    }

    void FixedUpdate() {
        if (inventory[selection].tag != "order") {
            PlayerMovement.isOrderOut = false;
        } else {
            PlayerMovement.isOrderOut = true;
        }
        if (inventory[selection].name.IndexOf("Antibiotic") != -1) {
            PlayerMovement.isPillsOut = true;
        } else {
            PlayerMovement.isPillsOut = false;
        }
        if (TrashScript.canTrashStill) {
            foreach (GameObject i in globalTools) {
                TrashScript.canConfirmTrash = true;
                if (inventory[selection].name == i.name) {
                    TrashScript.canConfirmTrash = false;
                    break;
                }
            }
            if (TrashScript.canConfirmTrash)
                PlayerMovement.EText.SetText("Press E to throw away " + inventory[selection].name);
        }
    }

    IEnumerator showDescription() {

        description.SetText(inventory[selection].name);
        yield return new WaitForSeconds(2);
        description.SetText("");

    }

}
