using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageProjectile : MonoBehaviour
{
    // this is a script to be attached to anything like a "bullet" or a projectile

    //damage dealt to the player
    public int damageIDealToPlayer;

    //how long the projectile will last
    public float projectileLifeSpan;

    public GameObject projectileDeathEffect;

    private void Start()
    {
        StartCoroutine(SelfDestructTimer());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController_2 playerGameObject = collision.gameObject.GetComponent<PlayerController_2>();
            playerGameObject.TakeDamage(damageIDealToPlayer);

            Instantiate(projectileDeathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    IEnumerator SelfDestructTimer()
    {
        yield return new WaitForSeconds(projectileLifeSpan);
        Instantiate(projectileDeathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
