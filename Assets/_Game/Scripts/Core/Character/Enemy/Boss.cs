using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Character
{
    public Gun gun;
    [SerializeField] public NavMeshAgent navMeshAgent;
    public MeshRenderer mesh;
    public Material phase2Material;

    public IState<Boss> currentState;
    public ShootingState shootingState { get; protected set; }
    public ChasingState chasingState { get; protected set; }

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
        heatlh = 700;
        healthBar.maxHealth = healthBar.curHealth = heatlh;
        healthBar.healthFill.fillAmount = healthBar.curHealth / healthBar.maxHealth;
        gun.charDamage = 20;
        gun.fireRate = 0.25f;
        gun.coolDown = 1 / gun.fireRate;
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
        SimplePool.Spawn<DamageDisplay>(damageUI, charPos, Quaternion.identity);
        if(heatlh <= 350)
        {
            ChangeState(chasingState);
        }

        if (heatlh <= 0)
        {
            LevelManager.Ins.GainScore();
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
