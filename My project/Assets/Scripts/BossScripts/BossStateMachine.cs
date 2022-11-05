using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{
    [Header("Not Assigned")]
    BossState currentState;
    Player_Health playerHealth;
    public Transform player;
    public Rigidbody2D rb;
    public bool grounded;

    [Header("General Boss Variables")]
    public float bossSpeed;
    public float timeBetweenAttacks;
    public float windUpTime;
    public Transform feet;
    public LayerMask whatIsGround;
    public GameObject gateTileMap;
    public Collider2D bossCollider;
    public BossHealth bossHealth;

    [Header("Rock Throw Variables")]
    public Transform rockOrigin;
    public GameObject rockPrefab;
    public float rockSpeed;
    public float timeForRockThrow;
    public int numRocksPerAttack;
    public float timeDecrementBetweenThrows;
    public GameObject targetImage;

    [Header("Power Dash Variables")]
    public float powerDashSpeed;

    [Header("Ground Pound Variables")]
    public FallingRock fallingRockPrefab;
    public List<List<Transform>> fallingRockPositions = new List<List<Transform>>();
    public List<Transform> fallingRockPositions1;
    public List<Transform> fallingRockPositions2;
    public List<Transform> fallingRockPositions3;
    public float maxTimeBeforeRockFall;
    public float timeBetweenGroundPounds;
    public int numberOfGroundPounds;
    public float cameraShakeIntensity;
    public float cameraShakeDuration;

    //Boss States
    public BossChaseState bossChaseState = new BossChaseState();
    public BossWindUpState bossWindUpState = new BossWindUpState();
    public BossRockThrowState bossRockThrowState = new BossRockThrowState();
    public BossPowerDashState bossPowerDashState = new BossPowerDashState();
    public BossGroundPoundState bossGroundPoundState = new BossGroundPoundState();
    public BossIdleState bossIdleState = new BossIdleState();

    //Animation
    public Animator BossAnim;

    private void Start()
    {
        BossAnim = GetComponent<Animator>();
        fallingRockPositions.Add(fallingRockPositions1);
        fallingRockPositions.Add(fallingRockPositions2);
        fallingRockPositions.Add(fallingRockPositions3);
        player = FindObjectOfType<Player_Mine>().centreOfPlayerTransform;
        playerHealth = player.GetComponentInParent<Player_Health>();
        rb = GetComponent<Rigidbody2D>();
        currentState = bossIdleState;
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

    //Function called by rock throw state
    //Simply instantiates and throws a rock at player
    public void ThrowRock()
    {
        GameObject rock = Instantiate<GameObject>(rockPrefab, rockOrigin.position, Quaternion.identity);
        rock.GetComponent<Rigidbody2D>().velocity = (targetImage.transform.position - rockOrigin.position).normalized * rockSpeed;
    }

    //Function called by groundpound state
    //Makes rocks fall at locations specified in fallingRockPositions list
    //num is used to determine which set of positions to used for variations between sets of falling rocks
    public void GroundPound(int num)
    {
        CinemachineCameraShake.instance.ShakeCamera(cameraShakeIntensity, cameraShakeDuration);
        foreach(Transform t in fallingRockPositions[num - 1])
        {
            FallingRock fallingRock = Instantiate<FallingRock>(fallingRockPrefab, t.position, Quaternion.identity);
            fallingRock.maxTimeBeforeFalling = maxTimeBeforeRockFall;
        }
        //Do something like shake the screen
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.BossCollision(this, collision);
        if(collision.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage();
        }
    }

    //Called when triggered by player walking into a trigger point
    //Starts the boss fight
    public void StartFight()
    {
        gateTileMap.gameObject.SetActive(true);
        bossHealth.EnableHealthBar();
        ChangeState(bossChaseState);
    }
}
