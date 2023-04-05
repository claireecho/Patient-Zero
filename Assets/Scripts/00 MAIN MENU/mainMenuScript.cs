using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenuScript : MonoBehaviour
{

    public Button startButton;
    public Button exitButton;
    public Button settingButton;
    public AudioSource audioSource;

    public void startGame() {
        SceneManager.LoadScene("SampleScene");
    }

    public void changeScene(string sceneName) {
        audioSource.Play();
        SceneManager.LoadScene(sceneName);
    }

    public void Exit() {
        audioSource.Play();
        Application.Quit();
    }

    public void Awake() {
        Cursor.visible = true;
        startButton.onClick.AddListener(startGame);
        exitButton.onClick.AddListener(Exit);
    }

}
