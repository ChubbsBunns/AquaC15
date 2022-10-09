using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    public DistanceJoint2D joint;
    public Vector3 targetPos;
    public Camera mainCamera;

    public RaycastHit2D hit;
    float maxDistanceOfGrapple;
    public LayerMask thingsICanGrappleToMask;

    public float grappleStep;
    //grapple min distance is the joint.distance that the grapple will push the player towards, e.g. max distance is 10, grappleMin is 1, joint.distance will decrease (this shortens the grapple by applying a force to the gameobject to the origin point) until 1
    public float grappleMinDistance;

    // Start is called before the first frame update
    void Start()
    {
        
        joint.GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        mainCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //targetPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;
            //hit = Physics2D.Raycast(transform.position, targetPos - transform.position, distance, mask);
            hit = Physics2D.Raycast(transform.position, targetPos - transform.position, maxDistanceOfGrapple, thingsICanGrappleToMask);
            if (hit == false)
            {
                Debug.Log("hit is also false");
            }
            if (hit.collider == null)
            {
                Debug.Log("HELPPP IM HITTING NOTHINGGG");
            }
            else
            {
                Debug.Log("YEAHHHH FINALLY SHITFACE");
            }

            if (hit.collider!= null && hit.collider.gameObject.GetComponent<Rigidbody2D>()!= null)
            {
                Debug.Log(hit.collider.gameObject);
                joint.enabled = true;
                joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                Debug.Log("this is working 4");
                joint.distance = Vector2.Distance(transform.position, hit.point);
                Debug.Log("this is working 5");
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            joint.enabled = false;
        }
    }
}
