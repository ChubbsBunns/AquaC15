using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : Enemy_Health
{
    [SerializeField] BossStateMachine bossStateMachine;
    [SerializeField] Slider healthSlider;
    [SerializeField] GameObject healthBarObject;

    private void Start()
    {
        healthBarObject.SetActive(false);
    }

    public void EnableHealthBar()
    {
        healthBarObject.SetActive(true);
        healthSlider.value = enemy_health;
    }

    public void DisableHealthBar()
    {
        healthBarObject.SetActive(false);
    }
    public override void Enemy_Take_Damage(int player_damage)
    {
        enemy_health -= player_damage;
        //Update some UI
        healthSlider.value = enemy_health;
        if(enemy_health <= 0)
        {
            bossStateMachine.ChangeState(bossStateMachine.bossIdleState);
        }
    }
}
