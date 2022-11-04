using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int enemy_health;
    public GameObject death_effect;
    [SerializeField] Quest quest;
    bool dead = false;


    [SerializeField] GameObject ParentIfAny = null;
    
    public virtual void Enemy_Take_Damage(int player_damage)
    {
        enemy_health -= player_damage;
        if (enemy_health <= 0)
        {
            if (dead!= true)
            {
                Instantiate(death_effect, gameObject.transform.position, Quaternion.identity);
 
                Debug.Log("This happened");
                if (quest != null && quest.goal != null)
                {
                    quest.goal.EnemyKilled();
                }

                dead = true;
                if (ParentIfAny != null)
                {
                    Destroy(ParentIfAny);
                }
                Destroy(gameObject); 
          
            }

        }
    }
}
