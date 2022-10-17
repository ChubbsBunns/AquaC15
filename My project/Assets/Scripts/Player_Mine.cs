using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Mine : MonoBehaviour
{
    [SerializeField] Transform centreOfPlayerTransform; //Transform for the centre of player, assigned in inspector
    [SerializeField] LayerMask whatIsMineable;          
    [SerializeField] float radius;                      //Radius of mining
    [SerializeField] float timeBetweenMine;             //Time between mining while holding interact key
    float currentTime;


    private void Update()
    {
        if(Input.GetKey(KeyCode.E)) 
        {
            //Disable player movement
            //Play mining animation
            if(currentTime >= timeBetweenMine)
            {
                //Check for any mineable thing near player and call function on those objects
                Collider2D[] results = Physics2D.OverlapCircleAll(centreOfPlayerTransform.position, radius, whatIsMineable);
                foreach (Collider2D item in results)
                {
                    //Call mine function on item;
                    item.GetComponent<MineableObject>().Mine();
                }
                currentTime = 0;
            }
        }
        if (currentTime < timeBetweenMine)
        {
            currentTime += Time.deltaTime;
        }
    }



    //Code to draw circle
    /*
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(centreOfPlayerTransform.position, radius);
    }*/
}
