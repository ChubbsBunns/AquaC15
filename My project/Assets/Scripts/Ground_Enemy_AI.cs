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

    public int GroundLayerMaskIndex = 6;

    private Path path;
    private int currentWaypoint = 0;
    bool isGrounded = false;
    Seeker seeker;
    public Rigidbody2D rb;

    public GameObject Sprites;

    [SerializeField] Queue<Transform> targets = new Queue<Transform>();
    public Ground_Enemy_AI ai;
    private bool caught = false;
    public bool facingLeft = true;

    public bool blockedInFront = false;
    public float blockFrontCheckOffset = 1.5f;
    public float blockFrontCheckDistance = 13f;
    public Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
        if (target == null)
        {
            target = FindObjectOfType<Player_Controller_1>().transform;
        }
        ai = this;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void FixedUpdate()
    {
        CheckFront();
        if(TargetInDistance() && followEnabled && target != null)
        {
            anim.SetBool("Follow", true);
            PathFollow();
        }
        else
        {
            anim.SetBool("Follow" ,false);
        }
    }

    private void CheckFront()
    {
        //used to check if there is something in front blocking the enemy from reaching the player
        if (facingLeft)
        {
            Vector2 checkInitialPosition = new Vector2 (transform.position.x - blockFrontCheckOffset, transform.position.y);
            RaycastHit2D hit = Physics2D.Raycast(checkInitialPosition, Vector2.left, blockFrontCheckDistance);
            Debug.Log("I am facing left");
            if (hit)
            {
                Debug.Log("I have hit something while facing left");
                blockedInFront = true;
            }
            else
            {
                                Debug.Log("I have failed to hit something while facing left");
                blockedInFront = false;
            }
        }
        else
        {
            Vector2 checkInitialPosition = new Vector2 (transform.position.x + blockFrontCheckOffset, transform.position.y);
            RaycastHit2D hit = Physics2D.Raycast(checkInitialPosition, Vector2.right, blockFrontCheckDistance);
            Debug.Log("I am facing right");
            if (hit)
            {
                                Debug.Log("I have hit something while facing right");
                blockedInFront = true;
            }
            else
            {
                                Debug.Log("I have failed to hit something while facing right");
                blockedInFront = false;
            }
        }

    }

    private void OnDrawGizmos() {
        Vector2 checkInitialPosition = new Vector2 (transform.position.x + blockFrontCheckOffset, transform.position.y);
        Gizmos.DrawRay(checkInitialPosition, Vector2.right);
        Gizmos.DrawRay(checkInitialPosition, Vector2.left);
    }

    private void UpdatePath()
    {
        if(followEnabled && TargetInDistance() && seeker.IsDone() && (target != null))
        {

            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    } 

    public void Jump()
    {
        anim.Play("jump");
        rb.AddForce(Vector2.up * speed * jumpModifier);
    }

    public void FaceDirection(bool faceLeft)
    {
        if (faceLeft)
        {
            // transform.Rotate(0f, 180f, 0f);
            Sprites.transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            facingLeft = true;
        }
        else 
        {
            // transform.Rotate(0f, 180f, 0f);
            facingLeft = false;
            Sprites.transform.localScale = new Vector3(-1f *Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
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
        if (caught) { 
                        Debug.Log("caught") ;
            return; }
        targets.Enqueue(newTarget);
        if (!followEnabled)
        {
                        Debug.Log("set target") ;
            target = targets.Dequeue();
            followEnabled = true;
        }
    }

    public void testThing(Transform targetThing)
    {
        followEnabled = true;
        target = targetThing;
    }

    public void Caught()
    {
        SpeakerManager.instance.Speak(4);
        SpeakerManager.instance.StopSpeak();
        caught = true;
        targets.Clear();
    }

    private void JumpAnimEnded()
    {
        anim.SetBool("Jump", false);
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
        if(jumpEnabled && isGrounded )
        {
            if((direction.y > jumpNodeHeightRequirement && followEnabled) || (followEnabled && blockedInFront))
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player_Health player_health = other.gameObject.GetComponent<Player_Health>();
            player_health.TakeDamage();
        }
    }
}
