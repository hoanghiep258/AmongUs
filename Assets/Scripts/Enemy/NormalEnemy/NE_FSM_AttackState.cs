using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class NE_FSM_AttackState : FSMState
{

	[NonSerialized]
	public NormalEnemyControl parent;

	public override void OnEnter ()
	{		
        parent.OnEndAttack();
		base.OnEnter ();
	}

	public override void OnFixedUpdate ()
	{
		if (parent.count_AttackRate >= 3) {
			//parent.dataBinding.Attack = true;
			parent.GotoState(parent.deadState);
			parent.count_AttackRate = 0;
		}
		base.OnFixedUpdate ();
	}
}
