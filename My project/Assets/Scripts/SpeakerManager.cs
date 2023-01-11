using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeakerManager : MonoBehaviour
{
    public static SpeakerManager instance;
    public Canvas canvas;
    public string[] texts;
    public Text text;
    public float timeBeforeRemove;
    private float time;
    bool allowSpeak = true;

    private void Start()
    {
        instance = this;
        Speak(0);
    }

    private void Update()
    {
        if(time < timeBeforeRemove)
        {
            time += Time.deltaTime;
            if(time > timeBeforeRemove)
            {
                canvas.enabled = false;
            }
        }
    }

    public void Speak(int textNo)
    {
        Debug.Log("Allow speak is " + allowSpeak);
        if (!allowSpeak) { return; }
        text.text = texts[textNo].Replace(';','\n');
        canvas.enabled = true;
        time = 0;
    }

    public void StopSpeak()
    {
        allowSpeak = false;
    }

}
