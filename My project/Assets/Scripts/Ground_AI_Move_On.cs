using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_AI_Move_On : MonoBehaviour
{
    public Transform nextTarget;
    public Ground_AI_Move_On nextStop;
    public Ground_Enemy_AI ai;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Ground_Enemy_AI otherai = other.GetComponent<Ground_Enemy_AI>();
        if(otherai.Equals(ai))
        {
            ai.StopFollowing();
            ai.SetTarget(nextTarget);
            nextStop.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}

