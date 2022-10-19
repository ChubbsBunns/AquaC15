using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{
    BossState currentState;
    public Transform player;
    public Transform rockOrigin;
    public GameObject rockPrefab;
    public float rockSpeed;
    public Rigidbody2D rb;
    public float bossSpeed;
    public float timeBetweenAttacks;
    public float windUpTime;
    public GameObject targetImage;

    public BossChaseState bossChaseState = new BossChaseState();
    public BossInAirState bossInAirState = new BossInAirState();
    public BossWindUpState bossWindUpState = new BossWindUpState();
    public BossRockThrowState bossRockThrowState = new BossRockThrowState();
    private void Start()
    {
        player = FindObjectOfType<Player_Mine>().centreOfPlayerTransform;
        rb = GetComponent<Rigidbody2D>();
        currentState = bossChaseState;
        currentState.BossEnterState(this);
    }

    private void Update()
    {
        currentState.BossUpdate(this);
    }

    public void ChangeState(BossState state)
    {
        currentState.BossExitState(this);
        currentState = state;
        currentState.BossEnterState(this);
    }

    public void ThrowRock()
    {
        GameObject rock = Instantiate<GameObject>(rockPrefab, rockOrigin.position, Quaternion.identity);
        rock.GetComponent<Rigidbody2D>().velocity = (targetImage.transform.position - rockOrigin.position).normalized * rockSpeed;
    }

    //Do something for when the boss takes damage later
}
