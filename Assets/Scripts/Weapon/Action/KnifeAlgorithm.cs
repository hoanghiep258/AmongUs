using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeAlgorithm : WeaponAlgorithm
{
    public void OnAttackAlgorithm(object data)
    {
        // ban 
        Knife wp = (Knife)data;
        wp.OnShootBullet();
        wp.currentBullet--;
      
        wp.OnUpdateBullet();
        if(wp.currentBullet<=0)
        {
            wp.OnReload();
        }

    }
}
