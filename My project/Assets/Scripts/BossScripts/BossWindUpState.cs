using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWindUpState : BossState
{
    List<int> attacks = new List<int> { 1, 2, 3 };  //1 for rock throw, 2 for power dash, 3 for ground pound
    List<int> availAttacks = new List<int>();       //This list is to keep track of what attacks havent yet been done for a set of 3 attacks
    float time;

    public Animator bossAnim;
    

    private void Start() {
    }
    

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
        int index = Random.Range(0, availAttacks.Count);
        //Debug.Log("Index is " + index);
        //Attacks after a period of time
        if(time < boss.windUpTime)
        {   
            time += Time.deltaTime;
        }
        else
        {
            Attack(boss, index);
        }
    }

    public override void BossCollision(BossStateMachine boss, Collision2D collision)
    {
    }

    private void Attack(BossStateMachine boss, int index)
    {
        //Chooses a random attack out of the 3 but always goes through all 3 attacks before repeating attacks
        if (availAttacks.Count == 0)
        {
            for(int i = 0; i < attacks.Count; ++i)
            {
                availAttacks.Add(attacks[i]);
            }
        }
        //int index = Random.Range(0, availAttacks.Count);
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
