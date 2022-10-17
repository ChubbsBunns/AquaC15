using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Stop : MonoBehaviour
{
    public bool faceLeft;
    public AI_Stop nextStop;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Friend_AI ai = other.GetComponent<Friend_AI>();
        if(ai)
        {
            ai.StopFollowing();
            ai.rb.velocity = new Vector3(0f,ai.rb.velocity.y);
            ai.FaceDirection(faceLeft);
            nextStop.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
