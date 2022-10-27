using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int enemy_health;
    public GameObject death_effect;
    [SerializeField] Quest quest;
    

    public virtual void Enemy_Take_Damage(int player_damage)
    {
        enemy_health -= player_damage;
        if (enemy_health <= 0)
        {
            Instantiate(death_effect, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            quest.goal.EnemyKilled();
        }
    }
}
