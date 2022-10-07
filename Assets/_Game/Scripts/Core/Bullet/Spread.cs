using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletAngles
{
    right = 2,
    middleRight = 1,
    left = -2,
    middleLeft = -1
}

public class Spread : Bullet
{
    Vector3 angle;

    public override void OnInit()
    {
        damage = 10;
        existTime = 0.4f;
        existCount = existTime;
        angle = new Vector3(0, 10, 0);
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
        else if (coll.gameObject.CompareTag(GameConstant.OBSTACLE_TAG))
        {
            OnDespawn();
        }
    }

    public void ChangeEulerAngles(BulletAngles bulletAngles)
    {
        angle.y = 10 * (int)bulletAngles;
        bulletTransform.eulerAngles += angle;
    }

    public override void BulletUpdate()
    {
        base.BulletUpdate();
        bulletTransform.Translate(Vector3.forward * Time.deltaTime * 20f);
    }
}
