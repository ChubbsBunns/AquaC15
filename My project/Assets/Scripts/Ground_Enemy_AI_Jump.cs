using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Enemy_AI_Jump : MonoBehaviour
{
    public bool jumpLeft;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Ground_Enemy_AI ai = other.GetComponent<Ground_Enemy_AI>();
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
