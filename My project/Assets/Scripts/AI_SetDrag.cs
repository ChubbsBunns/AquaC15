using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_SetDrag : MonoBehaviour
{
    public float drag;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Friend_AI ai = other.GetComponent<Friend_AI>();
        if(ai)
        {
            ai.rb.drag = drag;
        }
    }
}
