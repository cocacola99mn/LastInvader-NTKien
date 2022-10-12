using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : Bullet
{
    public Vector3 bulletDirect;
    float bulletSpeed;
    public override void OnInit()
    {
        existTime = 5;
        existCount = existTime;
        bulletSpeed = 6.5f;
    }

    public override void BulletUpdate()
    {
        bulletDirect = Vector3.MoveTowards(bulletTransform.position, LevelManager.Ins.player.charPos, bulletSpeed * Time.deltaTime);
        bulletTransform.position = bulletDirect;
    }

    public override void DealDamage(Collider coll)
    {
        if (coll.gameObject.CompareTag(GameConstant.PLAYER_TAG))
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
}
