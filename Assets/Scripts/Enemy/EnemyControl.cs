using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreateData
{
    public ConfigEnemyData config;
}

public class EnemyControl : FSMSystem
{
    public event Action<EnemyControl> OnEnemyDead;
    public EnemyCreateData data;
    public int hp;
    public int currenthp;
    public Transform anchorHub;
    protected HPHub hubHP;
    public Transform trans;
    public bool isGetHit = false;

    private void Awake()
    {
        trans = transform;
    }

    public virtual void OnSetup(object dataInit, int maxHp = 1)
    {

        hp = maxHp;
        currenthp = hp;
        // tao hub enemy 
        hubHP = HubControl.instance.CreateHub();
    }

    public virtual void Ondamage(int damage)
    {

    }
    public void OnDead()
    {        
        if (OnEnemyDead != null)
        {
            OnEnemyDead(this);
        }
        Destroy(hubHP.gameObject);
        Destroy(gameObject);
    }
    public override void OnSystemLateUpdate()
    {
        hubHP.OnUpdatePos(anchorHub.position);
        base.OnSystemLateUpdate();
    }
    private void OnBecameInvisible()
    {
        isGetHit = false;
    }

    private void OnBecameVisible()
    {
        isGetHit = true;
    }

}
