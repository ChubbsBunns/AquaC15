using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionForBoss : MonoBehaviour
{
    public Animator bossKingAnim;
    public BossKing bossKing;
    private void Start()
    {
        bossKing = FindObjectOfType<BossKing>();
        bossKingAnim = bossKing.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bossKingAnim.SetBool("Start", true);

        }
    }
}
