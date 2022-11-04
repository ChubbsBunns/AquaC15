using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AI_Start_Chase_Flying : MonoBehaviour
{
    public Transform nextTarget;
    public AI_Start_Chase_Flying nextStart;
    public Flying_Enemy_AI enemy;
    public int textNo = -1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemy.findPlayer = true;
            nextTarget = other.gameObject.GetComponent<Player_Controller_1>().PlayerCenter;
            if(textNo >= 0) { SpeakerManager.instance.Speak(textNo); textNo = -1; }
            nextStart.gameObject.SetActive(true);
        }
    }
}
