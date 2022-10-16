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

    public Vector3 GetRandomPos(float min, float max, float boundaryOffset)
    {
        xPos = Random.Range(min, max);
        zPos = Random.Range(min, max);

        while (Mathf.Abs(xPos) < boundaryOffset && Mathf.Abs(zPos) < boundaryOffset)
        {
            xPos = Random.Range(min, max);
            zPos = Random.Range(min, max);
        }

        cacheVector.x = xPos;
        cacheVector.y = 0;
        cacheVector.z = zPos;

        return cacheVector;
    }

    //Check if object is outside the boundary, if true, send enemy inside the boundary
    /*public void IsOutOfBound(Transform objectTf)
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
    }*/

    public void SpawnEnemy()
    {
        characterHolder = SimplePool.Spawn<Character>(enemy, GetRandomPos(-20, 20, 15), Quaternion.identity);
    }

    public void SpawnHealthBox()
    {
        Vector3 spawnPos = GetRandomPos(-12, 12, 5);
        spawnPos.y -= 0.5f;
        SimplePool.Spawn<HealthBox>(healthBox, spawnPos, Quaternion.identity);
    }
}
