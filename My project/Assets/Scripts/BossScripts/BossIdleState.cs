using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossState
{
    public override void BossCollision(BossStateMachine boss, Collision2D collision)
    {
    }

    public override void BossEnterState(BossStateMachine boss)
    {
        boss.bossHealth.DisableHealthBar();
        boss.rb.velocity = Vector2.zero;
        boss.rb.isKinematic = true;
        boss.bossCollider.enabled = false;
    }

    public override void BossExitState(BossStateMachine boss)
    {
        boss.rb.isKinematic = false;
        boss.bossCollider.enabled = true;
    }

    public override void BossTakeDamage(BossStateMachine boss)
    {
    }

    public override void BossUpdate(BossStateMachine boss)
    {
    }
}
