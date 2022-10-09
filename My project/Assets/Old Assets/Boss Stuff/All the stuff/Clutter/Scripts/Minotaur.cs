using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : MonoBehaviour
{
    public int damageIDealToPlayer;

    public Animator anim;
    public minotaur_legs legs;
    public Rigidbody2D rb;
    //states
    [Header("States")]
    public bool phase1;
    public bool phase2;

    public int phase1Index;
    public int phase2Index;

    //attack 1
    [Header("Attack 1")]
    public Transform[] patrolPoints;
    public float speed;
    public int currentPointIndex;

    [Header("Attack 2")]
    public Transform MidCheck;
    public Transform leftCheck;
    public Transform rightCheck;
    public bool isChargingLeft;
    public bool isChargingRight;
    public float chargeSpeed;

    public PlayerController_2 player;

    public int attackIndex;

    

    //attack indexes:
    // 1 == charge
    // 2 == shockwave attack
    // 3 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        legs = FindObjectOfType<minotaur_legs>();
        player = FindObjectOfType<PlayerController_2>();
        attackIndex = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void FixedUpdate()
    {
        if (isChargingLeft)
        {
            transform.position = Vector2.MoveTowards(transform.position, leftCheck.position, chargeSpeed);
        }
        else if (isChargingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, rightCheck.position, chargeSpeed);
        }
    }

    public void AngeryBoiAttack()
    {
        anim.SetBool("AngeryBoi", true);
        
    }


    public void ChooseAttack()
    {
        if (attackIndex == 1)
        {
            attackIndex = 2;
        }
        else if (attackIndex == 2)
        {
            attackIndex = 1;
        }
        if (attackIndex == 1)
        {
            anim.SetBool("ChargeAttackGo", true);
            anim.SetBool("ShockwaveAttackGo", false);
        }
        else
        {
            anim.SetBool("ShockwaveAttackGo", true);
            anim.SetBool("ChargeAttackGo", false);
        }
    }

    public void Attack_Charge()
    {
        if (transform.position.x > MidCheck.position.x)
        {
            isChargingLeft = true;
            isChargingRight = false;
        }
        else if (transform.position.x < MidCheck.position.x)
        {
            isChargingRight = true;
            isChargingLeft = false;
        }    
    }

    public void poof_here()
    {
        legs.InstantiatePoofHere();
    }

    public void setShockwaveToFalse()
    {
        anim.SetBool("ShockwaveAttackGo", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController_2 playerGameObject = collision.gameObject.GetComponent<PlayerController_2>();
            playerGameObject.TakeDamage(damageIDealToPlayer);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController_2 playerGameObject = collision.gameObject.GetComponent<PlayerController_2>();
            playerGameObject.TakeDamage(damageIDealToPlayer);
        }
    }
}
