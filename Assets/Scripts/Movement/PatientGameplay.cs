using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PatientGameplay : MonoBehaviour
{
    private Patient patient;
    public 
    private string[] lastNames;
    private string[] firstNames;
    private string[] concerns;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ReadString();
    }

    // read text file at runtime 
    //[MenuItem("Tools/Write file")]
    static void ReadString()
    {
        string path =  "Assets/Scripts/Patient\ Information/firstNames.txt";
        // Read the text from directly from the txt file
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }

}
