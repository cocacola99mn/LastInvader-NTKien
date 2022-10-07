using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField]
    private NavMeshAgent navMeshAgent;
    public Animation anim;

    float delay, delayCountDown;
    bool exploded, touchPlayer;
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
        delay = 1;
        delayCountDown = delay;
        exploded = false;
        touchPlayer = false;
        explodeDamage = 20;
        heatlh = 30;
        healthBar.maxHealth = healthBar.curHealth = heatlh;
        healthBar.healthFill.fillAmount = healthBar.curHealth / healthBar.maxHealth;
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
        }

        if (touchPlayer)
        {
            anim.Play();
            atkRange = 2;
            delayCountDown -= Time.deltaTime;
            if(delayCountDown <= 0 && !exploded)
            {
                Explode();
                exploded = true;
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

        //Add Any Explode Effect you want
        OnDespawn();
    }

    public override void OnGetHit(int damage)
    {
        base.OnGetHit(damage);
        if (heatlh <= 0)
        {
            LevelManager.Ins.GainScore();
            OnDespawn();
        }
    }

    public void OnDespawn()
    {
        OnInit();
        SimplePool.Despawn(this);
        LevelManager.Ins.enemyLeft--;
        if(LevelManager.Ins.enemyLeft == 0)
        {
            LevelManager.Ins.NextWave();
        }
    }
}
