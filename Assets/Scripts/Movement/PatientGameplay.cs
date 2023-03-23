using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PatientGameplay : MonoBehaviour
{
    private Patient patient;
    private string[] lastNames = new string[25];
    private string[] firstNames = new string[43];
    private string[] concerns = new string[25];

    // Start is called before the first frame update
    // may need to change path if files change location depending on user
    void Start()
    {
        readFirstNameTextFile(@"C:\Users\emily\Desktop\files\cs\capstone project\ComputerSystem\Assets\Scripts\Patient Information\firstNames.txt");
        readLastNameTextFile(@"C:\Users\emily\Desktop\files\cs\capstone project\ComputerSystem\Assets\Scripts\Patient Information\lastNames.txt");
        readConcernTextFile(@"C:\Users\emily\Desktop\files\cs\capstone project\ComputerSystem\Assets\Scripts\Patient Information\concerns.txt");
    }

    // Update is called once per frame
    void Update()
    {
    }

    // reads first name text file and adds to array
    void readFirstNameTextFile(string file_path)
    {
        StreamReader inp_stm = new StreamReader(file_path);
        int val = 0;
        while(!inp_stm.EndOfStream)
        {
            string currentLine = inp_stm.ReadLine( );
            val++;
            firstNames[val] = currentLine;
            Debug.Log(firstNames[val]);
        }
        inp_stm.Close( );  
    }

    // reads last name text file and assigns to array
    void readLastNameTextFile(string file_path)
    {
        StreamReader inp_stm = new StreamReader(file_path);
        int val = 0;
        while(!inp_stm.EndOfStream)
        {
            string currentLine = inp_stm.ReadLine( );
            val++;
            lastNames[val] = currentLine;
            Debug.Log(lastNames[val]);
        }
        inp_stm.Close( );  
    }

    // reads concerns text file and assigns to array
    void readConcernTextFile(string file_path)
    {
        StreamReader inp_stm = new StreamReader(file_path);
        int val = 0;
        while(!inp_stm.EndOfStream)
        {
            string currentLine = inp_stm.ReadLine( );
            val++;
            concerns[val] = currentLine;
            Debug.Log(concerns[val]);
        }
        inp_stm.Close( );  
    }
    
}
