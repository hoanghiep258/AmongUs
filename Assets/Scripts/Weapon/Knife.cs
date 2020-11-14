using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : WeaponBehaviour {

    public Transform bulletPrefab;
    public Transform posShoot;
    public Transform aimPos;
    private void Start()
    {
        this.weaponAlgorithm = new KnifeAlgorithm();
    }

    protected override void Setup()
    {
        this.weaponAlgorithm = new KnifeAlgorithm();
    }
    public void OnShootBullet()
    {
        CallEventShoot();
        Transform bullet = Instantiate(bulletPrefab);
        posShoot = MissionControl.instance.enemyKdTree.FindClosest(aimPos.position);
        if (posShoot != null)
        {
            bullet.position = aimPos.position;
            Vector3 dir = posShoot.position - aimPos.position;
            dir.Normalize();
            bullet.up = dir;

            bullet.GetComponent<BulletPlayer>().Setup(dir, 20);
        }
    }
}
