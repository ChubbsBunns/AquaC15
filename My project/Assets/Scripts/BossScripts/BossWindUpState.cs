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

    public override void BossCollision(BossStateMachine boss, Collision2D collision)
    {
    }

    private void Attack(BossStateMachine boss)
    {
        if (availAttacks.Count == 0)
        {
            for(int i = 0; i < attacks.Count; ++i)
            {
                availAttacks.Add(attacks[i]);
            }
        }
        int index = Random.Range(0, availAttacks.Count);
        switch (availAttacks[index])
        {
            case 1:
                boss.ChangeState(boss.bossRockThrowState);
                break;
            case 2:
                boss.ChangeState(boss.bossPowerDashState);
                break;
            case 3:
                boss.ChangeState(boss.bossGroundPoundState);
                break;
        }
        availAttacks.Remove(availAttacks[index]);
    }
}
