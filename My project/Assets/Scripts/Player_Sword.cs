using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Sword : MonoBehaviour
{
    public Collider2D sword_collider;
    public Player_Controller_1 player;

    public int player_attack_damage;

    public Animator sword_anim;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent.CompareTag("Player"))
        {
            player = transform.parent.GetComponent<Player_Controller_1>();
        }
        else
        {
            Debug.LogError("Parent of Player object is " + transform.parent.tag);
        }
        sword_collider = GetComponent<Collider2D>();

        sword_anim = GetComponent<Animator>();
        SetSwordToNotAttack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("I have hit an enemy");
//            Debug.Log("HIJNFSFD");
            Enemy_Health enemy_health = collision.gameObject.GetComponent<Enemy_Health>();
            if (enemy_health == null)
            {
                Debug.Log("Enemy health cant be found");
            }
            enemy_health.Enemy_Take_Damage(player_attack_damage);
        }
    }

    public void StartUpAttack()
    {
        sword_anim.SetBool("UpAttackSword", true);
    }

    public void StartDownAttack()
    {
        sword_anim.SetBool("DownAttackSword", true);
    }

    //this attack is for horizontal attacks
    public void StartHorizontalAttack()
    {
        sword_anim.SetBool("AttackSword", true);
    }

    public void SetSwordToAttack()
    {
        sword_collider.enabled = true;
    }

    public void SetSwordToNotAttack()
    {
        sword_collider.enabled = false;
        sword_anim.SetBool("AttackSword", false);
        sword_anim.SetBool("UpAttackSword", false);
        sword_anim.SetBool("DownAttackSword", false);
    }
}
