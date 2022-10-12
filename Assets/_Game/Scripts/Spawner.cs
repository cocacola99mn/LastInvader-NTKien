using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Character enemy, boss, characterHolder;
    public HealthBox healthBox, healthBoxHolder;
    public Transform player, spawner;

    [SerializeField]
    float xPos, zPos;
    public int enemyNum;
    private Vector3 cacheVector;

    public Vector3 GetRandomPosition(float min, float max)
    {
        xPos = Random.Range(min, max);
        zPos = Random.Range(min, max);
        //Avoid enemy spawn too near player
        while (xPos < 5 && zPos < 5 && xPos > -5 && zPos > -5)
        {
            xPos = Random.Range(min, max);
            zPos = Random.Range(min, max);
        }

        cacheVector.x = xPos;
        cacheVector.y = 0;
        cacheVector.z = zPos;
        cacheVector = player.position + cacheVector;

        return cacheVector;
    }

    //Check if object is outside the boundary, if true, send enemy inside the boundary
    public void IsOutOfBound(Transform objectTf)
    {
        Vector3 localPos = objectTf.localPosition;
        Vector3 newPos;
        float boundValue = 14;
        float newPosValue = 8;

        if (localPos.x > boundValue || localPos.x < -boundValue || localPos.z > boundValue || localPos.z < -boundValue)
        {
            newPos.x = Random.Range(-newPosValue, newPosValue);
            newPos.y = 0;
            newPos.z = Random.Range(-newPosValue, newPosValue);
            objectTf.localPosition = newPos;
        }
    }

    public void SpawnEnemy()
    {
        characterHolder = SimplePool.Spawn<Character>(enemy, GetRandomPosition(-10, 10), Quaternion.identity);
        IsOutOfBound(characterHolder.transform);
    }

    public void SpawnHealthBox()
    {
        Vector3 spawnPos = GetRandomPosition(-7, 7);
        spawnPos.y -= 0.5f;
        SimplePool.Spawn<HealthBox>(healthBox, spawnPos, Quaternion.identity);
    }
}
