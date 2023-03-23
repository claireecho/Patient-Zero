using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PatientGameplay : MonoBehaviour
{
    private Patient patient;
    private string[] lastNames;
    private string[] firstNames;
    private string[] concerns;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // may need to change path if files change location depending on user
    void Update()
    {
        readFirstNameTextFile(@"C:\Users\emily\Desktop\files\cs\capstone project\ComputerSystem\Assets\Scripts\Patient Information\firstNames.txt");
        readLastNameTextFile(@"C:\Users\emily\Desktop\files\cs\capstone project\ComputerSystem\Assets\Scripts\Patient Information\lastNames.txt");
        readConcernTextFile(@"C:\Users\emily\Desktop\files\cs\capstone project\ComputerSystem\Assets\Scripts\Patient Information\concerns.txt");
    }

    // reads first name text file and assigns randome first name to patient
    void readFirstNameTextFile(string file_path)
    {
        StreamReader inp_stm = new StreamReader(file_path);
        while(!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine( );
            Debug.Log(inp_ln);
            // Do Something with the input. 
        }
        inp_stm.Close( );  
    }

    // reads last name text file and assigns random last name to patient
    void readLastNameTextFile(string file_path)
    {
        StreamReader inp_stm = new StreamReader(file_path);
        while(!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine( );
            Debug.Log(inp_ln);
            // Do Something with the input. 
        }
        inp_stm.Close( );  
    }

    // reads concerns text file and assigns random concern to patient
    void readConcernTextFile(string file_path)
    {
        StreamReader inp_stm = new StreamReader(file_path);
        while(!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine( );
            Debug.Log(inp_ln);
            // Do Something with the input. 
        }
        inp_stm.Close( );  
    }
    
}
