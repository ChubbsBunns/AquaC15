using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Mine : MonoBehaviour
{
    [SerializeField] Transform centreOfPlayerTransform; //Transform for the centre of player, assigned in inspector
    [SerializeField] LayerMask whatIsMineable;          
    [SerializeField] float radius;                      //Radius of mining

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)) //Temporary use of T, could be any key
        {
            //Check for any mineable thing near player and call function on those objects
            Collider2D[] results = Physics2D.OverlapCircleAll(centreOfPlayerTransform.position, radius, whatIsMineable);
            foreach(Collider2D item in results)
            {
                //Call mine function on item;
                item.GetComponent<MineableObject>().Mine();
            }
        }
    }

    //Code to draw circle
    /*
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(centreOfPlayerTransform.position, radius);
    }*/
}
