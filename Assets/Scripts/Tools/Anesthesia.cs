using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anesthesia : MonoBehaviour
{
    public GameObject position;
    // Start is called before the first frame update
    void Awake()
    {
        transform.position = position.transform.position;
        transform.rotation = position.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
