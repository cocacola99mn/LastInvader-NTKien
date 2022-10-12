using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    public ParticleSystem explodeBulletVfx;
    public Transform bulletTransform;
    public BulletType type;
    public Rigidbody rb;

    public float existCount, existTime;
    public int damage;

    private void FixedUpdate()
    {
        OnUpdate();
    }

    private void OnTriggerEnter(Collider coll)
    {
        DealDamage(coll);
    }

    public virtual void OnInit()
    {
        rb.velocity = bulletTransform.forward * 20f;
        existTime = 2;
        existCount = existTime;
    }

    public virtual void OnUpdate()
    {
        LifeTime();
        BulletUpdate();
    }

    public virtual void BulletUpdate() { }

    public void LifeTime()
    {
        existCount -= Time.deltaTime;
        if(existCount <= 0)
        {
            OnDespawn();
        }
    }

    public virtual void OnHit(Collider coll)
    {
        DealDamage(coll);
    }

    public virtual void DealDamage(Collider coll)
    {
        if (coll.gameObject.CompareTag(GameConstant.ENEMY_TAG))
        {
            if (Cache.GetCharacter(coll) != null)
            {
                Cache.GetCharacter(coll).OnGetHit(damage);
                OnDespawn();
            }
        }
        else
        {
            OnDespawn();
        }
    }

    public virtual void OnDespawn(){
        SimplePool.Despawn(this);
        existCount = existTime;
    }
}
