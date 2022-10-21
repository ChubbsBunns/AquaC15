using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Flying_AI : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed;
    public int currentPointIndex;
    public bool move;
    public float timeToWaitToMove;

    public int maxPoint;

    public bool followPlayer = false;

    public Rigidbody2D rb;

    [SerializeField] bool facingRight = false;

    public int damageIDealToPlayer;
    // Start is called before the first frame update
    private void Awake()
    {
        transform.position = patrolPoints[0].position;
    }

    void Start()
    {
        if (followPlayer)
        {
            Player_Controller_1 player = FindObjectOfType<Player_Controller_1>();
            patrolPoints[1] = player.transform;
        }


        maxPoint = patrolPoints.Length;
        rb = GetComponent<Rigidbody2D>();
        //StartCoroutine(NowYouMove());
        move = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (move == true)
        {

            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
            if ((Math.Abs(transform.position.x - patrolPoints[currentPointIndex].position.x) <= 0.1) 
                && (Math.Abs(transform.position.y - patrolPoints[currentPointIndex].position.y) <= 0.1) )
            {
                Debug.Log("Transform Position is equal to patrol points");
                if (currentPointIndex + 1 < patrolPoints.Length)
                {
                    if (currentPointIndex == 0)
                    {
                        Flip();
                    }
                    currentPointIndex++;
                }
                else
                {
                    Flip();
                    currentPointIndex = 0;
                }
            }
            else
            {
                //Debug.Log(transform.position.x);
                //Debug.Log(patrolPoints[currentPointIndex].position.x);
            }
        }
        
    }

    void speedUp() 
    {
        if (move == true) {
            speed = speed * 2;
        }
    }

    void speedDown() {
        if (move == true) {
            speed = speed / 2;
        }
    }

    IEnumerator NowYouMove()
    {
        yield return new WaitForSeconds(timeToWaitToMove);
        move = true;
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.tag == "Player")
    //     {
    //         PlayerController_2 playerGameObject = collision.gameObject.GetComponent<PlayerController_2>();
    //         playerGameObject.TakeDamage(damageIDealToPlayer);
    //     }
    // }

    // private void OnTriggerStay2D(Collider2D collision)
    // {
    //     if (collision.tag == "Player")
    //     {
    //         PlayerController_2 playerGameObject = collision.gameObject.GetComponent<PlayerController_2>();
    //         playerGameObject.TakeDamage(damageIDealToPlayer);
    //     }
    // }
    //flipping
    public void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }
}
