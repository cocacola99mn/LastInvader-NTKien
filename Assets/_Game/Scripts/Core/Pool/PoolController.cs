using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    #region OBJECT
    public Transform enemyHolder, bulletHolder, healthBoxHolder, damageDisplayHolder;

    public Character enemy, boss;
    public Bullet normal, bounce, explosive, pierce, spread, bossBullet;
    public HealthBox healthBox;
    public DamageDisplay damageDisplay;
    #endregion

    #region PARTICLE
    public Transform enemyExplodeHolder, bulletExplodeHolder, dashHolder;

    public ParticleSystem enemyExplode, bulletExplode, dashVfx;
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
        SimplePool.Preload(healthBox, 3, healthBoxHolder);
        SimplePool.Preload(bossBullet, 10, bulletHolder);
        SimplePool.Preload(boss, 3, enemyHolder);
        SimplePool.Preload(damageDisplay, 20, damageDisplayHolder);

        ParticlePool.Preload(enemyExplode, 10, enemyExplodeHolder);
        ParticlePool.Preload(bulletExplode, 10, bulletExplodeHolder);
        ParticlePool.Preload(dashVfx, 3, dashHolder);
    }
}
