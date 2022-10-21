using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPowerDashState : BossState
{
    float time = 0;
    public override void BossEnterState(BossStateMachine boss)
    {
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
        if(time < 1)
        {
            time += Time.deltaTime;
        }
        else
        {
            boss.rb.gravityScale = 1;
            if(boss.grounded)
            {
                boss.ChangeState(boss.bossChaseState);
            }
        }
        
    }

    public override void BossCollision(BossStateMachine boss, Collision2D collision)
    {
        boss.rb.gravityScale = 1;
        if(boss.grounded)
        {
            boss.ChangeState(boss.bossChaseState);
        }
    }
}

