using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Inventory : MonoBehaviour
{
    public GameObject[] tools = new GameObject[0];
    private int selection = 0;
    public TextMeshProUGUI description;
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
