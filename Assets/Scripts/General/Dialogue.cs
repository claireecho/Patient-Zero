using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{

    public static Button nextArrow;
    public GameObject Canvas;
    public static string[,] dialogue;
    public static bool DialogueIsPlaying = false;
    public static int dialogueIndex = 0;
    public static TextMeshProUGUI dialogueText;
    public static TextMeshProUGUI speakerText;
    public GameObject confirmCollider;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        speakerText = Canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        dialogueText = Canvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        nextArrow = Canvas.transform.GetChild(3).GetComponent<Button>();
        PatientGameplay.confirmCollider = confirmCollider;
        resetNextArrow();
    }

    void Start() {
        Canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.queueDialogue) {
            startDialogue();
        }

        if (PlayerMovement.ConfirmedWithPatient) {
            confirmCollider.SetActive(false);
            string[,] d = new string[,] {
                {"You", "I'm sorry " + PatientGameplay.patient.getFirstName() + ", but I believe you have [" + ClipboardScript.dropdown.captionText.text + "]."},
                {PatientGameplay.patient.getFirstName(), "Oh... what should I do?"},
                {"You", "Don't worry " + PatientGameplay.patient.getFirstName() + ". I will treat you! Please wait here while I do so."}
            };
            setDialogue(d);
            startDialogue();
        }

        // Patient received antibiotics-------------------
        if (PlayerMovement.isPillsOut) {    
            if (PatientGameplay.patient.getTreated() == "YES") {
                setDialogue(new string[,] {
                    {"You", "Here you go " + PatientGameplay.patient.getFirstName() + ". I hope you feel better soon!"},
                    {PatientGameplay.patient.getFirstName(), "Thank you so much! I feel so much better now!"},
                    {"You", "I'm glad to hear that!"}
                });
                startDialogue();
            } else if (PatientGameplay.patient.getTreated() == "NO") {
                setDialogue(new string[,] {
                    {"You", "Here you go " + PatientGameplay.patient.getFirstName() + ". I hope you feel better soon!"},
                    {PatientGameplay.patient.getFirstName(), "..."},
                });
                startDialogue();
            }
        }

        // Can use space to skip through dialogue
        if (DialogueIsPlaying && Input.GetKeyDown(KeyCode.Space)) {
            nextArrow.onClick.Invoke();
        }

    }

    public void resetNextArrow() {
        nextArrow.onClick.RemoveAllListeners();
        nextArrow.onClick.AddListener(() => {
            if (dialogueIndex < dialogue.GetLength(0) - 1) {
                dialogueIndex++;
                speakerText.SetText(dialogue[dialogueIndex, 0]);
                dialogueText.SetText(dialogue[dialogueIndex, 1]);
            } else {
                dialogueIndex = 0;
                CameraLook.isPaused = false;
                Canvas.SetActive(false);
                Inventory.inventory[Inventory.selection].SetActive(true);
                DialogueIsPlaying = false;
                Inventory.isShowingDescription = true;
                if (PatientGameplay.patient.getTreated() != "") {  
                    PatientGameplay.isCompleted = true;
                }
            }
        });
    }

    public static void setDialogue(string[,] d) {
        dialogue = d;
        speakerText.SetText(dialogue[dialogueIndex, 0]);
        dialogueText.SetText(dialogue[dialogueIndex, 1]);
    }

    public void startDialogue() {
        resetNextArrow();
        CameraLook.isPaused = true;
        Canvas.SetActive(true);
        audioSource.Stop();
        audioSource.Play();
        Inventory.inventory[Inventory.selection].SetActive(false);

        DialogueIsPlaying = true;
        PlayerMovement.queueDialogue = false;
        PlayerMovement.ConfirmedWithPatient = false;

    }

}
