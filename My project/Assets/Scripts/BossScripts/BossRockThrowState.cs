using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRockThrowState : BossState
{
    float timeForRockThrow;
    float time = 0;
    int count = 0;
    public override void BossEnterState(BossStateMachine boss)
    {
        boss.targetImage.SetActive(true);
        timeForRockThrow = boss.timeForRockThrow;
    }

    public override void BossExitState(BossStateMachine boss)
    {
        boss.targetImage.SetActive(false);
        count = 0;
    }

    public override void BossTakeDamage(BossStateMachine boss)
    {
    }

    public override void BossUpdate(BossStateMachine boss)
    {
        Debug.Log("I am rock throw");
        //Moves target image to player and throws a rock after a period
        if(time < timeForRockThrow)
        {
            boss.BossAnim.SetBool("RockThrow", false);
            boss.BossAnim.SetBool("RockThrowPrep", true);
            time += Time.deltaTime;
            boss.targetImage.transform.position = Vector2.Lerp(boss.targetImage.transform.position, boss.player.position, time / timeForRockThrow);
        }
        else
        {
            ThrowRock(boss);
            time = 0;
        }
    }

    public override void BossCollision(BossStateMachine boss, Collision2D collision)
    {
    }

    void ThrowRock(BossStateMachine boss)
    {
        //Decreases time for next throw and increases count of rocks thrown this attack
        boss.BossAnim.SetBool("RockThrow", true);
        boss.ThrowRock();    
        timeForRockThrow -= boss.timeDecrementBetweenThrows;
        ++count;
        if(count == boss.numRocksPerAttack)
        {
            boss.BossAnim.SetBool("RockThrowPrep", false);
            boss.BossAnim.SetBool("RockThrow", false);
            boss.ChangeState(boss.bossChaseState);
        }
        //boss.BossAnim.SetBool("RockThrow", false);
    }
}
