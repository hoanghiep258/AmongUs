using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Control : EnemyControl
{
    public Boss1Databinding dataBinding;
    public PolyNavAgent agent;
    public Transform playerTrans;
    public CharacterHealth characterHealth;
    public B1_FSM_AttackState attackState;
    public B1_FSM_DeadState deadState;
    public B1_FSM_WalkState walkState;
    public float count_AttackRate = 3f;
    public GameObject goHand;
    public float speed = 3;
    public GameObject goBullet;
    public int indexBoss;

    [SerializeField]
    private List<GameObject> lsHands = new List<GameObject>();
    [SerializeField]
    private List<GameObject> lsgoBullets = new List<GameObject>();
    public RuntimeAnimatorController defaultAnimator;

    public List<AnimatorOverrideController> lsSkinAnimator;

    public AudioSource bossAudioSource;
    
    public override void OnSetup(object dataInit, int maxHP)
    {
        base.OnSetup(dataInit, maxHP);        

        data = (EnemyCreateData)dataInit;

        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        characterHealth = playerTrans.GetComponent<CharacterHealth>();
        Animator anim = gameObject.GetComponentInChildren<Animator>();
        //anim.GetBehaviour<ZN_Attack_StateMachineBehaviour>().zombieNormal = this;

        // Appear random on map
        agent = GetComponent<PolyNavAgent>();
        agent.map = ConfigScene.instance.polymap;        
        agent.OnDestinationReached += Agent_OnDestinationReached;

        walkState.parent = this;
        AddState(walkState);

        attackState.parent = this;
        AddState(attackState);

        deadState.parent = this;
        AddState(deadState);
        goHand.SetActive(false);
    }


    public void OnSetupBoss(int indexBoss)
    {
        this.indexBoss = indexBoss;
        goBullet = lsgoBullets[indexBoss];
        goHand = lsHands[indexBoss];
        speed += (indexBoss / 2);
        if (indexBoss == 0)
        {
            dataBinding.animator.runtimeAnimatorController = defaultAnimator;
        }
        else
        {
            dataBinding.animator.runtimeAnimatorController = lsSkinAnimator[indexBoss -1];
        }
    }
    void Agent_OnDestinationReached()
    {
        agent.OnDestinationReached -= Agent_OnDestinationReached;
        characterHealth.OnDamage(500);

    }

    public override void OnSystemUpdate()
    {
        agent.SetDestination(playerTrans.position);
        count_AttackRate += Time.deltaTime;
        base.OnSystemUpdate();
    }


    public void OnEndAttack()
    {        
        characterHealth.OnDamage(5);
        goHand.SetActive(false);
        agent.maxSpeed = speed;
        GotoState(walkState);        
    }

    public void OnShoot()
    {
        Transform bullet = Instantiate(goBullet).transform;
        bullet.position = trans.position;
        Vector3 dir = playerTrans.position - bullet.position;
        dir.Normalize();
        
        if (indexBoss < 2)
        {
            bullet.up = dir;
            bullet.GetComponent<BulletEnemy>().Setup(dir, 20);
        }
        else
        {
            bullet.SetParent(trans);
            bullet.position = Vector3.zero;
            bullet.GetComponent<Lazer>().Setup(goHand.transform.position, 20);
        }
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
