using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject impactEffect;


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
            } else
            { 
                Debug.Log("Impact with " + hitInfo.name);
            }

        Instantiate(impactEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
