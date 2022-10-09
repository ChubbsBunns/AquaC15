using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth;
    public GameObject deathEffect;
    public GameObject takeDamageEffect;

    public BlackScreen blackScreen;

    public string SceneNameToLoadIfBossIsDead;
    // Start is called before the first frame update
    void Start()
    {
        blackScreen = FindObjectOfType<BlackScreen>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enemyTakeDamage(int playerDamage)
    {
        enemyHealth -= playerDamage;
        Instantiate(takeDamageEffect, gameObject.transform.position, Quaternion.identity);
        if (enemyHealth <= 0)
        {
            Instantiate(deathEffect, gameObject.transform.position, Quaternion.identity);
            Debug.Log("this is fully working");
            blackScreen.SetSceneName(SceneNameToLoadIfBossIsDead);
            blackScreen.StartLoadSceneAnimation();
        }
    }
}
