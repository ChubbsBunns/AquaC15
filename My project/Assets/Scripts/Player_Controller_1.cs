using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller_1 : MonoBehaviour
{
    [Header("Player States")]
    public bool is_jumping = false;
    public bool is_dashing = false;
    public bool player_is_controllable = true;
    public bool is_airborne = false;
    public bool is_running = false;

    [Header("Horizontal Movement")]
    public float current_Speed;
    public bool facing_Right;

    public int prev_horizontal_input = 0;

    public float time_from_zero_to_max_horizontal_speed = 0.3f;
    public float max_horizontal_speed;
    public float time_from_max_horizontal_speed_to_zero = 0.2f;
    public float acceleration;
    public float deceleration;
    public int horizontal_Input;
    public Rigidbody2D rb;

    [Header("Jumping")]
    public float jump_speed = 10f;
    public int jump_threshold = 10;
    public float check_radius = 0.5f;
    public LayerMask what_is_ground;
    public float stop_jump_fast_speed = 4.0f;
    public int max_number_of_jumps;

    public float steps_jumped = 0;
    public bool is_grounded = false;
    public bool is_hitting_ceiling = false;
    public int jump_count;
    public bool dont_continuously_jump;
    public bool cant_jump_anymore;
    public int jump_count_placeholder = 1;

    public float current_falling_speed;

    //initial_jump_helper will be false when on the ground, during the inital jump, it will be true for a few milliseconds before going off
    public bool initial_jump_helper;
    public float jump_up_anim_duration;

    [Header("Ground Checks")]
    //ground checks are usually for jump management
    public Transform ground_check_mid;
    public Transform ground_check_front;
    public Transform ground_check_back;
    //gorund check top is only meant for jumping stoppages upon hitting a ceiling
    public Transform ground_check_top;

    public Collider2D PlayerCollider;

    [Header("Ground RayCasts")]
    RaycastHit2D ground_hit;
    public float distance_to_ground;
    public float ground_check_raycast_threshold = 0.5f;
    //distance_to_ground measures the distance from the middle of the Bottom of Nu (ground middle check) the the ground (platform below it)

    [Header ("Gravity Scales")]
    public float current_gravity_scale;
    public float original_gravity_scale;
    public float falling_faster_gravity_scale;
    public float falling_slower_gravity_scale;

    [Header("Last Directional Inputs")]
    public float last_horizontal_input;
    public float last_vertical_input;
    public float last_directional_input;

    [Header("Attacks")]
    public int player_attack_damage;
    public LayerMask Enemy;
    private Collider2D[] enemies_to_hit;

    public float time_between_attacks;

    public Player_Sword player_slash;
    public bool able_to_attack;

    [Header("Health")]
    public int health;

    [Header("Enemy Detection")]
    public Transform PlayerCenter;

    [Header("Animation")]
    public Animator Nu_Anim;

    private void Awake()
    {
        if (FindObjectsOfType<Player_Controller_1>().Length > 1)
        {
            //Destroy(gameObject);
            Debug.LogError("There are multiple Players in this scene");
            Debug.LogError(transform.position);
            Debug.LogError(FindObjectsOfType<Player_Controller_1>().Length);
            //Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //This is a placeholder for health
        health = 5;
         PlayerCollider = GetComponent<Collider2D>();
         
        acceleration = max_horizontal_speed / time_from_zero_to_max_horizontal_speed;
        deceleration = max_horizontal_speed / time_from_max_horizontal_speed_to_zero;
        rb = GetComponent<Rigidbody2D>();
        Nu_Anim = GetComponent<Animator>();

        rb.gravityScale = original_gravity_scale;

        //get sword weapon componenet
        player_slash = FindObjectOfType<Player_Sword>();
        if(player_slash == null)
        {
            Debug.LogError("Why ah");
        }

        player_is_controllable = true;


    }

    // Update is called once per frame
    void Update()
    {

        //Attack

            Sword_Attack();
            //Jump thing
            jump_count_placeholder = Mathf.Clamp(jump_count_placeholder, 1, max_number_of_jumps);

            //put input functions here
            Horizontal_Movement();
            Grounded_Check();
            Double_Jump_Handler();
            Jump_Inputs();

            Am_I_Airborne();
            current_falling_speed = rb.velocity.y;
            current_gravity_scale = rb.gravityScale;

    
    }

    private void FixedUpdate()
    {
        if (player_is_controllable)
        {
            Do_I_Flip();
            Jump();
            Update_Distance_To_Ground();
            //put jump physics functions here
        }


    }

    public void Update_Distance_To_Ground()
    {
        ground_hit = Physics2D.Raycast(ground_check_mid.position, Vector2.down, Mathf.Infinity);
        Vector2 vector_to_ground= (Vector2) ground_check_mid.position - ground_hit.point;
        distance_to_ground = Calculate_Distance_Vector2(vector_to_ground);

    }
    private void Horizontal_Movement()
    {
        horizontal_Input = (int)Input.GetAxisRaw("Horizontal");

//        Debug.Log("is_grounded is " + is_grounded);
//        Debug.Log("is_airborne is " + is_airborne);
//        Debug.Log("Math.Abs(horizontal_Input is " + Mathf.Abs(horizontal_Input));
        if (player_is_controllable != true)
        {
            horizontal_Input = 0;
            
        }
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 || is_running || is_jumping || is_dashing )
        {
            current_Speed += acceleration * Time.deltaTime;
            if (is_grounded == true && is_airborne == false)
            {
                Nu_Anim.SetBool("Sprinting", true);
            }
            if (is_grounded)
            {
                Nu_Anim.SetBool("Dont_Run_Yet", false);
            }
            else
            {
                Nu_Anim.SetBool("Dont_Run_Yet", true);
            }
        }
        if (Mathf.Abs(horizontal_Input) == 0)
        {
            Nu_Anim.SetBool("Sprinting", false);
            current_Speed -= deceleration * Time.deltaTime;
        }

        //remembering last input if it is 1 or -1
        if (horizontal_Input == 1)
        {
            last_horizontal_input = 1;
            last_directional_input = 2;
        }

        else if (horizontal_Input == -1)
        {
            last_horizontal_input = -1;
            last_directional_input = 1;
        }
        current_Speed = Mathf.Clamp(current_Speed, 0, max_horizontal_speed);
        rb.velocity = new Vector2(current_Speed * last_horizontal_input, rb.velocity.y);
    }


    void Jump_Inputs()
    {
        if (dont_continuously_jump == false)
        {
            if (Input.GetButton("Jump") && cant_jump_anymore == false)
            {
                is_jumping = true;
                Nu_Anim.SetBool("Jump1", true);
                StartCoroutine(Jump_Anim_Helper());
            }

            if (!Input.GetButton("Jump"))
            {
                Nu_Anim.SetBool("Jump1", false);
            }

            if (!Input.GetButton("Jump") && steps_jumped < jump_threshold && is_jumping)
            {
                Stop_Jump_Quick();
            }

            else if ((!Input.GetButton("Jump") && steps_jumped > jump_threshold && is_jumping) || is_hitting_ceiling)
            {
                Stop_Jump_Slow();
            }
        }
        Handling_Jump_States();
    }

    IEnumerator Jump_Anim_Helper()
    {
        initial_jump_helper = true;
        yield return new WaitForSeconds(jump_up_anim_duration);
        initial_jump_helper = false;
    }

    void Jump()
    {
        if (is_jumping == true)
        {
            if (steps_jumped < jump_threshold)
            {
                rb.velocity = new Vector2(rb.velocity.x, jump_speed);
                steps_jumped++;
            }
            else
            {
                Stop_Jump_Slow();
            }
        }
    }

    void Stop_Jump_Quick()
    {
        //stops the player from jumping immediately, causing them to fall down as soon as the button is released ( is used when the player stops his jump prematurely)\
        steps_jumped = 0;
        rb.velocity = new Vector2(rb.velocity.x, stop_jump_fast_speed);
        is_jumping = false;
        //Debug.Log("Stop Jump Fast");
    }

    void Stop_Jump_Slow()
    {
        //stops the jump but lets the player hang in the air for awhile. (when the player maxes out his or her jump time)
        steps_jumped = 0;
        is_jumping = false;
        //Debug.Log("Stop Jump Slow");
    }

    void Handling_Jump_States()
    {
        //This is just to ensure that when the player holds down the space key, the character does not continuously jump
        if (Input.GetButton("Jump"))
        {
            dont_continuously_jump = true;
            initial_jump_helper = true;
        }
        else
        {
            dont_continuously_jump = false;
        }
    }

    void Double_Jump_Handler()
    {
        if (is_grounded)
        {
            cant_jump_anymore = false;
            jump_count = 0;
            jump_count_placeholder = 1;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jump_count_placeholder += 1;
            if (jump_count == 0)
            {
                jump_count_placeholder = 1;
            }

            jump_count += 1;
        }

        if (jump_count > max_number_of_jumps)
        {
            cant_jump_anymore = true;
        }
    }

    void Grounded_Check()
    {
        is_grounded = Physics2D.OverlapCircle(ground_check_mid.position, check_radius, what_is_ground);
        if (is_grounded && initial_jump_helper == true)
        {
            Nu_Anim.SetBool("Jump1", false);
        }
        if (is_grounded)
        {
            Nu_Anim.SetBool("Dont_Run_Yet", false);
        }
        is_hitting_ceiling = Physics2D.OverlapCircle(ground_check_top.position, check_radius, what_is_ground);
    }

    void Am_I_Airborne()
    {
        if (is_grounded == false)
        {
            is_airborne = true;
        }
        else
        {
            //is_grounded == true
            is_airborne = false;
        }

        if (is_airborne == true)
        {
            jump_count = jump_count_placeholder;
        }
    }


    void Flip()
    {
        // transform.localScale = new Vector3(-(float)transform.localScale.x, transform.localScale.z, transform.localScale.z);
        transform.Rotate(0f, 180f, 0f);
        facing_Right = !facing_Right;
        current_Speed = 0;
    }

    void Do_I_Flip()
    {
        if (horizontal_Input > 0 && facing_Right == false)
        {
            Flip();
        }

        if (horizontal_Input < 0 && facing_Right == true)
        {
            Flip();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(ground_check_mid.position, check_radius);
        Gizmos.DrawSphere(ground_check_front.position, check_radius);
        Gizmos.DrawSphere(ground_check_back.position, check_radius);
    }

    private void Sword_Attack()
    {        
        if (Input.GetButtonDown("Fire1") && able_to_attack)
        {
//            Debug.Log("I am able to sense sword attack button and isabletoattack");
            if (Input.GetButton("Up"))
            {
                player_slash.StartUpAttack();
            }

            else if (Input.GetButton("Down"))
            {
                player_slash.StartDownAttack();
            }
            else
            {
                player_slash.StartHorizontalAttack();
            }
            StartCoroutine(SetAbleToAttackToFalse());
        }
    }

    IEnumerator SetAbleToAttackToFalse()
    {
        able_to_attack = false;
        yield return new WaitForSeconds(time_between_attacks);
        able_to_attack = true;
    }

    //PORTAL MANAGEMENT
    public void TransformChangeMoveMeHere(Transform myNewPosition)
    {
        transform.position = myNewPosition.position;
    }

    //MATH HELPER FUNCTIONS

    public float Calculate_Distance_Vector2(Vector2 vector2)
    {
        float total_square = Calculate_Square(vector2.x) + Calculate_Square(vector2.y);
        float distance = Mathf.Sqrt(total_square);
        return distance;
    }

    public float Calculate_Square(float value)
    {
        return value * value;
    }


    //Collision management relative to player
    private void OnCollisionEnter2D(Collision2D other) 
    {
//        Debug.Log(other);
        if (other.gameObject.CompareTag("AquaMites"))
        {
            Collider2D AquaMiteCollider2D = other.gameObject.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(AquaMiteCollider2D, PlayerCollider);
        }    
    }
    



    //end of class
}
