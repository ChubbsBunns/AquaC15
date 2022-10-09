using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed;
    public int currentPointIndex;
    public bool move;
    public float timeToWaitToMove;

    public int damageIDealToPlayer;
    // Start is called before the first frame update
    private void Awake()
    {
        transform.position = patrolPoints[0].position;
    }

    void Start()
    {
        StartCoroutine(NowYouMove());
    }

    // Update is called once per frame
    void Update()
    {
        if (move == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
            if (transform.position == patrolPoints[currentPointIndex].position)
            {
                if (currentPointIndex + 1 < patrolPoints.Length)
                {
                    currentPointIndex++;
                }
                else
                {
                    currentPointIndex = 0;
                }
            }
        }
    }

    IEnumerator NowYouMove()
    {
        yield return new WaitForSeconds(timeToWaitToMove);
        move = true;
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
