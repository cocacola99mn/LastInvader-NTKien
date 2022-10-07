using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : Bullet
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(GameConstant.BULLET_TAG))
        {
            OnDespawn();
        }

        Vector3 direction = Vector3.Reflect(bulletTransform.forward, collision.GetContact(0).normal);
        rb.velocity = direction * 20f;
        DealDamage(collision.collider);
    }

    public override void DealDamage(Collider coll)
    {
        if (coll.gameObject.CompareTag(GameConstant.ENEMY_TAG))
        {
            if (Cache.GetCharacter(coll) != null)
            {
                Cache.GetCharacter(coll).OnGetHit(damage);
                OnDespawn();
            }
        }
        else if (coll.gameObject.CompareTag(GameConstant.PLAYER_TAG))
        {
            OnDespawn();
        }
    }

}
