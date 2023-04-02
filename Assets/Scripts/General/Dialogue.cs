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

    // Start is called before the first frame update
    void Start()
    {
        speakerText = Canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        dialogueText = Canvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        Canvas.SetActive(false);
        nextArrow = Canvas.transform.GetChild(3).GetComponent<Button>();
        nextArrow.onClick.AddListener(() => {
            if (dialogueIndex < dialogue.GetLength(0)-1) {
                dialogueIndex++;
                speakerText.SetText(dialogue[dialogueIndex, 0]);
                dialogueText.SetText(dialogue[dialogueIndex, 1]);
            } else {
                CameraLook.isPaused = false;
                Canvas.SetActive(false);
                Inventory.inventory[Inventory.selection].SetActive(true);
                DialogueIsPlaying = false;
                dialogueIndex = 0;
                Inventory.isShowingDescription = true;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.queueDialogue) {
            CameraLook.isPaused = true;
            Canvas.SetActive(true);
            Inventory.inventory[Inventory.selection].SetActive(false);

            DialogueIsPlaying = true;
            PlayerMovement.queueDialogue = false;
        }
    }
}
