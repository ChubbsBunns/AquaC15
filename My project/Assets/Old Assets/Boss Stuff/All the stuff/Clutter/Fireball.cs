using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Rigidbody2D rb;
    public int damageFireballDealsToPlayer;
    public float speed;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetVelocity(Vector2 velocity)
    {
        rb.velocity = new Vector2(velocity.x*speed, velocity.y*speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController_2 playerGameObject = collision.gameObject.GetComponent<PlayerController_2>();
            playerGameObject.TakeDamage(damageFireballDealsToPlayer);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
