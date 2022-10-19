using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying_AI_Trigger : MonoBehaviour
{
    [SerializeField] Flying_AI ai;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ai.move = true;
        }
        Debug.Log("This isnt a player");
    }
}
