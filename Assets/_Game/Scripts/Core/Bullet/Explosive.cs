using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : Bullet
{
    public LayerMask targetLayer;
    public Collider[] colliders;
    float exRange;

    public override void OnInit()
    {
        base.OnInit();
        exRange = 3;
    }

    public void ExplodeZone()
    {
        colliders = Physics.OverlapSphere(bulletTransform.position, exRange, targetLayer);
    }

    public void Explode()
    {
        foreach(Collider coll in colliders)
        {
            if (coll != null)
            {
                Cache.GetCharacter(coll).OnGetHit(damage);
            }
        }

        ParticlePool.Play(explodeBulletVfx, bulletTransform.position, Quaternion.Euler(0, 0, 0));
    }

    public override void BulletUpdate()
    {
        base.BulletUpdate();
        ExplodeZone();
    }

    public override void DealDamage(Collider coll)
    {
        Explode();
        OnDespawn();
    }
}
