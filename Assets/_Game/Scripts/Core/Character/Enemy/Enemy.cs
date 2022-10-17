using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField]
    public NavMeshAgent navMeshAgent;

    float delay, defaultSpeed;
    bool touchPlayer;
    int explodeDamage;

    void Start()
    {
        OnInit();
    }

    void Update()
    {
        Action();
    }

    public override void OnInit()
    {
        atkRange = 1;
        delay = 2;
        dieAnimTime = 1;
        defaultSpeed = 5;
        navMeshAgent.speed = defaultSpeed;
        touchPlayer = false;
        explodeDamage = 20;
        health = 30;
        charCollider.enabled = true;
    }

    public override void Action()
    {
        base.Action();
        OnContact();
    }

    public override void Movement()
    {
        navMeshAgent.destination = LevelManager.Ins.player.charPos;
    }

    public void OnContact()
    {
        if (colliders.Length > 0)
        {
            touchPlayer = true;
            ChangeAnim(GameConstant.ATTACK_ANIM);
        }

        if (touchPlayer)
        {
            atkRange = 3;
            navMeshAgent.speed = 0;
            if(TimeCounter(ref delay))
            {
                Explode();
            }
        }
    }

    public void Explode()
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != null)
            {
                Cache.GetCharacter(colliders[i]).OnGetHit(explodeDamage);
            }
        }

        ParticlePool.Play(explodeVfx, charPos, Quaternion.Euler(0, 0, 0));
        OnDespawn();
    }

    public override void OnGetHit(int damage)
    {
        base.OnGetHit(damage);
        Vector3 uiPos = charPos;
        SimplePool.Spawn<DamageDisplay>(damageUI, uiPos, Quaternion.Euler(60,0,0));
        if(health <= 0)
        {
            LevelManager.Ins.GainScore();
        }
    }

    public override void DieEffect()
    {
        base.DieEffect();
        navMeshAgent.speed = 0;
        if (TimeCounter(ref dieAnimTime))
        {
            OnDespawn();
        }
    }

    public void OnDespawn()
    {
        OnInit();
        LevelManager.Ins.OnEnemyDie();
        SimplePool.Despawn(this);
    }
}
