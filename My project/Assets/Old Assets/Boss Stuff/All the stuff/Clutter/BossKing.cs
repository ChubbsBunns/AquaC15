using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKing : MonoBehaviour
{
    public PlayerController_2 player;
    public GameObject fireBall;
    public GameObject currentFireBall;
    public GameObject ballLightning;

    public int damageKingDoes;

    [Header("(Attack 1) ball lightning")]
    public bool ballLightningAttack;
    public float timeBetweenBallLightning;
    public Vector3 instantiateHere;

    [Header("(Attack 2) Fire Ball")]
    public Transform[] patrolPoints;
    public int currentPointIndex;
    public float speed;
    public bool move;
    public Vector3 attackHere;
    public float timeBetweenFireballs;
    public Vector2 velocityDirection;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController_2>();
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

    public void BallLightningAttackStart()
    {
        StartCoroutine(WaitForBallLightningToAppear());
    }

    IEnumerator WaitForBallLightningToAppear()
    {
        instantiateHere = player.transform.position;
        yield return new WaitForSeconds(timeBetweenBallLightning);
        Instantiate(ballLightning, instantiateHere, Quaternion.identity);
    }

    public void FireballAttack()
    {
        StartCoroutine(FireballAttackNow());
    }

    IEnumerator FireballAttackNow()
    {
        Vector3 vectorDirectionToPlayer = player.transform.position - transform.position;
        Debug.DrawRay(transform.position, vectorDirectionToPlayer);
        Quaternion swordRotation = Quaternion.LookRotation(Vector3.forward, vectorDirectionToPlayer);
        velocityDirection = new Vector2(vectorDirectionToPlayer.x, vectorDirectionToPlayer.y).normalized;

        attackHere = player.transform.position;
        yield return new WaitForSeconds(timeBetweenFireballs);
        currentFireBall = Instantiate(fireBall, transform.position, swordRotation);
        currentFireBall.GetComponent<Fireball>().SetVelocity(velocityDirection);
    }

    public void SetMoveToTrue()
    {
        move = true;
    }

    public void SetMoveToFalse()
    {
        move = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController_2 playerGameObject = collision.gameObject.GetComponent<PlayerController_2>();
            playerGameObject.TakeDamage(damageKingDoes);
        }
    }
}
