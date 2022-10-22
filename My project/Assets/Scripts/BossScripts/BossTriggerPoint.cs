using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggerPoint : MonoBehaviour
{
    public BossStateMachine bossStateMachine;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bossStateMachine.StartFight();
            gameObject.SetActive(false);
        }
    }
}
