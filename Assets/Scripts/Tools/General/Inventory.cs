using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject[] tools = new GameObject[0];
    private int selection = 0;
    public TextMeshProUGUI description; 
    public GameObject drug;
    public Transform toolHeader;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject t in tools) {
            t.SetActive(false);
        }
        tools[selection].SetActive(true);
        StartCoroutine("showDescription");
    }

    // Update is called once per frame
    void Update()
    {
        if (!CameraLook.isPaused) {
            if (Input.mouseScrollDelta.y < -0.99f || Input.GetKeyDown(KeyCode.RightBracket)) {
                if (tools.Length > 1 && selection < tools.Length-1) {
                    
                    StopCoroutine("showDescription");
                    tools[selection].SetActive(false);
                    
                    selection++;
                    tools[selection].SetActive(true);
                    StartCoroutine("showDescription");


                }
            } else if (Input.mouseScrollDelta.y > 0.99f || Input.GetKeyDown(KeyCode.LeftBracket)) {
                if (selection != 0) {
                    StopCoroutine("showDescription");
                    tools[selection].SetActive(false);
                    selection--;
                    tools[selection].SetActive(true);
                    StartCoroutine("showDescription");
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            if (PlayerMovement.canUsePharmacy) {
                tools[selection].SetActive(false);
            } else if (!PlayerMovement.canUsePharmacy) {
                tools[selection].SetActive(true);
            }
        }

        void onMouseDown() {
            foreach (string i in Website.antibiotics) {
                if (Website.grabbedAntibiotic == i) {
                    GameObject[] newTools = new GameObject[tools.Length+1];
                    for (int j = 0; j < tools.Length; j++) {
                        newTools[j] = tools[j];
                    }
                    newTools[tools.Length] = Instantiate(drug, transform);
                    Debug.Log(newTools[tools.Length].name);
                    newTools[tools.Length].SetActive(false);
                    tools = new GameObject[newTools.Length];
                    tools = newTools;
                }
            }
        }

    }

    void FixedUpdate() {
        if (tools[selection].name != "order") {
            PlayerMovement.isOrderOut = false;
        } else {
            PlayerMovement.isOrderOut = true;
        }
    }

    IEnumerator showDescription() {

        description.SetText(tools[selection].name);
        yield return new WaitForSeconds(2);
        description.SetText("");

    }

}
