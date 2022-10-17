using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIJump : MonoBehaviour
{
    public bool jumpLeft;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Friend_AI ai = other.GetComponent<Friend_AI>();
        if(ai)
        {
            if(jumpLeft && ai.rb.velocity.x < -0.05f)
            {
                ai.Jump();
            }
            if(!jumpLeft && ai.rb.velocity.x > 0.05f)
            {
                ai.Jump();
            }
        }
    }
}
