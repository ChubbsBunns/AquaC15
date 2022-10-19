using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseState : BossState
{
    float time = 0;

    // Start is called before the first frame update
    public override void BossUpdate(BossStateMachine boss)
    {
        if(time < boss.timeBetweenAttacks)
        {
            time += Time.deltaTime;
        }
        else
        {
            boss.ChangeState(boss.bossWindUpState);
        }
        Vector2 dir = boss.player.transform.position - boss.transform.position;
        if(dir.x < 0)
        {
            //Boss towards the right
            boss.rb.velocity = new Vector2(-boss.bossSpeed, 0);
        }
        else
        {
            //Boss towards the left
            boss.rb.velocity = new Vector2(boss.bossSpeed, 0);
        }
        //Likely here access some animator to change the bosses animation duh.
    }


    public override void BossTakeDamage(BossStateMachine boss)
    {
        
    }
    public override void BossEnterState(BossStateMachine boss)
    {
        time = 0;
    }

    public override void BossExitState(BossStateMachine boss)
    {
        
    }

    
}
