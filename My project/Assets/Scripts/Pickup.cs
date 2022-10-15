using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] int value = 1;         //Value of this pickup for player money variable

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(rb.velocity.y > 0) { return; }
        if(collision.CompareTag("Player"))
        {
            MoneyCanvas.instance.IncreaseMoney(value);
            Destroy(this.gameObject);
        }
    }
}
