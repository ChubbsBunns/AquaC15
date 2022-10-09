using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public PlayerController_2 player;
    public int swordAttackDamage;
    public Collider2D SwordCollider;
    // Start is called before the first frame update
    void Start()
    {
        swordAttackDamage = player.playerAttackDamage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.enemyTakeDamage(swordAttackDamage);
            Debug.Log("this is working 1");
        }
        else if (collision.CompareTag("TestDummy"))
        {
            TestDummy enemyHealth = collision.gameObject.GetComponent<TestDummy>();
            enemyHealth.enemyTakeDamage(swordAttackDamage);
            Debug.Log("this is working 1");
        }
    }

    public void ChangeColliderSword()
    {
        player.SwordColliderChange();
    }
  
}
