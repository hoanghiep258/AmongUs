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
    public GameObject goCoin;
    public int percentSpawnItem = 60;
    public GameObject goItem;
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
        
        GameObject goObject = Instantiate(goCoin, MissionControl.instance.transform);
        goObject.transform.position = trans.position;
        int rand = UnityEngine.Random.Range(0, 100);
        if (rand > percentSpawnItem)
        {
            goObject = Instantiate(goItem, MissionControl.instance.transform);
            goObject.transform.position = trans.position;
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
