using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneSpeaker : MonoBehaviour
{
    
    public SpeakerManager spkrMng;
    [SerializeField] private float timeBetweenTexts;

    private void Start() {
        spkrMng = FindObjectOfType<SpeakerManager>();
    }

    void speakMessage(int i)
    {
        Debug.Log("Spearking" + i);
        spkrMng.Speak(i);
    }

    private void Update() {
        if (Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene("Intro1");
        }
    }
}
