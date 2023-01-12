using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Menu : MonoBehaviour
{
    public bool paused = false;
    public GameObject blackOverlay;
    public GameObject resumeButton;
    public GameObject quitButton;
    public void QuitGame () {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void ResumeGame(){
        paused = false;
        Time.timeScale = 1.0f;
        blackOverlay.SetActive(false);
        resumeButton.SetActive(false);
        quitButton.SetActive(false);
    }

    private void Update() {
        Debug.Log("Check");
        if (Input.GetButtonDown("Cancel"))
        {
            Debug.Log("Paused button is pressed");
            if (paused == false)
            {
                paused = true;
                Time.timeScale = 0.0f;
                blackOverlay.SetActive(true);
                resumeButton.SetActive(true);
                quitButton.SetActive(true);
            }
            else
            {
                paused = false;
                Time.timeScale = 1.0f;
                blackOverlay.SetActive(false);
                resumeButton.SetActive(false);
                quitButton.SetActive(false);
            }
        }
    }
}
