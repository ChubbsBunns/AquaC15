using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWindUpState : BossState
{
    List<int> attacks = new List<int> { 1, 2, 3 };
    List<int> availAttacks = new List<int>();
    float time;
    public override void BossEnterState(BossStateMachine boss)
    {
        time = 0;
    }

    public override void BossExitState(BossStateMachine boss)
    {
    }

    public override void BossTakeDamage(BossStateMachine boss)
    {
    }

    public override void BossUpdate(BossStateMachine boss)
    {
        if(time < boss.windUpTime)
        {
            time += Time.deltaTime;
        }
        else
        {
            Attack(boss);
        }
    }

    private void Attack(BossStateMachine boss)
    {
        if (availAttacks.Count == 0)
        {
            availAttacks = attacks;
        }
        int index = 1; //Random.Range(0, availAttacks.Count);
        switch (index)
        {
            case 1:
                boss.ChangeState(boss.bossRockThrowState);
                break;
            case 2:
                break;
            case 3:
                break;
        }
        //availAttacks.Remove(availAttacks[index]);
    }
}
