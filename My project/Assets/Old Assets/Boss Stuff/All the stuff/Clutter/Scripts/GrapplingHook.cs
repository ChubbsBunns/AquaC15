using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    //camera to be attained every time the scene changes
    public Camera maincamera;

    public DistanceJoint2D joint;
    public Vector3 targetPos;
    public RaycastHit2D hit;
    public LayerMask mask;


    //for debugging last time
    //public Vector3 debugray;
    //public Vector3 debugray2;

    public float maxDistance;

    //states
    public bool isGrappling;

    //sprite rendering
    public LineRenderer line;

    //physics to push the player to grapple point
    public float grappleStep;
    //grapple min distance is the joint.distance that the grapple will push the player towards, e.g. max distance is 10, grappleMin is 1, joint.distance will decrease (this shortens the grapple by applying a force to the gameobject to the origin point) until 1
    public float grappleMinDistance;

    //physics polishing
    public Rigidbody2D player_rb;
    public float downwardPullForce;

    public PlayerController_2 playerScript;

    // Start is called before the first frame update
    void Start()
    {
        FindMyCamera();
        playerScript = GetComponent<PlayerController_2>();
        player_rb = GetComponent<Rigidbody2D>();
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;

        line.enabled = false;
    }
    private void FixedUpdate()
    {
        if (joint.distance > grappleMinDistance )
        {
            if (Input.GetButton("Down"))
            {

            }

            else if(Input.GetKey(KeyCode.Mouse1))
            {
                joint.distance -= grappleStep;
            }

            else
            {
                joint.enabled = false;
                if (line != null)
                {
                    line.enabled = false;
                }

            }
        }
    }
    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            targetPos = maincamera.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;

            
            
            hit = Physics2D.Raycast(transform.position, targetPos - transform.position, maxDistance, mask);

            // this was for debugging last time
            //hit = Physics2D.Raycast(transform.position, debugray, maxDistance, mask);       

            //debugray = new Vector3(transform.position.x + 5, transform.position.y + 5, 0);
            //debugray2 = new Vector3(targetPos.x - 5, targetPos.y - 5, 0);
            //Debug.DrawRay(transform.position, debugray, Color.red, maxDistance);
            //Debug.DrawRay(targetPos, debugray2, Color.green, maxDistance);


            if (hit.collider!= null && hit.collider.gameObject.GetComponent<Rigidbody2D>()!= null)
            {
                joint.enabled = true;
                joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                joint.connectedAnchor = hit.point - new Vector2 (hit.collider.transform.position.x, hit.collider.transform.position.y);
                joint.distance = Vector2.Distance(transform.position, hit.point);


                line.enabled = true;
                line.SetPosition(0, transform.position);
                line.SetPosition(1, hit.point);

                isGrappling = true;

                playerScript.grapplingOnPhysicsChanges();
            }

            else
            {
                //Debug.Log("no object was hit");
            }
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            line.SetPosition(0, transform.position);
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            joint.enabled = false;

            line.enabled = false;
            playerScript.grapplingOffPhysicsChanges();
        }
    }

    public void FindMyCamera()
    {
        maincamera = FindObjectOfType<Camera>();
    }
}
