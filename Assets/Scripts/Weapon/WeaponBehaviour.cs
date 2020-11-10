using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GunData
{    
    public int amoutAmo;    
}
public delegate void ShootHandle();
public delegate void ReloadHandle(float timereload, Action callBack);
public delegate void UpdateBulletHandle(int currentNUmber, int total);
public abstract class WeaponBehaviour : MonoBehaviour
{
    public event ShootHandle OnShootHandle;
    public event ReloadHandle OnReloadHandle;
    public event UpdateBulletHandle OnUpdateBulletHandle;

    public const float rof = 1;        
    public int amoutAmo;
    public const int damage = 1;
    public int currentBullet;

    private float timer;

    public bool isFire = false;
    public WeaponAlgorithm weaponAlgorithm;
    public AnimatorOverrideController animatorOverrideController;
    public GunData gunData;
    // Use this for initialization
    /// <summary>
    /// Setup the specified data. Force child class " new WeaponAlgorithm"
    /// </summary>
    /// <param name="data">Data.</param>
    /// 
    protected abstract void Setup();
    public void SetupData(GunData data)
    {
        this.gunData = data;        
        amoutAmo = gunData.amoutAmo;
        currentBullet = amoutAmo;        
        Setup();
    }


    private void Update()
    {
        transform.localRotation = Quaternion.identity;
        timer += Time.deltaTime;
        isFire = CharacterInput.isFire;
        if (isFire && timer >= rof)
        {
            timer = 0;
            CharacterInput.isFire = false;
            weaponAlgorithm.OnAttackAlgorithm(this);

        }
        OnUpdate();
    }
    public virtual void OnUpdate()
    {

    }
    public void OnUpdateBullet()
    {
        if (OnUpdateBulletHandle != null)
        {
            OnUpdateBulletHandle(currentBullet, amoutAmo);
        }
    }
    public void OnReload()
    {
        CharacterInput.isFire = false;
    }
    public void CallEventShoot()
    {
        if (OnShootHandle != null)
        {
            OnShootHandle();
        }
    }
}
