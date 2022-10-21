using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGroundPoundState : BossState
{
    float time = 0;
    float timeBetweenGroundPounds = 6f;
    int numberOfGroundPounds = 3;
    int count = 0;

    public override void BossCollision(BossStateMachine boss, Collision2D collision)
    {
    }

    public override void BossEnterState(BossStateMachine boss)
    {
        count = 0;
        GroundPound(boss);
    }

    public override void BossExitState(BossStateMachine boss)
    {
    }

    public override void BossTakeDamage(BossStateMachine boss)
    {
    }

    public override void BossUpdate(BossStateMachine boss)
    {
        if(time < timeBetweenGroundPounds)
        {
            time += Time.deltaTime;
        }
        else
        {
            GroundPound(boss);
        }
    }

    void GroundPound(BossStateMachine boss)
    {
        time = 0;
        ++count;
        if(count >= numberOfGroundPounds)
        {
            boss.ChangeState(boss.bossChaseState);
        }
        boss.GroundPound(count);
    }
}
