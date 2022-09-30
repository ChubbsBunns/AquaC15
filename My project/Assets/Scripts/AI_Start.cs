using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Start : MonoBehaviour
{
    public Transform nextTarget;
    public AI_Start nextStart;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Friend_AI.ai.SetTarget(nextTarget);
            nextStart.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
