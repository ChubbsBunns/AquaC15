using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    public float timeAlive = 8f;
    public float maxTimeBeforeFalling;
    public Rigidbody2D rb;
    private void Start()
    {
        StartCoroutine(Fall());
        rb.gravityScale = 0;
    }

    IEnumerator Fall()
    {
        float time = Random.Range(0f,maxTimeBeforeFalling);
        yield return new WaitForSeconds(time);
        rb.gravityScale = 1;
        yield return new WaitForSeconds(timeAlive);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Player_Health>().TakeDamage();
            Destroy(this.gameObject);
        }
    }

}
