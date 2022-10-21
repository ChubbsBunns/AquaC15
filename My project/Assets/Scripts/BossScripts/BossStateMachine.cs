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
    public float powerDashSpeed;
    public bool grounded;
    public Transform feet;
    public LayerMask whatIsGround;
    public FallingRock fallingRockPrefab;
    public List<List<Transform>> fallingRockPositions = new List<List<Transform>>();
    public List<Transform> fallingRockPositions1;
    public List<Transform> fallingRockPositions2;
    public List<Transform> fallingRockPositions3;
    public float maxTimeBeforeRockFall;

    public BossChaseState bossChaseState = new BossChaseState();
    public BossWindUpState bossWindUpState = new BossWindUpState();
    public BossRockThrowState bossRockThrowState = new BossRockThrowState();
    public BossPowerDashState bossPowerDashState = new BossPowerDashState();
    public BossGroundPoundState bossGroundPoundState = new BossGroundPoundState();
    private void Start()
    {
        fallingRockPositions.Add(fallingRockPositions1);
        fallingRockPositions.Add(fallingRockPositions2);
        fallingRockPositions.Add(fallingRockPositions3);
        player = FindObjectOfType<Player_Mine>().centreOfPlayerTransform;
        rb = GetComponent<Rigidbody2D>();
        currentState = bossChaseState;
        currentState.BossEnterState(this);
    }

    private void Update()
    {
        grounded = Physics2D.OverlapCircle(feet.position, 0.1f, whatIsGround);
        currentState.BossUpdate(this);
    }

    public void ChangeState(BossState state)
    {
        currentState.BossExitState(this);
        state.BossEnterState(this);
        currentState = state;

    }

    public void ThrowRock()
    {
        GameObject rock = Instantiate<GameObject>(rockPrefab, rockOrigin.position, Quaternion.identity);
        rock.GetComponent<Rigidbody2D>().velocity = (targetImage.transform.position - rockOrigin.position).normalized * rockSpeed;
    }

    public void GroundPound(int num)
    {
        foreach(Transform t in fallingRockPositions[num - 1])
        {
            FallingRock fallingRock = Instantiate<FallingRock>(fallingRockPrefab, t.position, Quaternion.identity);
            fallingRock.maxTimeBeforeFalling = maxTimeBeforeRockFall;
        }
        //Do something like shake the screen
    }

    //Do something for when the boss takes damage later

    public void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.BossCollision(this, collision);
    }
}
