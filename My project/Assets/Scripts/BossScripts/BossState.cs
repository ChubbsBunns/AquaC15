using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState
{
    public abstract void BossUpdate(BossStateMachine boss);

    public abstract void BossTakeDamage(BossStateMachine boss);

    public abstract void BossEnterState(BossStateMachine boss);

    public abstract void BossExitState(BossStateMachine boss);

}
