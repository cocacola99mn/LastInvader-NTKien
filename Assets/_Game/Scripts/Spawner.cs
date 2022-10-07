using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Character character, characterHolder;

    public Transform player, spawner;

    [SerializeField]
    float xPos, zPos;
    public int enemyNum;
    private Vector3 cacheVector;

    private float timer, secondsFloatTimer, spawnWaitTime;

    public void Start()
    {
        OnInit();
    }

    private void Update()
    {
        Timer();
        SpawnEnemy();
    }

    public void OnInit()
    {
        spawnWaitTime = 2;
        enemyNum = LevelManager.Ins.enemyNum;
    }

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
        if(secondsFloatTimer > spawnWaitTime && enemyNum > 0)
        {
            characterHolder = SimplePool.Spawn<Character>(character, GetRandomPosition(-10, 10), Quaternion.identity);
            IsOutOfBound(characterHolder.transform);
            ResetTimer();
            enemyNum--;
        }
    }

    public void Timer()
    {
        timer += Time.deltaTime;
        secondsFloatTimer = (float)(timer % 60);
    }

    public void ResetTimer()
    {
        timer = 0.0f;
    }
}
