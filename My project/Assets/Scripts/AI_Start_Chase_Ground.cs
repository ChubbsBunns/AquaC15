using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Start_Chase_Ground : MonoBehaviour
{
    public Transform nextTarget;
    public AI_Start_Chase_Ground nextStart;
    public Ground_Enemy_AI enemy;
    public int textNo = -1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            nextTarget = other.gameObject.GetComponent(typeof(Transform)) as Transform;
            enemy.ai.SetTarget(nextTarget);
            if(textNo >= 0) { SpeakerManager.instance.Speak(textNo); textNo = -1; }
            nextStart.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
