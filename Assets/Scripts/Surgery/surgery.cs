using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class surgery : MonoBehaviour
{

    public GameObject canva;
    public static TextMeshProUGUI canvaText;
    public static bool canInteractWithPatient = false;
    public static int step = 0;

    // step 2
    public GameObject scalpelCanvas;
    public static bool isUsingScalpel = false;
    RectTransform scalpel;
    RectTransform cut;
    public int marginSize = 355;
    int maxSize;
    float percentage = 0;

    // step 3
    public GameObject tweezersCanvas;
    public static RectTransform bar;
    public static RectTransform loading;
    public static TextMeshProUGUI loadingText;
    public float time;
    public GameObject affliction;
    public static bool isUsingTweezers = false;

    // step 5
    public GameObject sutureCanvas;
    Image sutureBar;
    RectTransform suture;
    public static bool isUsingSuture = false;
    int startPositionX;
    public int marginSizeSuture;
    int maxSizeSuture;
    
    Collider interactWithPatientCollider;
    public AudioClip completeSound;
    public AudioSource audioSource;

    public static bool isSurgeryComplete = false;

    // Start is called before the first frame update
    void Awake()
    {
        interactWithPatientCollider = GameObject.Find("interactWithPatient").GetComponent<Collider>();
        interactWithPatientCollider.enabled = true;

        canvaText = canva.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        bar = tweezersCanvas.transform.GetChild(0).GetComponent<RectTransform>();
        loading = tweezersCanvas.transform.GetChild(1).GetComponent<RectTransform>();
        loadingText = tweezersCanvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        scalpel = scalpelCanvas.transform.GetChild(3).GetComponent<RectTransform>();
        cut = scalpelCanvas.transform.GetChild(2).GetComponent<RectTransform>();
        maxSize = (int)cut.rect.width;

        scalpelCanvas.SetActive(false);
        canva.SetActive(false);
        affliction.SetActive(false);
        tweezersCanvas.SetActive(false);
    }

    void Start() {
        sutureBar = sutureCanvas.transform.GetChild(3).GetComponent<Image>();
        suture = sutureCanvas.transform.GetChild(4).GetComponent<RectTransform>();

        sutureCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Initialize surgery------------
        if (PlayerMovement._inSurgery) {
            StopCoroutine("showTitle");
            StartCoroutine("showTitle");
            step = 1;
            // inventory to turn into surgeryTools
            Inventory.inventory = new GameObject[Inventory.globalSurgeryTools.Length];
            for (int i = 0; i < Inventory.globalSurgeryTools.Length; i++) {
                Inventory.inventory[i] = Inventory.globalSurgeryTools[i];
            }
            PlayerMovement._inSurgery = false;

        }

        // During surgery----------------

        // player has to follow procedure

        // step 1: anesthesia
        if (Input.GetKeyDown(KeyCode.E)) {
            if (canInteractWithPatient) {
                switch (step) {
                    case 1:
                        step1(Inventory.inventory[Inventory.selection].name);
                        break;
                    case 2:
                        percentage = 0;
                        step2(Inventory.inventory[Inventory.selection].name);
                        break;
                    case 3: // step 3: remove affliction
                        if (!isUsingTweezers) {
                            step3(Inventory.inventory[Inventory.selection].name);
                        }
                        break;
                    case 4: // step 4: throw away affliction
                        // did so using TrashSurgery.cs
                        break;
                    case 5: // step 5: suture
                        if (!isUsingSuture) {
                            percentage = 0;
                            step5(Inventory.inventory[Inventory.selection].name);
                        }
                        break;
                    default:
                        Debug.Log("error");
                        break;
                }
            }
        }
        // step 2: cut open patient
        if (isUsingScalpel) {
            scalpel.anchoredPosition = new Vector2(-577 + (percentage * (maxSize-marginSize)), -54);
            cut.sizeDelta = new Vector2(maxSize * percentage, cut.rect.height);
            if (Input.GetKeyDown(KeyCode.Space)) {
                percentage += 0.1f;
            }
            if (percentage >= 1) {
                isUsingScalpel = false;
                scalpelCanvas.SetActive(false);
                CameraLook.isPaused = false;
                interactWithPatientCollider.enabled = true;
                Inventory.selection = 0;
                Inventory.inventory[Inventory.selection].SetActive(true);
                step++;
                playSound(completeSound);
            }

        }

        if (isUsingSuture) {
            suture.anchoredPosition = new Vector2(584 - (percentage * marginSizeSuture), 9);
            sutureBar.fillAmount = 1f - percentage;
            if (Input.GetKeyDown(KeyCode.Space)) {
                percentage += 0.1f;
                Debug.Log(percentage);
            }
            if (percentage >= 1) {
                isUsingSuture = false;
                sutureCanvas.SetActive(false);
                CameraLook.isPaused = false;
                interactWithPatientCollider.enabled = true;
                Inventory.selection = 0;
                Inventory.inventory[Inventory.selection].SetActive(true);
                if (PatientGameplay.patient.getTreatment() == "Surgery" && PatientGameplay.patient.getDiagnosis() == ClipboardScript.diagnosisText.text) {
                    PatientGameplay.patient.Success();
                    gameObject.transform.position = PlayerMovement.globalOfficeSpawn.transform.position;
                    transform.rotation = Quaternion.Euler(0, -133, 0);
                } else {
                    PatientGameplay.patient.Failure();
                    gameObject.transform.position = PlayerMovement.globalOfficeSpawn.transform.position;
                    transform.rotation = Quaternion.Euler(0, -133, 0);
                }
                Debug.Log(PatientGameplay.patient.getDiagnosis() + " " + ClipboardScript.diagnosisText.text);
                isSurgeryComplete = true;
                PatientGameplay.patientObject.transform.position = PatientGameplay._officeSpawn.transform.position;
                PatientGameplay.patientObject.transform.rotation = PatientGameplay._officeSpawn.transform.rotation;
                PatientGameplay.mask.SetActive(false);
                audioSource.Stop();
            }
        }

        // if player fails, patient dies
    }

    void step1(string tool) {
        Debug.Log(tool);
        if (tool == "Anesthesia Mask") {
            Inventory.inventory[Inventory.selection].SetActive(false);
            GameObject[] newTools = new GameObject[Inventory.inventory.Length-1];
            for (int i = 0; i < Inventory.inventory.Length; i++) {
                if (i < Inventory.selection) {
                    newTools[i] = Inventory.inventory[i];
                } else if (i > Inventory.selection) {
                    newTools[i-1] = Inventory.inventory[i];
                }
            }
            Inventory.selection = 0;
            Inventory.inventory = new GameObject[newTools.Length];
            Inventory.inventory = newTools;
            Inventory.inventory[Inventory.selection].SetActive(true);

            PatientGameplay.mask.SetActive(true);

            step++;
            playSound(completeSound);
        } else if (tool == "Clipboard" || tool == "Affliction") {
            
        } else {
            killedPatient();
        } 
    }

    void step2(string tool) {
        if (tool == "scalpel") {
            interactWithPatientCollider.enabled = false;
            PatientGameplay.cut.SetActive(true);
            Inventory.inventory[Inventory.selection].SetActive(false);
            scalpelCanvas.SetActive(true);
            CameraLook.isPaused = true;
            PlayerMovement.EText.SetText("");
            cut.sizeDelta = new Vector2(0, cut.rect.height);
            isUsingScalpel = true;
            percentage = 0;

            
        } else if (tool == "Clipboard" || tool == "Affliction") {
            
        } else {
            killedPatient();
        } 
    }

    void step3(string tool) {
        if (tool == "tweezers") {
            tweezersCanvas.SetActive(true);
            loading.sizeDelta = new Vector2(0, loading.rect.height);
            StartCoroutine(loadingAffliction(time));

        } else if (tool == "Clipboard" || tool == "Affliction") {
            
        } else {
            killedPatient();
        }
    }

    void step5(string tool) {
        if (tool == "suture") {
            interactWithPatientCollider.enabled = false;
            PatientGameplay.cut.SetActive(false);
            PatientGameplay.stitched.SetActive(true);
            Inventory.inventory[Inventory.selection].SetActive(false);
            sutureCanvas.SetActive(true);
            CameraLook.isPaused = true;
            PlayerMovement.EText.SetText("");
            suture.anchoredPosition = new Vector2(584, 9);
            maxSizeSuture = (int)sutureBar.gameObject.GetComponent<RectTransform>().rect.width;

            startPositionX = (int)suture.anchoredPosition.x;
            isUsingSuture = true;
        } else if (tool == "Clipboard" || tool == "Affliction") {
            
        } else {
            killedPatient();
        } 
    }

    void killedPatient() {
        PatientGameplay.patient.killed();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("interactWithPatient")) {
            canInteractWithPatient = true;
        }
    } 

    void OnTriggerStay(Collider other) {
        if (other.CompareTag("interactWithPatient")) {
            PlayerMovement.EText.color = Color.black;
            switch (Inventory.inventory[Inventory.selection].name) {
                case "Anesthesia Mask":
                    PlayerMovement.EText.SetText("Press E to administer " + Inventory.inventory[Inventory.selection].name);
                    break;
                case "Clipboard":
                    PlayerMovement.EText.SetText("");
                    break;
                case "tweezers":
                    if (isUsingTweezers) {
                        PlayerMovement.EText.SetText("");
                    } else {
                        PlayerMovement.EText.SetText("Press E to use " + Inventory.inventory[Inventory.selection].name);
                    }
                    break;
                case "affliction":
                    PlayerMovement.EText.SetText("");
                    break;
                default:
                    PlayerMovement.EText.SetText("Press E to use " + Inventory.inventory[Inventory.selection].name);
                    break;
            }
                
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("interactWithPatient")) {
            canInteractWithPatient = false;
            PlayerMovement.EText.SetText("");
            StopAllCoroutines();
            tweezersCanvas.SetActive(false);
        }
    }

    /* Surgery Notes
    Monitor will show patient heart rate
        - Flat lines if you don't follow procedure correctly
        - Otherwise, will stay at a steady rate
    TV will show instructions with step-by-step procedure on how to do surgery
    Inventory:
        - Clipboard
        - Anesthesia
        - Scalpel
        - Scissors
        - Forceps
        - Suture
        - Needle
        - Syringe
        - Sponge
        - Bandage
        - Tape
        - Gauze
        - Tongs
        - Tweezer

    */

    IEnumerator showTitle() {

        canva.SetActive(true);
        canvaText.text = "Surgery: " + ClipboardScript.diagnosisText.text;
        CameraLook.isPaused = true;
        Inventory.inventory[Inventory.selection].SetActive(false);
        yield return new WaitForSeconds(3);
        canva.SetActive(false);
        CameraLook.isPaused = false;
        Inventory.selection = 0;
        Inventory.inventory[0].SetActive(true);

    }

    IEnumerator loadingAffliction(float time) {

        isUsingTweezers = true;
        for (int i = 0; i < 10; i ++) {
            yield return new WaitForSeconds(time/10);
            loadingText.text = "Grabbing Affliction";
            for (int j = 0; j < i % 4; j++) {
                loadingText.text += ".";
            }
            loading.sizeDelta = new Vector2((bar.sizeDelta.x / 10) * (i+1), loading.sizeDelta.y);
        }
        tweezersCanvas.SetActive(false);
        Inventory.inventory[Inventory.selection].SetActive(false);
        GameObject[] newTools = new GameObject[Inventory.inventory.Length+1];
        for (int i = 0; i < Inventory.inventory.Length; i++) {
            newTools[i] = Inventory.inventory[i];
        }
        newTools[newTools.Length - 1] = affliction;
        Inventory.inventory = new GameObject[newTools.Length];
        Inventory.selection = Inventory.inventory.Length - 1;
        Inventory.inventory = newTools;
        Inventory.inventory[Inventory.selection].SetActive(true);
        Inventory.isShowingDescription = true;
        step++;
        playSound(completeSound);
        isUsingTweezers = false;
    }

    public void playSound(AudioClip sound) {
        audioSource.Stop();
        audioSource.clip = sound;
        audioSource.Play();
        audioSource.loop = false;
    }


}
