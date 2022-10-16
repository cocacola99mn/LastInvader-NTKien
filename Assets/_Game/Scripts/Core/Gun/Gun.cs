using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Player player;
    public Transform shotPoints;
    public Bullet bullet, bulletHolder;

    public float fireRate;
    public int charDamage;
    public float time, coolDown;

    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        time = 0;
    }

    public void OnAttack()
    {
        time += Time.deltaTime;

        if(time >= coolDown)
        {
            time -= coolDown;
            Shoot();
        }
    }

    private void Shoot()
    {
        bulletHolder = SimplePool.Spawn<Bullet>(bullet, shotPoints.position, shotPoints.rotation);
        OnTypeSpread(bulletHolder);
        Cache.GetBullet(bulletHolder.gameObject).OnInit();
        Cache.GetBullet(bulletHolder.gameObject).damage = charDamage;
    }

    private void OnTypeSpread(Bullet bullet)
    {
        if(bullet.type.Equals(BulletType.SPREAD))
        {
            SetBulletSpreadAngle(BulletAngles.right);
            SetBulletSpreadAngle(BulletAngles.left);
            SetBulletSpreadAngle(BulletAngles.middleRight);
            SetBulletSpreadAngle(BulletAngles.middleLeft);
        }
    }

    public void SetBulletSpreadAngle(BulletAngles angles)
    {
        Spread spreadBullet = SimplePool.Spawn<Spread>(bullet, shotPoints.position, shotPoints.rotation);
        spreadBullet.OnInit();
        spreadBullet.ChangeEulerAngles(angles);
        spreadBullet.damage = charDamage;
    }
}
