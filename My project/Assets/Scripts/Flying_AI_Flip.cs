using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying_AI_Flip : MonoBehaviour
{
    [SerializeField] Flying_AI ai;
    public bool isFlip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Flying_AI ai = other.GetComponent<Flying_AI>();
        if (ai && isFlip) {
            ai.Flip();
        }
    }
}
