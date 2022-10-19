using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Rock Hit you");
            //Do some damage to player knockback something
        }
        //Destroy a breakable object maybe
        Destroy(this.gameObject);
    }
}
