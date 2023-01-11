using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public void PlayGame () {
        SceneManager.LoadScene("0 IntroCutscene");
    }

    public void QuitGame () {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
