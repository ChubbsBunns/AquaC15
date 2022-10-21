using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_AI_Start : MonoBehaviour
{
    public Transform nextTarget;
    public Ground_Enemy_AI ai;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ai.SetTarget(nextTarget);
            gameObject.SetActive(false);
        }
    }
}
