using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController_2 : MonoBehaviour
{


    //TODO : Clean up code such that I only see the values I need to see at the top of the editor
    //How to use this character controller: 
    // before I forget, make sure the public class of the script matches its name, a problem will arise when copying this script and renaming it to another script name
    //1) set up "Buttons" in the Project settings under edit--
    // buttons to set up: Up, Down, Jump, Dash
    // 2) Fill up everything underneath "Jumping To See Header" (meaning thgat everything under Movement and below should have a value that makes sense
    // To note: maximumFloatingFallSpeed should be negative
    // 3) Create the frontCheck and GroundCheck variable game objects
    // 4) Set them as children to the player object
    // 5) Create "Ground" layermask in the "layer" dropdown from any game object
    // 6) In the "WhatIsGround" LayerMask, make sure it is Ground
    // 7) If your character is Facing right by default, make sure FacingRight is true
    // 8) Idk man 
    [Header("Scene Management")]
    public bool newSceneJustLoaded;

    [Header("Health Related")]
    public bool invulnerableToDamage = false;
    public float invulnerableToDamageTime;

    [Header("Unlocks")]
    public bool ableToDash = false;
    public bool ableToGlide = false;
    public float currentAvailableJumps = 1;

    [Header("Physics To see")]


    [Header("Last Directional Inputs")]
    public float lastHorizontalInput;
    public float lastVerticalInput;
    public float lastDirectionalInput;
    // lastDirectionalInput reference: 
    // left = 1
    // right = 2
    // up = 3
    // down = 4
    [Header("Movement To See")]
    public float currentSpeed;
    public bool facingRight;

    [Header("Jumping To see")]
    public int stepsJumped = 0;
    public bool isGrounded = false;
    public int jumpCount;
    public bool dontContiuouslyJump;
    public bool cantJumpAnymore;
    public float jumpCountPlaceholder = 1;

    [Header("Movement (current)")]
    //currents
    public float currentMaxHorizontalSpeed;

    public float current_acceleration;
    public float current_deceleration;


    [Header("Movement (Default values))")]
    public float acceleration;
    public float deceleration;
    public float timeFromMaxHorizontalSpeeedToZero = 0.2f;
    public float timeFromZeroToMaxHorizontalSpeed = 0.3f;
    public float maxHorizontalSpeed_Original;
    float HorizontalInput;
    private Rigidbody2D rb;

    [Header("Jumping")]
    public float jumpSpeed = 10f;
    public int jumpThreshold = 10;
    public Transform groundCheckMiddle;
    public Transform groundCheckFront;
    public Transform groundCheckBack;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float stopJumpFastSpeed;
    public int maxNumberOfJumpsPermitted;
    public float firstJumpCheck;
    public float currentJumpDistanceFromGround;

    [Header("Falling")]
    public float maximumFloatingFallSpeed;

    [Header("Dashing")]
    public float dashAddedVelocityX;
    public bool amIAbleToDashNow = true;
    public float VerticalInput;
    public float dashDuration;
    public float dashHorizontalInput;
    public float timeBetweenDashes;
    public float dashDirectionInput;

    public bool dashBugFix = true;

    [Header("Health")]
    public int health;
    public int numOfHealth;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Physics")]
    public float originalGravityScale;
    public float fallingSlowerGravityScale;
    public float fallingFasterGravityScale;
    public float currentGravityScale;


    [Header("WallSliding And WallJumping")]
    public bool isTouchingFront;
    public Transform frontCheck;
    public bool wallSliding;
    public float wallSlidingSpeed;
    public float wallCheckRadius;

    public bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    [Header("Player Sword")]
    public Sword sword_Player; 
    public Animator sword_animator;
    public Collider2D sword_Collider;
    //attackAnimationTime MUST be hardcoded help pls
    public float attackAnimationTime;

    public int placeholder = 0;

    [Header("Player States")]
    public bool isJumping;
    public bool isDashing;
    public bool playerIsControllable;
    public bool isAirBorne;

    [Header("Player Combat")]
    public int playerAttackDamage;

    public Transform attackPoint;
    public Vector2 attackSize;
    public float attackAngle;
    public LayerMask enemy;
    private Collider2D[] enemiesToHit;

    [Header("Grappling floats")]
    public float grapple_acceleration;
    public float grapple_maxHorizontalSpeed;
    public float grapple_deceleration;

    public float timeBetweenAttacks;
    public bool ableToAttack = true;

    public bool amIGrappling;

    [Header("SceneManagement if dead")]
    public BlackScreen blackScreen;
    
    void Start()
    {
        blackScreen = FindObjectOfType<BlackScreen>();
        currentMaxHorizontalSpeed = maxHorizontalSpeed_Original;

        current_acceleration = currentMaxHorizontalSpeed / timeFromZeroToMaxHorizontalSpeed;
        current_deceleration = currentMaxHorizontalSpeed / timeFromMaxHorizontalSpeeedToZero;

        grapple_acceleration = grapple_maxHorizontalSpeed / timeFromZeroToMaxHorizontalSpeed;
        grapple_deceleration = grapple_maxHorizontalSpeed / timeFromMaxHorizontalSpeeedToZero;

        rb = GetComponent<Rigidbody2D>();
        //DontDestroyOnLoad(gameObject);

        //health related
        hearts = FindObjectOfType<Canvas>().heartsOnCanvas;
    }

    // Update is called once per frame
    void Update()
    {
        jumpCountPlaceholder = Mathf.Clamp(jumpCountPlaceholder, 1, maxNumberOfJumpsPermitted);
        Attack();
        GroundedCheck();
        DoubleJumpHandler();
        JumpInputs();

        MovingHorizontally();
        GetVerticalInputs();
        DoIDash();
        AmIAirborne();
        FallingSpeedCheck();
        WallSlideCheck();

        WallJump();
        WallJumpCheck();
        WallSlideJumpCountHandler();

        Dashing(dashDirectionInput);
        HealthManagement();
        if (amIAbleToDashNow == false && dashBugFix == true)
        {
            StartCoroutine(DashBugFix());
        }
    }



    private void FixedUpdate()
    {
        DoIFlip();
        Jump();
    }


    private void MovingHorizontally()
    {
        // getting horizontal input and applying acceleration and deceleration
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(HorizontalInput) > 0)
        {
            currentSpeed += current_acceleration * Time.deltaTime;
        }


        if (HorizontalInput == 0)
        {
            currentSpeed -= current_deceleration * Time.deltaTime;
        }

        //remembering last input 1 or -1
        if (HorizontalInput == 1)
        {
            lastHorizontalInput = 1;
            lastDirectionalInput = 2;
        }
        else if (HorizontalInput == -1)
        {
            lastHorizontalInput = -1;
            lastDirectionalInput = 1;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, currentMaxHorizontalSpeed);
        if (rb.velocity.x > currentMaxHorizontalSpeed)
        {
            currentSpeed = currentMaxHorizontalSpeed;
        }

        // the actual velocity code
        rb.velocity = new Vector2(currentSpeed * lastHorizontalInput, rb.velocity.y);
    }

    //dashing stuff

    private void DoIDash()
    {
        if (Input.GetButtonDown("Dash") && amIAbleToDashNow)
        {
            if (amIAbleToDashNow = true && lastDirectionalInput == 1)
            {
                dashDirectionInput = -1f;
            }
            else if (amIAbleToDashNow = true && lastDirectionalInput == 2)
            {
                dashDirectionInput = 1f;
            }
            if (amIAbleToDashNow)
            {
                isDashing = true;
                StartCoroutine(DashDurationHandler());
                amIAbleToDashNow = false;
                StartCoroutine(DashAvailability());
            }
        }
    }



    public void Dashing(float dashDirectionInput)
    {
        if (isDashing)
        {
            if ((lastDirectionalInput == 1 || lastDirectionalInput == 2) && isDashing)
            {
                rb.velocity = new Vector2(rb.velocity.x + dashAddedVelocityX * dashDirectionInput, 0);
            }
        }
    }

    IEnumerator DashDurationHandler()
    {
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }

    IEnumerator DashAvailability()
    {
        yield return new WaitForSeconds(timeBetweenDashes);
        amIAbleToDashNow = true;
    }

    IEnumerator DashBugFix()
    {
        dashBugFix = false;
        yield return new WaitForSeconds(timeBetweenDashes);
        amIAbleToDashNow = true;
        dashBugFix = true;
    }

    private void GetVerticalInputs()
    {
        VerticalInput = Input.GetAxisRaw("Vertical");
        if (VerticalInput == 1)
        {
            lastVerticalInput = 1;
            lastDirectionalInput = 3;
        }
        else if (VerticalInput == -1)
        {
            lastHorizontalInput = -1;
            lastDirectionalInput = 4;
        }
    }


    void GroundedCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckMiddle.position, checkRadius, whatIsGround) || Physics2D.OverlapCircle(groundCheckMiddle.position, checkRadius, whatIsGround) || Physics2D.OverlapCircle(groundCheckBack.position, checkRadius, whatIsGround);
    }

    //flipping
    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
        currentSpeed = 0;
    }

    void DoIFlip()
    {
        if (HorizontalInput > 0 && facingRight == false)
        {
            Flip();
        }

        if (HorizontalInput < 0 && facingRight == true)
        {
            Flip();
        }
    }

    //jumping
    void JumpInputs()
    {
        if (dontContiuouslyJump == false)
        {
            if (Input.GetButton("Jump") && cantJumpAnymore == false)
            {
                isJumping = true;
            }

            if (!Input.GetButton("Jump") && stepsJumped < jumpThreshold && isJumping)
            {
                StopJumpQuick();
            }
            else if (!Input.GetButton("Jump") && stepsJumped > jumpThreshold && isJumping)
            {
                StopJumpSlow();
            }
        }
        HandlingJumpStates();
    }

    void HandlingJumpStates()
    {

        if (Input.GetButton("Jump"))
        {
            dontContiuouslyJump = true;
        }
        else
        {
            dontContiuouslyJump = false;
        }
    }
    void Jump()
    {
        if (isJumping == true)
        {
            if (stepsJumped < jumpThreshold)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                stepsJumped++;
            }
            else
            {
                StopJumpSlow();
            }
        }
    }

    void StopJumpQuick()
    {
        //stops the player from jumping immediately, causing them to fall down as soon as the button is released ( is used when the player stops his jump prematurely)
        stepsJumped = 0;
        rb.velocity = new Vector2(rb.velocity.x, stopJumpFastSpeed);
        isJumping = false;
    }

    void StopJumpSlow()
    {
        //stops the jump but lets the player hang in the air for awhile. (when the player maxes out his or her jump time)
        stepsJumped = 0;
        isJumping = false;
    }

    void DoubleJumpHandler()
    {
        if (isGrounded)
        {
            cantJumpAnymore = false;
            jumpCount = 0;
            jumpCountPlaceholder = 1;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpCountPlaceholder += 1;
            if (jumpCount == 0)
            {
                jumpCountPlaceholder = 1;
            }

            jumpCount += 1;
        }

        if (jumpCount > maxNumberOfJumpsPermitted)
        {
            cantJumpAnymore = true;
        }

    }


    //check for isfalling
    private void AmIAirborne()
    {
        if (isGrounded == false)
        {
            isAirBorne = true;
        }
        else
        {
            isAirBorne = false;
        }

        if (isAirBorne == true)
        {
            jumpCount = (int)jumpCountPlaceholder;
        }
    }




    //falling speed check uses gravity to manipulate fall speed. future manipulations of gravity need to take note of this
    private void FallingSpeedCheck()
    {

        //if (isAirBorne == true && Input.GetButton("Up"))
        //{
        //    currentGravityScale = fallingSlowerGravityScale;
        //    rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, maximumFloatingFallSpeed, 100000));
        //}
       // else if (isAirBorne == true && Input.GetButton("Down"))
       //{
       //     currentGravityScale = fallingFasterGravityScale;
       //}
        //else
        {
            currentGravityScale = originalGravityScale;
        }
        rb.gravityScale = currentGravityScale;
    }


    //wall slide stuff
    private void WallSlideCheck()
    {
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, wallCheckRadius, whatIsGround);
        if (isTouchingFront == true && isGrounded == false && HorizontalInput != 0)
        {
            wallSliding = true;
        }

        else
        {
            wallSliding = false;
        }

        if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlidingSpeed);
        }
    }

    private void WallSlideJumpCountHandler()
    {
        if (wallSliding == true)
        {
            jumpCount = 0;
        }
    }

    void WallJumpCheck()
    {
        if (wallJumping == true)
        {

            if (facingRight)
            {
                rb.velocity = new Vector2(-xWallForce, yWallForce);
            }
            else
            {
                rb.velocity = new Vector2(xWallForce, yWallForce);
            }
        }
    }

    private void WallJump()
    {
        if (Input.GetButtonDown("Jump") && wallSliding && HorizontalInput != 0)
        {
            wallSliding = false;
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }
    }

    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }

    // portal management

    //health management
    public void HealthManagement()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    //Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheckMiddle.position, checkRadius);
        Gizmos.DrawSphere(groundCheckFront.position, checkRadius);
        Gizmos.DrawSphere(groundCheckBack.position, checkRadius);
        Gizmos.DrawSphere(frontCheck.position, wallCheckRadius);
    }

    //yse this for taking damage
    public void TakeDamage(int damageITake)
    {
        if (invulnerableToDamage == false)
        {
            health -= damageITake;
            StartCoroutine(invulnerableFrames());
        }

        if (health <= 0)
        {
            blackScreen.FadeToBlackOni();
            blackScreen.RetryButton();
        }
    }

    IEnumerator invulnerableFrames()
    {
        invulnerableToDamage = true;
        yield return new WaitForSeconds(invulnerableToDamageTime);
        invulnerableToDamage = false;
    }


    private void Attack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            if (ableToAttack)
            {
                sword_animator.SetBool("Attacking", true);
                SwordColliderChange();
                StartCoroutine(SetAbleToAttackToFalse());
                StartCoroutine(SetSwordColliderChangeToFalseAfterAttackAnim());
            }
        }
    }

    public void SwordColliderChange()
    {
        sword_Collider.enabled = !sword_Collider.enabled;
    }



    IEnumerator SetAbleToAttackToFalse()
    {
        ableToAttack = false;
        yield return new WaitForSeconds(timeBetweenAttacks);
        sword_animator.SetBool("Attacking", false);
        ableToAttack = true;
    }

    IEnumerator SetSwordColliderChangeToFalseAfterAttackAnim()
    {
        yield return new WaitForSeconds(attackAnimationTime);
        SwordColliderChange();
    }

    private void DealDamageToEnemy()
    {
        foreach (Collider2D enemy in enemiesToHit)
        {
            Debug.Log("i am me. i am here");
            if (enemy.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = enemy.gameObject.GetComponent<EnemyHealth>();
                enemyHealth.enemyTakeDamage(playerAttackDamage);
                Debug.Log("this is working 1");
            }
        }
    }

    public void grapplingOnPhysicsChanges()
    {
        currentMaxHorizontalSpeed = grapple_maxHorizontalSpeed;
        current_acceleration = grapple_acceleration;
        current_deceleration = grapple_deceleration;
    }

    public void grapplingOffPhysicsChanges()
    {
        currentMaxHorizontalSpeed = maxHorizontalSpeed_Original;
        current_acceleration = acceleration;
        current_deceleration = deceleration;
    }
}
