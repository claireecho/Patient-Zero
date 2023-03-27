using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class book : MonoBehaviour
{
    public GameObject closedPosition;
    public GameObject openPosition;
    public GameObject bookOpen;
    public GameObject bookClosed;
    public GameObject hidePosition;
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;
    public GameObject translucent;
    private bool isOpen = false;
    public GameObject backButtonObject;
    public GameObject forwardButtonObject;
    public Button backButton;
    public Button forwardButton;
    private int[] page = {1, 2};
    private int numberOfDOnPage = 4;

    void OnDisable() {
        hideBook();
        isOpen = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        hideBook();
        backButton.onClick.AddListener(back);
        forwardButton.onClick.AddListener(forward);
        
    }

    private void forward() {
        page[0] += 2;
        page[1] += 2;
        if (page[1] > PatientGameplay.diagnoses.Length / numberOfDOnPage + 1) {
            page[0] -= 2;
            page[1] -= 2;
        }
        leftText.SetText(PatientGameplay.toString(numberOfDOnPage, page[0]));
        rightText.SetText(PatientGameplay.toString(numberOfDOnPage, page[1]));
    }

    private void back() {
        page[0] -= 2;
        page[1] -= 2;
        if (page[0] < 1) {
            page[0] += 2;
            page[1] += 2;
        }
        leftText.SetText(PatientGameplay.toString(numberOfDOnPage, page[0]));
        rightText.SetText(PatientGameplay.toString(numberOfDOnPage, page[1]));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (isOpen) {
                hideBook();
                isOpen = false;
            } else {
                showBook();
                isOpen = true;
            }
        }
    }

    void hideBook() {
        
        setPosition(bookOpen, hidePosition);
        setPosition(bookClosed, closedPosition);
        leftText.SetText("");
        rightText.SetText("");
        CameraLook.isPaused = false;
        translucent.SetActive(false);
        backButtonObject.SetActive(false);
        forwardButtonObject.SetActive(false);
        
    }
    void showBook() {
        
        CameraLook.isPaused = true;
        setPosition(bookOpen, openPosition);
        setPosition(bookClosed, hidePosition);
        leftText.SetText(PatientGameplay.toString(numberOfDOnPage, 1));
        rightText.SetText(PatientGameplay.toString(numberOfDOnPage, 2));
        translucent.SetActive(true);
        backButtonObject.SetActive(true);
        forwardButtonObject.SetActive(true);
        
    }

    void setPosition(GameObject x, GameObject y) {
        x.transform.position = y.transform.position;
        x.transform.rotation = y.transform.rotation;
    }

}
