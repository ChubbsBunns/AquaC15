using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shockwave : MonoBehaviour
{
    public int damageIDealToPlayer;
    public Rigidbody2D rb;
    public float moveSpeed;

    public float shockwaveSize;
    public float shockwaveSizeIncrement;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        shockwaveSize += shockwaveSizeIncrement;
        transform.localScale = new Vector3(1, shockwaveSize, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController_2 playerGameObject = collision.gameObject.GetComponent<PlayerController_2>();
            playerGameObject.TakeDamage(damageIDealToPlayer);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController_2 playerGameObject = collision.gameObject.GetComponent<PlayerController_2>();
            playerGameObject.TakeDamage(damageIDealToPlayer);
        }
    }

    public void move(bool left_)
    {
        rb = FindObjectOfType<Rigidbody2D>();
        if (left_)
        {
            rb.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, 0f);
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

    }
}
