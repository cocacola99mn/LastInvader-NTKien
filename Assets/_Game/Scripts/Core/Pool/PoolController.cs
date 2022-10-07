using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    #region OBJECT
    public Transform enemyHolder, bulletHolder, healthBoxHolder;

    public Character enemy;
    public Bullet normal, bounce, explosive, pierce, spread;
    public HealthBox healthBox;
    #endregion

    private void Awake()
    {
        
        OnInit();
    }

    public void OnInit()
    {
        SimplePool.Preload(enemy, 20, enemyHolder);
        SimplePool.Preload(normal, 10, bulletHolder);
        SimplePool.Preload(bounce, 10, bulletHolder);
        SimplePool.Preload(explosive, 10, bulletHolder);
        SimplePool.Preload(pierce, 10, bulletHolder);
        SimplePool.Preload(spread, 30, bulletHolder);
        SimplePool.Preload(healthBox, 10, healthBoxHolder);
    }
}
