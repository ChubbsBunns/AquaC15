using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestDummy : MonoBehaviour
{
    public float enemyHealth;
    public GameObject deathEffect;
    public GameObject takeDamageEffect;


    public string SceneNameToLoadIfBossIsDead;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void enemyTakeDamage(int playerDamage)
    {
        Debug.Log("this is working 2");
        enemyHealth -= playerDamage;
        Instantiate(takeDamageEffect, gameObject.transform.position, Quaternion.identity);
        if (enemyHealth <= 0)
        {
            Debug.Log("gsfsad");
            Instantiate(deathEffect, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }
}