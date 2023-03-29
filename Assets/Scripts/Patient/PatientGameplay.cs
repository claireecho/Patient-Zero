using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PatientGameplay : MonoBehaviour
{
    public static Patient patient;
    public static string[] lastNames;
    public static string[] firstNames;
    public static string[] sexes;
    public static string[] diagnoses;
    public static string[] concerns;
    public static string[] treatments;
    public GameObject officeSpawn;
    public GameObject surgerySpawn;
    public static bool isCurrentPatient = false;

    // Start is called before the first frame update
    // may need to change path if files change location depending on user
    void Start()
    {
        patient = new Patient("N/A", "", "N/A", "N/A", "N/A", "N/A");

        // read in files
        firstNames = File.ReadAllLines("Assets/Scripts/Patient/Patient Information/firstNames.txt");
        sexes = new string[firstNames.Length];
        for (int i = 0; i < firstNames.Length; i++)
        {
            sexes[i] = firstNames[i].Substring(firstNames[i].Length-1);
            firstNames[i] = firstNames[i].Substring(0, firstNames[i].Length-2);
        }
        lastNames = File.ReadAllLines("Assets/Scripts/Patient/Patient Information/lastNames.txt");
        diagnoses = File.ReadAllLines("Assets/Scripts/Patient/Patient Information/diagnoses.txt");
        string[] temp = File.ReadAllLines("Assets/Scripts/Patient/Patient Information/concerns.txt");
        concerns = new string[diagnoses.Length];
        int index = 0;
        for (int i = 0; i < diagnoses.Length; i++)
        {
            for (int j = 0; j < int.Parse(diagnoses[i].Substring(diagnoses[i].Length-1)); j++)
            {                
                concerns[i] += temp[index] + "\n";
                index++;
            }
            diagnoses[i] = diagnoses[i].Substring(0, diagnoses[i].Length-2);
        }
        treatments = File.ReadAllLines("Assets/Scripts/Patient/Patient Information/treatments.txt");
        Website.antibiotics = new List<string>();
        for (int i = 0; i < diagnoses.Length; i++)
        {
            if (treatments[i] == "Antibiotics") {
                Website.antibiotics.Add(diagnoses[i]);
            }
        }

        book.maxPage = diagnoses.Length / book.numberOfDOnPage + (diagnoses.Length % book.numberOfDOnPage == 0 ? 0 : 1);

    }

    // Update is called once per frame
    void Update()
    {
        // Creates a new patient if the player has grabbed a patient
        if (PlayerMovement.grabbedPatient) {
            PlayerMovement.grabbedPatient = false;
            int randomIndex = Random.Range(0, diagnoses.Length);
            int randomIndex2 = Random.Range(0, firstNames.Length);
            patient = new Patient(firstNames[randomIndex2], lastNames[Random.Range(0, lastNames.Length)], sexes[randomIndex2], diagnoses[randomIndex], concerns[randomIndex], treatments[randomIndex]);
            while (patient.getSex() == "M" && patient.getDiagnosis() == "Yeast Infection") {
                randomIndex = Random.Range(0, diagnoses.Length);
                randomIndex2 = Random.Range(0, firstNames.Length);
                patient = new Patient(firstNames[randomIndex2], lastNames[Random.Range(0, lastNames.Length)], sexes[randomIndex2], diagnoses[randomIndex], concerns[randomIndex], treatments[randomIndex]);
            }
            isCurrentPatient = true;
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            if (PlayerMovement.canGrabPatient) {
                gameObject.transform.position = officeSpawn.transform.position;
                gameObject.transform.rotation = officeSpawn.transform.rotation;
                // transform.Rotate(0, 10f, 0);
            } else if (PlayerMovement.canEnterSurgery && PlayerMovement.isOrderOut) {
                gameObject.transform.position = surgerySpawn.transform.position;
                gameObject.transform.rotation = surgerySpawn.transform.rotation;
            }
        }

    }


    // reads first name text file and adds to array
    void readTextFile(string file_path, string[] array)
    {
        StreamReader inp_stm = new StreamReader(file_path);
        int val = 0;
        while(!inp_stm.EndOfStream)
        {
            string currentLine = inp_stm.ReadLine( );
            val++;
            array[val] = currentLine;
            Debug.Log(array[val]);
        }
        inp_stm.Close( );  
    }

    public static string toString(int x, int y) { // x is number of texts, y is page number
        string temp = "";
        for (int i = x * (y - 1); i < (x * (y - 1) + x < diagnoses.Length ? x * (y - 1) + x : diagnoses.Length); i++) {
            string[] cTemp = concerns[i].Split('\n');
            string CTemp = "";
            foreach (string j in cTemp) {
                if (j != "") 
                    CTemp += "  - " + j + "\n";
            }
            temp += "Sickness: " + diagnoses[i] + "\n" +
                    "Symptoms: \n" + CTemp +
                    "Treatment: " + treatments[i] + "\n\n";
            // Sickness: Chicken Pox
            // Symptoms: Itching, Fever, Rash
            // Treatment: Rest, Medicine
        }
        return temp;
    }
    

}
