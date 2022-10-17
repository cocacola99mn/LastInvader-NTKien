using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Character
{
    public Gun gun;
    [SerializeField] public NavMeshAgent navMeshAgent;
    public SkinnedMeshRenderer mesh;
    public Material phase2Material;
    public GameObject healthBarTop;

    public IState<Boss> currentState;
    public ShootingState shootingState { get; protected set; }
    public ChasingState chasingState { get; protected set; }
    public DieState dieState { get; protected set; }

    float delay, delayCountDown;
    bool exploded, isTouched;
    int explodeDamage;

    void Start()
    {
        OnInit();
    }

    void FixedUpdate()
    {
        Action();
    }

    public override void OnInit()
    {
        shootingState = new ShootingState();
        chasingState = new ChasingState();
        ChangeState(shootingState);
        atkRange = 20;
        delay = 2;
        delayCountDown = delay;
        exploded = false;
        isTouched = false;
        explodeDamage = 200;
        health = 1500;
        healthBar.maxHealth = healthBar.curHealth = health;
        healthBar.curHealthText.text = health.ToString();
        healthBar.maxHealthText.text = health.ToString();
        healthBar.healthFill.fillAmount = healthBar.curHealth / healthBar.maxHealth;
        gun.charDamage = 20;
        gun.fireRate = 0.25f;
        gun.coolDown = 1 / gun.fireRate;
        dieAnimTime = 1;
        isBoss = true;
    }

    public override void Action()
    {
        base.Action();
        ExecuteState();
    }

    public void OnContact()
    {
        if(colliders.Length > 0)
        {
            isTouched = true;
        }

        if (isTouched)
        {
            mesh.material = phase2Material;
            atkRange = 20;
            delayCountDown -= Time.deltaTime;
            if (delayCountDown <= 0 && !exploded)
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

        ParticlePool.Play(explodeVfx, charPos, Quaternion.Euler(0, 0, 0));
        SimplePool.Despawn(this);
    }

    public override void OnGetHit(int damage)
    {
        base.OnGetHit(damage);
        health = (health < 0) ? 0 : health;
        healthBar.curHealth = health;
        healthBar.curHealthText.text = health.ToString();
        healthBar.healthFill.fillAmount = healthBar.curHealth / healthBar.maxHealth;
        SimplePool.Spawn<DamageDisplay>(damageUI, charPos, Quaternion.identity);
        if(health <= 1500/2)
        {
            ChangeState(chasingState);
        }
        if (health <= 0)
        {
            LevelManager.Ins.GainScore();
        }
    }

    public override void DieEffect()
    {
        base.DieEffect();
        navMeshAgent.speed = 0;
        exploded = true;
        ChangeState(dieState);
        if (TimeCounter(ref dieAnimTime))
        {
            SimplePool.Despawn(this);
            LevelManager.Ins.OnVictory();
        }
    }

    #region STATEMACHINE

    public void ExecuteState()
    {
        if (currentState != null)
            currentState.OnExecute(this);
    }

    public void ChangeState(IState<Boss> state)
    {
        if (currentState != null)
            currentState.OnExit(this);

        currentState = state;

        if (currentState != null)
            currentState.OnEnter(this);
    }

    public bool IsState(IState<Boss> state)
    {
        if (state == currentState)
            return true;
        else
            return false;
    }

    #endregion
}
