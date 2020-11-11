using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class B1_FSM_AttackState : FSMState
{

	[NonSerialized]
	public Boss1Control parent;
	private float timer;
    private bool isShoot;
	public override void OnEnter ()
	{		
		parent.goHand.SetActive(true);
		parent.agent.maxSpeed = 0;
		parent.dataBinding.Speed = 0;
		timer = 0;
        isShoot = false;

        base.OnEnter ();
	}

	public override void OnFixedUpdate ()
	{
		timer += Time.deltaTime;		                    

		if (timer > 1)
        {
			parent.OnEndAttack();
			timer = -1000;

		}
        if (isShoot)
            return;

        if (timer > 0.5f)
        {
            isShoot = true;
            parent.OnShoot();
        }
		base.OnFixedUpdate ();
	}
    public override void OnExit()
    {
		base.OnExit();

    }
}
