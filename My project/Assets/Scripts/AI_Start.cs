using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Start : MonoBehaviour
{
    public Transform nextTarget;
    public AI_Start nextStart;
    public int textNo = -1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Friend_AI.ai.SetTarget(nextTarget);
            if(textNo >= 0) { SpeakerManager.instance.Speak(textNo); textNo = -1; }
            nextStart.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
