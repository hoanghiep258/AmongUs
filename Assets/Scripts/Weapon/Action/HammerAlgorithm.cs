using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerAlgorithm : WeaponAlgorithm
{
    public void OnAttackAlgorithm(object data)
    {
        // ban 
        Hammer wp = (Hammer)data;
        wp.OnShootBullet();
        wp.currentBullet--;
      
        wp.OnUpdateBullet();
        if(wp.currentBullet<=0)
        {
            wp.OnReload();
        }

    }
}
