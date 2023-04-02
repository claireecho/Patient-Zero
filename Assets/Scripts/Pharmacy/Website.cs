using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Website : MonoBehaviour
{
    public GameObject item;
    private GameObject[] items;
    public static string[] antibiotics;
    public Transform grid;
    private bool isAntibioticsLoaded = false;
    public static string grabbedAntibiotic;
    public static bool leftWebsite = false;
    public GameObject drug;
    public Transform toolHeader;
    public Button exitButton;

    // Start is called before the first frame update
    void Awake()
    {
        exitButton.onClick.AddListener(delegate{
            CameraLook.isPaused = false;
            leftWebsite = true;
            Inventory.inventory[Inventory.selection].SetActive(true);
            Inventory.isShowingDescription = true;
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAntibioticsLoaded && antibiotics.Length != 0) {
            items = new GameObject[antibiotics.Length];
            for (int i = 0; i < antibiotics.Length; i++) {
                items[i] = Instantiate(item, transform);
                items[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(antibiotics[i]);
                int temp = i;
                items[i].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate{sendAntibiotic(items[temp]);});
                items[i].transform.SetParent(grid, false);
            }
            isAntibioticsLoaded = true;
        }
    }
    private void sendAntibiotic(GameObject g) {
        grabbedAntibiotic = g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
        leftWebsite = true;
        CameraLook.isPaused = false;
        foreach (string i in antibiotics) {
                if (grabbedAntibiotic == i) {
                    GameObject[] newTools = new GameObject[Inventory.inventory.Length+1];
                    for (int j = 0; j < Inventory.inventory.Length; j++) {
                        newTools[j] = Inventory.inventory[j];
                    }
                    newTools[Inventory.inventory.Length] = Instantiate(drug, transform);
                    newTools[Inventory.inventory.Length].transform.SetParent(toolHeader, false);
                    newTools[Inventory.inventory.Length].name = grabbedAntibiotic + " Antibiotic";
                    newTools[Inventory.inventory.Length].SetActive(false);
                    Inventory.inventory = new GameObject[newTools.Length];
                    Inventory.inventory = newTools;
                    Inventory.selection = Inventory.inventory.Length-1;
                    Inventory.inventory[Inventory.selection].SetActive(true);
                    Debug.Log("Success!");
                    Website.grabbedAntibiotic = "";
                    Inventory.isShowingDescription = true;
                }
            }
    }
}
