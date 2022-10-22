using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGroundPoundState : BossState
{
    float time = 0;
    int count = 0;

    public override void BossCollision(BossStateMachine boss, Collision2D collision)
    {
    }

    public override void BossEnterState(BossStateMachine boss)
    {
        //Pound the ground on enter since wind up time was already gone through in wind up state
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
        //Pound the ground after a period
        if(time < boss.timeBetweenGroundPounds)
        {
            time += Time.deltaTime;
        }
        else
        {
            GroundPound(boss);
        }
    }

    //Adds to the count for limiting number of ground pounds
    void GroundPound(BossStateMachine boss)
    {
        time = 0;
        ++count;
        if(count >= boss.numberOfGroundPounds)
        {
            boss.ChangeState(boss.bossChaseState);
        }
        boss.GroundPound(count);
    }
}
