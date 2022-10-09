using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemy : MonoBehaviour
{
    public Swordsman swordsMan;
    public int damageSwordDealToPlayer;

    public float secondsToWait;

    // Start is called before the first frame update
    void Start()
    {
        swordsMan = FindObjectOfType<Swordsman>();
        damageSwordDealToPlayer = swordsMan.damageIDealToPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == swordsMan.SwordOriginalPosition.position && swordsMan.SwordGoBack)
        {
            swordsMan.animSwordEnemy.SetBool("SwordReturnNow", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController_2 playerGameObject = collision.gameObject.GetComponent<PlayerController_2>();
            playerGameObject.TakeDamage(damageSwordDealToPlayer);
        }

        if (swordsMan.swordMoving)
        {
            StartCoroutine(WaitForSeconds());

        }
    }

    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(secondsToWait);
        swordsMan.swordMoving = false;
        swordsMan.SwordGoBack = true;
    }

}
