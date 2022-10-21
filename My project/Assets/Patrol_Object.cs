using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol_Object : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed;

    public AquaMiteWorkerRoute AquamiteRoute;

    public AquamiteInstantiator AquamiteInstantiatorThing;
    public float lower_bound_speed;
    public float upper_bound_speed;
    
    public int currentPointIndex;
    public bool move;
    public float timeToWaitToMove;

    public int damageIDealToPlayer;

    public Collider2D thisCollider;

    void Start()
    {

        thisCollider = GetComponent<Collider2D>();
        speed = Random.Range(lower_bound_speed, upper_bound_speed);
        AquamiteInstantiatorThing = FindObjectOfType<AquamiteInstantiator>();
        //patrolPoints = new Transform[AquamiteRoute.points.Length + 2];
        patrolPoints[0].position = AquamiteInstantiatorThing.transform.position;
        GetPoints();
        if (AquamiteRoute)
        {
            StartCoroutine(NowYouMove());
        }

    }

    public void GetPoints()
    {
        Debug.Log("This is getting called");
        AquamiteRoute = FindObjectOfType<AquaMiteWorkerRoute>();
        if (AquamiteRoute != true)
        {
            Debug.LogError("I cant find the aquamite worker route object");
        }
        else
        {
            Debug.Log("Aquamite is true");
        }
        transform.position = patrolPoints[0].position;

        for (int i = 0; i < AquamiteRoute.points.Length; i++)
        {
            Debug.Log("AquamteRoute Points" + i + AquamiteRoute.points[i].position.x);
        }

        Debug.Log("PatrolPoints.Length" + patrolPoints.Length);
        Debug.Log("AquamiteRoute.points.Length" + AquamiteRoute.points.Length);

        for (int i = 0; i < AquamiteRoute.points.Length; i++)
        {
            patrolPoints[i] = AquamiteRoute.points[i];
            //Debug.Log(i);
            //patrolPoints[i + 1] = transform;
            //Debug.Log(i + 1);
            //AquamiteRoute.points[i] = transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (move == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
            if (Mathf.Abs(transform.position.x - patrolPoints[currentPointIndex].position.x) <= 0.3f   )
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

    private void OnCollisionEnter2D(Collision2D other) 
    {
//        Debug.Log(other);
        if (other.gameObject.CompareTag("AquaMites"))
        {
            Collider2D AquaMiteCollider2D = other.gameObject.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(AquaMiteCollider2D, thisCollider);
        }    
    }
}