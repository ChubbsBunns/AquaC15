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
//        Debug.Log("I am ground pound");
        //Pound the ground after a period
        if(time < boss.timeBetweenGroundPounds)
        {
            boss.BossAnim.SetBool("GroundPoundPrep", true);
            time += Time.deltaTime;
        }
        else
        {
            GroundPound(boss);
            //boss.BossAnim.SetBool("GroundPound", false);
        }
    }

    //Adds to the count for limiting number of ground pounds
    void GroundPound(BossStateMachine boss)
    {
        time = 0;
        boss.BossAnim.SetBool("GroundPound", true);
        ++count;
        if(count >= boss.numberOfGroundPounds)
        {                
            boss.BossAnim.SetBool("GroundPound", false);
            boss.BossAnim.SetBool("GroundPoundPrep", false);
            boss.ChangeState(boss.bossChaseState);
        }
        boss.GroundPound(count);
    }
}
