using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class book : MonoBehaviour
{
    public GameObject closedPosition;
    public GameObject openPosition;
    public GameObject bookOpen;
    public GameObject bookClosed;
    public GameObject hidePosition;
    private bool isOpen = false;

    void OnDisable() {
        hideBook();
        isOpen = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        hideBook();
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
        
    }
    void showBook() {
        
        setPosition(bookOpen, openPosition);
        setPosition(bookClosed, hidePosition);
        
    }

    void setPosition(GameObject x, GameObject y) {
        x.transform.position = y.transform.position;
        x.transform.rotation = y.transform.rotation;
    }

}
