using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechController : MonoBehaviour
{
    [SerializeField] private SpeakerManager speakerManager;
    [SerializeField] private int speechICall = 0;

    private void Start() {
        speakerManager = FindObjectOfType<SpeakerManager>();

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            speakerManager.Speak(speechICall);
            gameObject.SetActive(false);
        }
    }
}
