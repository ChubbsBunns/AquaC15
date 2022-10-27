using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Player_Health>().TakeDamage();
        }
        //Destroy a breakable object maybe
        Destroy(this.gameObject);
    }
}
