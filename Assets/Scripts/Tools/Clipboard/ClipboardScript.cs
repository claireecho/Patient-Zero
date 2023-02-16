using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipboardScript : MonoBehaviour
{

    public GameObject restPosition;
    public GameObject secondPosition;
    private bool isBeingUsed = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = restPosition.transform.position;
        transform.rotation = restPosition.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!isBeingUsed) {
                transform.position = secondPosition.transform.position;
                transform.rotation = secondPosition.transform.rotation;
                isBeingUsed = true;
            } else {
                transform.position = restPosition.transform.position;
                transform.rotation = restPosition.transform.rotation;
                isBeingUsed = false;
            }
        }
    }
}
