using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CharacterHealth : MonoBehaviour {

    public event Action<int, int> OnHPChange;
    // Use this for initialization
    private int maxHP;
    private int curHP;
    private void Awake()
    {
        Setup(20);
    }
    public void Setup(int maxHP)
    {
        this.maxHP = maxHP;
        curHP = maxHP;
    }
    public void OnDamage(int damage)
    {
        curHP -= damage;
        if(OnHPChange!=null)
        {
            OnHPChange(curHP, maxHP);
        }

        if (curHP <= 0)
        {
            Debug.LogError("Player dead");
        }
    }
}
