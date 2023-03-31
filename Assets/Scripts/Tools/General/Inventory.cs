using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject[] tools = new GameObject[0];
    public static GameObject[] inventory;
    public static int selection = 0;
    public TextMeshProUGUI description; 
    // Start is called before the first frame update
    void Start()
    {
        inventory = new GameObject[tools.Length];
        for (int i = 0; i < tools.Length; i++) {
            inventory[i] = tools[i];
            inventory[i].SetActive(false);
        }
        inventory[selection].SetActive(true);
        StartCoroutine("showDescription");
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


                }
            } else if (Input.mouseScrollDelta.y > 0.99f || Input.GetKeyDown(KeyCode.LeftBracket)) {
                if (selection != 0) {
                    StopCoroutine("showDescription");
                    inventory[selection].SetActive(false);
                    selection--;
                    inventory[selection].SetActive(true);
                    StartCoroutine("showDescription");
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            if (PlayerMovement.canUsePharmacy) {
                inventory[selection].SetActive(false);
            } else if (!PlayerMovement.canUsePharmacy) {
                inventory[selection].SetActive(true);
            }
        }

    }

    void FixedUpdate() {
        if (inventory[selection].name != "order") {
            PlayerMovement.isOrderOut = false;
        } else {
            PlayerMovement.isOrderOut = true;
        }
    }

    IEnumerator showDescription() {

        description.SetText(inventory[selection].name);
        yield return new WaitForSeconds(2);
        description.SetText("");

    }

}
