using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Cryptography.X509Certificates;
using Random = UnityEngine.Random;

[Serializable]
public class B1_FSM_WalkState : FSMState
{

	[NonSerialized]
	public Boss1Control parent;
	private float timeWait = 0;
	private float randomTime = 2;
	public override void OnEnter ()
	{		
		timeWait = 0;
		randomTime = Random.Range(3f, 5f);
		parent.agent.SetDestination(parent.playerTrans.position);
        parent.bossAudioSource.Play();
        base.OnEnter ();
	}

	public override void OnFixedUpdate()
	{
		parent.dataBinding.Speed = 1;
		timeWait += Time.deltaTime;
		if (timeWait >= randomTime)
		{			
			parent.GotoState(parent.attackState);
		}		
	}

	public override void OnExit()
	{
        parent.bossAudioSource.Pause();
        timeWait = 0;
	}
}
