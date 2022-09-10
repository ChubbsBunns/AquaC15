using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreiddyIntro : MonoBehaviour
{
    [Header("Trigger Points")]
    public Intro_Trigger_Point[] trigger_points;

    // Start is called before the first frame update
    [Header("Horizontal Movement")]
    public float currentspeed;

    public Rigidbody2D rb;

    public float max_speed;
    public float acceleration;
    public float deceleration;

    //left right control controls how coroutines for the left and right are handled
    // if it is true that means something is controlling it, if it is false then something
    public bool left_right_control;

    [Header("Jumping")]
    public int steps_jumped;
    public int jump_threshold;

    public float jump_speed;

    [Header("Gravity Scales")]
    public float gravity_creiddy;

    [Header("States")]
    public bool moving_left;
    public bool moving_right;
    public  bool jumping;

    private Queue<IEnumerator> coroutineQueueVertical = new Queue<IEnumerator>();
    private Queue<IEnumerator> coroutineQueueHorizontal = new Queue<IEnumerator>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(CoroutineCoordinatorHorizontal());
        StartCoroutine(CoroutineCoordinatorVertical());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Manage_Horizontal_Movement();
        Moving_Horizontally();
        Moving_Vertically();
    }

    IEnumerator CoroutineCoordinatorHorizontal()
    {
        while (true)
        {
            while (coroutineQueueHorizontal.Count > 0)
            {
                yield return StartCoroutine(coroutineQueueHorizontal.Dequeue());
            }
            yield return null;
        }
    }

    IEnumerator CoroutineCoordinatorVertical()
    {
        while (true)
        {
            while (coroutineQueueVertical.Count > 0)
            {
                yield return StartCoroutine(coroutineQueueVertical.Dequeue());
            }
            yield return null;
        }
    }

    void Manage_Horizontal_Movement()
    {
        currentspeed = Mathf.Clamp(currentspeed, -max_speed, max_speed);
    }

    void Moving_Horizontally()
    {
        rb.velocity = new Vector2(currentspeed, rb.velocity.y);
        if (moving_left)
        {
            currentspeed -= acceleration;
        }
        else if (moving_right)
        {
            currentspeed += acceleration;
        }
        else
        {
            if (currentspeed > 0)
            {
                currentspeed -= deceleration;
            }
            else if (currentspeed < 0)
            {
                currentspeed += deceleration;
            }
        }
    }

    void Moving_Vertically()
    {
        if (jumping)
        {
            if (steps_jumped < jump_threshold)
            {
                {
                    rb.velocity = new Vector2(rb.velocity.x, jump_speed);
                    steps_jumped++;
                }
            }
            else
            {
                steps_jumped = 0;
                jumping = false;
            }
        }
    }

    public void Move_To_Next_Point(int index)
    {
        if (index == 1)
        {
            /*
            StartCoroutine(Control_Horizontal_Movement(0.2f, 0.6f, 1));
            StartCoroutine(Control_Vertical_Movement(0.3f, 0.4f));
            */
            coroutineQueueHorizontal.Enqueue(Control_Horizontal_Movement(0.2f, 0.6f, 1));
            coroutineQueueHorizontal.Enqueue(Control_Horizontal_Movement(0.3f, 1.7f, 2));
            coroutineQueueVertical.Enqueue(Control_Vertical_Movement(0.3f, 0.4f));
            coroutineQueueVertical.Enqueue(Control_Vertical_Movement(0.4f, 0.6f));
            coroutineQueueVertical.Enqueue(Control_Vertical_Movement(0.4f, 0.2f));

        }
        else if (index == 2)
        {
            coroutineQueueHorizontal.Enqueue(Control_Horizontal_Movement(0.2f, 0.6f, 1));
            coroutineQueueVertical.Enqueue(Control_Vertical_Movement(1.2f, 0.6f));
            coroutineQueueHorizontal.Enqueue(Control_Horizontal_Movement(0.6f, 0.7f, 1));
        }
        else // index == 3
        {
            
        }
    }

    private IEnumerator Control_Horizontal_Movement(float start_wait, float length_of_status, int left_or_right)
    {
        // if left_or_right is 1, then move left
        // if left_or_right is 2, then move right
        yield return new WaitForSeconds(start_wait);
        if (left_or_right == 1)
        {
            moving_left = true;
        }
        else if (left_or_right == 2)
        {
            moving_right = true;
        }
        yield return new WaitForSeconds(length_of_status);
        moving_left = false;
        moving_right = false;
    }

    private IEnumerator Control_Vertical_Movement(float start_wait, float length_of_status)
    {
        // if left_or_right is 1, then move left
        // if left_or_right is 2, then move right
        yield return new WaitForSeconds(start_wait);
        jumping = true;
        yield return new WaitForSeconds(length_of_status);
        jumping = false;
    }

}
