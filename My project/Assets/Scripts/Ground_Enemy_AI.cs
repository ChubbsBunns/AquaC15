using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Ground_Enemy_AI : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform target;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.8f; //How vertical the next node needs to be in order for the character to jump
    public float jumpModifier = 0.6f; //Setting how high jump is
    public float jumpCheckOffset = 0.1f; //Adjust it to make sure collider is right
    public Transform feet;
    public LayerMask whatIsGround;

    [Header("Custom Behaviour")]
    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;

    private Path path;
    private int currentWaypoint = 0;
    bool isGrounded = false;
    Seeker seeker;
    public Rigidbody2D rb;

    Queue<Transform> targets = new Queue<Transform>();
    public Ground_Enemy_AI ai;
    private bool caught = false;
    public void Start()
    {
        ai = this;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void FixedUpdate()
    {
        if(TargetInDistance() && followEnabled)
        {
            PathFollow();
        }
    }

    private void UpdatePath()
    {
        if(followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * speed * jumpModifier);
    }

    public void FaceDirection(bool faceLeft)
    {
        if (faceLeft) { transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); }
        else { transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); }
    }

    public void StopFollowing()
    {
        if(targets.Count > 0)
        {
            target = targets.Dequeue();
        }
        else
        {
            followEnabled = false;
        }
    }

    public void SetTarget(Transform newTarget)
    {
        if (caught) { return; }
        targets.Enqueue(newTarget);
        if (!followEnabled)
        {
            target = targets.Dequeue();
            followEnabled = true;
        }
    }

    public void Caught()
    {
        SpeakerManager.instance.Speak(4);
        SpeakerManager.instance.StopSpeak();
        caught = true;
        targets.Clear();
    }

    private void PathFollow()
    {
        if(path == null)
        {
            return;
        }

        //Reached end of path
        if(currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        //See if colliding with the ground
        // Vector3 startOffset = transform.position - new Vector3(0f,GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);
        // isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);
        isGrounded = Physics2D.OverlapCircle(feet.position, 0.05f, whatIsGround);

        //Direction calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        //Jump
        if(jumpEnabled && isGrounded)
        {
            if(direction.y > jumpNodeHeightRequirement)
            {
                Jump();
            }
        }

        //Movement
        rb.AddForce(new Vector2(force.x, 0f));

        //Next Waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if(distance < nextWaypointDistance)
        {
            ++currentWaypoint;
        }

        if(directionLookEnabled)
        {
            FaceDirection(rb.velocity.x < -0.05f);
            /*if(rb.velocity.x > 0.05f)
            {
                //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if(rb.velocity.x < -0.05f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }*/
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    private void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void FollowSpecificTarget(Transform FollowThis)
    {
        target = FollowThis;
    }
}
