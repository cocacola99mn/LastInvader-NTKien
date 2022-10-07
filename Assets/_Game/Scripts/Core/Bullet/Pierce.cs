using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pierce : Bullet
{
    public override void DealDamage(Collider coll)
    {
        if (coll.gameObject.CompareTag(GameConstant.ENEMY_TAG))
        {
            if (Cache.GetCharacter(coll) != null)
            {
                Cache.GetCharacter(coll).OnGetHit(damage);
            }
        }
        else
        {
            OnDespawn();
        }
    }
}
