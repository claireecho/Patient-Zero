using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tv : MonoBehaviour
{

    public GameObject canvas;
    public static Image image;
    public Sprite[] sprites = new Sprite[5];

    // Start is called before the first frame update
    void Awake()
    {
        image = canvas.transform.GetChild(0).GetComponent<Image>();
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (monitor.isInSurgery) {
            canvas.SetActive(true);
            image.sprite = sprites[surgery.step - 1];
        } else {
            canvas.SetActive(false);
        }



    }

}
