using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(1, 3);
    }


    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        Player_Health player = hitInfo.transform.GetComponent<Player_Health>();
            if (player != null)
            {
                Debug.Log("Impact with Player");
                player.TakeDamage();
                Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            } else if (hitInfo.gameObject.name == "Tilemap")
            { 
                Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
    }
}
