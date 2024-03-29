﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class B1_FSM_DeadState : FSMState
{

	[NonSerialized]
	public Boss1Control parent;
    float timerCount = 0;
    public override void OnEnter()
    {        
        timerCount = 0;
        parent.dataBinding.Dead = true;
        parent.GetComponent<CircleCollider2D>().enabled = false;
        SoundManager.instance.PlaySound(SoundIndex.Boss_kill);
        base.OnEnter();
    }
    public override void OnUpdate()
    {
        timerCount += Time.deltaTime;        
        if (timerCount > 1f)
        {            
            parent.OnDead();            
        }
        base.OnUpdate();
    }
}
