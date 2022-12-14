using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : GameUnit
{
    Character character;
    int healAmount = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstant.PLAYER_TAG))
        {
            character = Cache.GetCharacter(other);
            if(character.health < character.healthBar.maxHealth)
            {
                character.OnGetHit(-healAmount);
                SimplePool.Despawn(this);
                LevelManager.Ins.spawner.SpawnHealthBox();
            }
        }
        if (other.CompareTag(GameConstant.OBSTACLE_TAG))
        {
            SimplePool.Despawn(this);
            LevelManager.Ins.spawner.SpawnHealthBox();
        }
    }
}
