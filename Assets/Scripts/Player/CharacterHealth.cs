using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CharacterHealth : MonoBehaviour {

    public event Action<int, int> OnHPChange;
    // Use this for initialization
    private int maxHP;
    private int curHP;

    public HPHub hubHP;
    public Transform anchorHub;
    private void Awake()
    {
        //Setup(20);
    }
    public void Setup(int maxHP)
    {
        this.maxHP = maxHP;
        curHP = maxHP;
        hubHP = HubControl.instance.CreateHub();
        string strName = DataAPIManager.Instance.GetName();
        hubHP.OnSetName(strName);
    }
    public void OnDamage(int damage)
    {
        curHP -= damage;
        if(OnHPChange!=null)
        {
            OnHPChange(curHP, maxHP);
            
        }
        hubHP.OnUpdateHP(curHP, maxHP);
        if (curHP <= 0)
        {
            GetComponent<CharacterDataBinding>().Dead = true;
            Time.timeScale = 0;
            DialogManager.Instance.ShowDialog(DialogIndex.GameOverDialog, new GameOverDialogParam { valueCoin = MissionControl.instance.curCoin, valueKill = MissionControl.instance.totalEnemyDead });
        }
    }

    private void LateUpdate()
    {
        if (hubHP != null)
        {
            hubHP.OnUpdatePos(anchorHub.position);
        }
        
    }

    public float PercentHP()
    {
        return (float)curHP / (float)maxHP;
    }
}
