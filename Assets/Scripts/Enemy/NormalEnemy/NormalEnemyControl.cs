using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyControl : EnemyControl
{
    public NormalEnemyDatabinding dataBinding;
    public PolyNavAgent agent;
    public Transform playerTrans;
    public CharacterHealth characterHealth;
    public NE_FSM_AttackState attackState;
    public NE_FSM_DeadState deadState;
    public NE_FSM_WalkState walkState;
    public float count_AttackRate = 3f;

    public override void OnSetup(object dataInit, int maxHP)
    {
        base.OnSetup(dataInit);


        data = (EnemyCreateData)dataInit;

        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        characterHealth = playerTrans.GetComponent<CharacterHealth>();
        Animator anim = gameObject.GetComponentInChildren<Animator>();
        //anim.GetBehaviour<ZN_Attack_StateMachineBehaviour>().zombieNormal = this;

        // Appear random on map
        agent = GetComponent<PolyNavAgent>();
        agent.map = ConfigScene.instance.polymap;
        agent.SetDestination(playerTrans.position);
        agent.OnDestinationReached += Agent_OnDestinationReached;

        walkState.parent = this;
        AddState(walkState);

        attackState.parent = this;
        AddState(attackState);

        deadState.parent = this;
        AddState(deadState);
    }

    void Agent_OnDestinationReached()
    {
        agent.OnDestinationReached -= Agent_OnDestinationReached;
        GotoState(attackState);

    }

    public override void OnSystemUpdate()
    {
        agent.SetDestination(playerTrans.position);
        count_AttackRate += Time.deltaTime;
        base.OnSystemUpdate();
    }


    public void OnEndAttack()
    {
        characterHealth.OnDamage(data.config.damage);
    }


    public override void Ondamage(int damage)
    {
        if (!isGetHit)
            return;
        currenthp -= damage;
        hubHP.OnUpdateHP(currenthp, hp);
        if (currenthp <= 0)
        {
            GotoState(deadState);
        }
        base.Ondamage(damage);
    }
}
