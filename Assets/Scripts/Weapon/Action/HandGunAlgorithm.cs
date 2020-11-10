using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGunAlgorithm : WeaponAlgorithm
{
    public void OnAttackAlgorithm(object data)
    {
        // ban 
        HandGun wp = (HandGun)data;
        wp.OnShootBullet();
        wp.currentBullet--;
      
        wp.OnUpdateBullet();
        if(wp.currentBullet<=0)
        {
            wp.OnReload();
        }

    }
}
