using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GameConstant.ENEMY_TAG))
        {
            if (!Cache.GetCharacter(other).isBoss)
            {
                SimplePool.Despawn(Cache.GetCharacter(other));
                LevelManager.Ins.spawner.SpawnEnemy();
            }
        }
    }
}
