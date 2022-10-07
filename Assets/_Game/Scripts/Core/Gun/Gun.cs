using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform shotPoints;
    public Bullet bullet, bulletHolder;

    public float fireRate;
    float time, coolDown;

    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        time = 0;
        fireRate = 3;
        coolDown = 1 / fireRate;
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
        OnTypeSpread(bullet);
        Cache.GetBullet(bulletHolder.gameObject).OnInit();
    }

    private void OnTypeSpread(Bullet bullet)
    {
        if(Cache.GetSpread(bulletHolder.gameObject) != null)
        {
            Spread right = SimplePool.Spawn<Spread>(bullet, shotPoints.position, shotPoints.rotation);
            Spread middleRight = SimplePool.Spawn<Spread>(bullet, shotPoints.position, shotPoints.rotation);
            Spread left = SimplePool.Spawn<Spread>(bullet, shotPoints.position, shotPoints.rotation);
            Spread middleLeft = SimplePool.Spawn<Spread>(bullet, shotPoints.position, shotPoints.rotation);
            right.OnInit();
            middleRight.OnInit();
            left.OnInit();
            middleLeft.OnInit();
            right.ChangeEulerAngles(BulletAngles.right);
            left.ChangeEulerAngles(BulletAngles.left);
            middleRight.ChangeEulerAngles(BulletAngles.middleRight);
            middleLeft.ChangeEulerAngles(BulletAngles.middleLeft);
        }
    }
}
