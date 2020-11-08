using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class NE_FSM_DeadState : FSMState
{

	[NonSerialized]
	public NormalEnemyControl parent;
    float timerCount = 0;
    public override void OnEnter()
    {
        parent.dataBinding.Dead = true;

        base.OnEnter();
    }
    public override void OnUpdate()
    {
        timerCount += Time.deltaTime;
        if(timerCount>1)
        {
            parent.OnDead();
        }
        base.OnUpdate();
    }
}
