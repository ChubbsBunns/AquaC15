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

    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        int orderInLayer = (int)Random.Range(0.0f,5.0f);

        sr.sortingOrder = orderInLayer ;
        thisCollider = GetComponent<Collider2D>();
        speed = Random.Range(lower_bound_speed, upper_bound_speed);
        AquamiteInstantiatorThing = FindObjectOfType<AquamiteInstantiator>();
        AquamiteRoute = FindObjectOfType<AquaMiteWorkerRoute>();
        patrolPoints = new Transform[AquamiteRoute.points.Length + 2];
        
        Debug.Log("AquamiteInstantiatorThing.transform.position" + AquamiteInstantiatorThing.transform.position.x + AquamiteInstantiatorThing.transform.position.y);
        GetPoints();
        patrolPoints[0].position = AquamiteInstantiatorThing.transform.position;
        if (AquamiteRoute)
        {
            StartCoroutine(NowYouMove());
        }

    }

    public void GetPoints()
    {
        for (int i = 0; i < AquamiteRoute.points.Length; i++)
        {
            Transform PositionThing = AquamiteRoute.points[i];
            patrolPoints[i] = PositionThing;
        }
        patrolPoints[0] = gameObject.transform;
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
        if (other.gameObject.CompareTag("PlayerBlockerCollider") || other.gameObject.CompareTag("PlayerCollider"))
        {
            Collider2D ColliderToIgnore = other.gameObject.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(ColliderToIgnore, thisCollider);
        }
    }
}