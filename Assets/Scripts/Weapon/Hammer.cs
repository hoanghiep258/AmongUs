using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : WeaponBehaviour {

    public Transform bulletPrefab;
    public Transform posShoot;
    public Transform aimPos;
    private void Start()
    {
        this.weaponAlgorithm = new HammerAlgorithm();
    }

    protected override void Setup()
    {
        this.weaponAlgorithm = new HammerAlgorithm();
    }
    public void OnShootBullet()
    {
        CallEventShoot();
        Transform bullet = Instantiate(bulletPrefab);
        posShoot = MissionControl.instance.enemyKdTree.FindClosest(aimPos.position);
        bullet.position = aimPos.position;
        Vector3 dir = posShoot.position - aimPos.position;
        dir.Normalize();
        bullet.up = dir;

        bullet.GetComponent<BulletPlayer>().Setup(dir, 20);
    }
}
