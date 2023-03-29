using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Website : MonoBehaviour
{

    public GameObject item;
    private GameObject[] items;
    public static List<string> antibiotics;
    private bool isAntibioticsLoaded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (!isAntibioticsLoaded && antibiotics.Length != 0) {
        //     item.transform.GetChild(0).gameObject;
        // }
    }
}
