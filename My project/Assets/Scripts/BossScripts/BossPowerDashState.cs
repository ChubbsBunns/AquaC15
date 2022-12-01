using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPowerDashState : BossState
{
    float time = 0;
    public override void BossEnterState(BossStateMachine boss)
    {
        boss.BossAnim.SetBool("Charge", true);
        Debug.Log("I am charging");
        //Pushes boss towards the player on enter state
        time = 0;
        boss.rb.gravityScale = 0;
        boss.rb.velocity = (boss.player.position - boss.transform.position).normalized * boss.powerDashSpeed;
    }

    public override void BossExitState(BossStateMachine boss)
    {
    }

    public override void BossTakeDamage(BossStateMachine boss)
    {
    }

    public override void BossUpdate(BossStateMachine boss)
    {
  
        //On the boss being grounded, returns the boss to chase state
        if(time < 2)
        {
            time += Time.deltaTime;
        }
        else
        {
            boss.rb.gravityScale = 1;
            if(boss.grounded)
            {
                boss.BossAnim.SetBool("Charge", false);
                boss.ChangeState(boss.bossChaseState);
            }
        }
        
    }

    public override void BossCollision(BossStateMachine boss, Collision2D collision)
    {
        //On collision also returns the boss to chase state
        boss.rb.gravityScale = 1;
        if(boss.grounded)
        {
        //    boss.BossAnim.SetBool("Charge", false);
        //    boss.ChangeState(boss.bossChaseState);
        }
    }
}

