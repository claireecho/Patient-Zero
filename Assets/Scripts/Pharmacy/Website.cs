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

    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
