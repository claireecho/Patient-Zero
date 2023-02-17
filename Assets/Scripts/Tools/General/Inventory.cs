using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] tools = new GameObject[0];
    private int selection = 0;
    public GameObject spawn;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject t in tools) {
            t.SetActive(false);
        }
        tools[selection].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y < -0.99f || Input.GetKeyDown(KeyCode.RightBracket)) {
            if (tools.Length > 1 && selection < tools.Length-1) {
                tools[selection].SetActive(false);
                
                selection++;
                tools[selection].SetActive(true);
            }
        } else if (Input.mouseScrollDelta.y > 0.99f || Input.GetKeyDown(KeyCode.LeftBracket)) {
            if (selection != 0) {
                tools[selection].SetActive(false);
                selection--;
                tools[selection].SetActive(true);
            }
        }

    }
}
