using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : Bullet
{
    public LayerMask targetLayer;
    Collider[] colliders;
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
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != null)
            {
                Cache.GetCharacter(colliders[i]).OnGetHit(damage);
            }
        }
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
