using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRockThrowState : BossState
{
    float timeForRockThrow = 5;
    float time = 0;
    int count = 0;
    public override void BossEnterState(BossStateMachine boss)
    {
        boss.targetImage.SetActive(true);
    }

    public override void BossExitState(BossStateMachine boss)
    {
        boss.targetImage.SetActive(false);
        count = 0;
        timeForRockThrow = 5;
    }

    public override void BossTakeDamage(BossStateMachine boss)
    {
        throw new System.NotImplementedException();
    }

    public override void BossUpdate(BossStateMachine boss)
    {
        if(time < timeForRockThrow)
        {
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
        boss.ThrowRock();
        timeForRockThrow -= 1;
        ++count;
        if(count == 3)
        {
            boss.ChangeState(boss.bossChaseState);
        }
    }
}
