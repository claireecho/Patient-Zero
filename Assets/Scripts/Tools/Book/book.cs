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
    private int page = 2;
    [SerializeField] private int numberOfDOnPage = 4;

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

    private void forward() { // next page of diagnoses book
        // this is confusing the shit out of me and it's late so do tmrw
    }

    private void back() { // previous page of diagnoses book
        // this is confusing the shit out of me and it's late so do tmrw
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
        if (translucent != null) {
            translucent.SetActive(false);
        }
        if (backButtonObject != null) {
            backButtonObject.SetActive(false);
        }
        if (forwardButtonObject != null) {
            forwardButtonObject.SetActive(false);
        }
        
    }
    void showBook() {
        
        CameraLook.isPaused = true;
        setPosition(bookOpen, openPosition);
        setPosition(bookClosed, hidePosition);
        leftText.SetText(PatientGameplay.toString(numberOfDOnPage, 1));
        rightText.SetText(PatientGameplay.toString(numberOfDOnPage, 2));
        if (translucent != null) {
            translucent.SetActive(true);
        }
        backButtonObject.SetActive(true);
        forwardButtonObject.SetActive(true);
        
    }

    void setPosition(GameObject x, GameObject y) {
        x.transform.position = y.transform.position;
        x.transform.rotation = y.transform.rotation;
    }

}
