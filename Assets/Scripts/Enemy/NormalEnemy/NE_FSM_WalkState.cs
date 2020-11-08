using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Cryptography.X509Certificates;

[Serializable]
public class NE_FSM_WalkState : FSMState
{

	[NonSerialized]
	public NormalEnemyControl parent;

	public override void OnEnter ()
	{
		
		base.OnEnter ();
	}
    
	public override void OnExit ()
	{
        parent.agent.SetDestination(parent.trans.position);
		parent.dataBinding.Speed = 0;
		base.OnExit ();
	}
}
