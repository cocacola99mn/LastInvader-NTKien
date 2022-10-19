using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Joystick joyStick;
    public Gun gun;

    public ParticleSystem dashVfx;
    public CharacterController controller;

    //Variable for Movement;
    private Vector3 direction, moveDirection, dashVfxPos;
    //Variable for Movement
    private float targetAngle, angle, turnVelocity, turnTime, horizontal, vertical;
    private bool canDash;
    //Variable for Dash
    private float dashTime, dashDuration, dashTimeCounter, dashDurationCounter;
   
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
        atkRange = 6;
        charSpeedDefault = 5;
        charSpeed = charSpeedDefault;
        turnTime = 0.1f;
        dashTime = 1;
        dashDuration = 0.15f;
        dashTimeCounter = dashTime;
        dashDurationCounter = dashDuration;
        canDash = true;
        health = 60;
        healthBar.maxHealth = healthBar.curHealth = health;
        healthBar.healthFill.fillAmount = healthBar.curHealth / healthBar.maxHealth;
        gun.charDamage = 10;
        gun.fireRate = 4;
        gun.coolDown = 1 / gun.fireRate;
        dieAnimTime = 1;
    }

    public override void Action()
    {
        if (LevelManager.Ins.levelStart)
        {
            base.Action();
            AnimCondition();
            Dash();
            gun.OnAttack();

            if (InRange())
            {
                PlayerRotation(GetClosestEnemyCollider(colliders) - charPos);
            }
            else
            {
                PlayerRotation(MoveDirection());
            }
        }
    }

    public override void Movement()
    {
        controller.Move(charSpeed * Time.deltaTime * MoveDirection());
        
    }

    private void PlayerRotation(Vector3 direction)
    {
        if(MoveDirection().magnitude > 0 || InRange())
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);
            charTransform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }

    private Vector3 MoveDirection()
    {
        horizontal = joyStick.Horizontal;
        vertical = joyStick.Vertical;

        moveDirection.x = horizontal;
        moveDirection.y = 0;
        moveDirection.z = vertical;

        direction = moveDirection.normalized;
        return direction;
    }

    public Vector3 GetClosestEnemyCollider(Collider[] enemyColliders)
    {
        float bestDistance = 10000;
        Collider bestCollider = null;

        for (int i = 0; i < enemyColliders.Length; i++)
        {
            if (enemyColliders[i] != null)
            {
                float distance = Vector3.Distance(charPos, enemyColliders[i].transform.position);

                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    bestCollider = enemyColliders[i];
                }
            }
        }

        return bestCollider.transform.position;
    }

    public override void OnGetHit(int damage)
    {
        base.OnGetHit(damage);
        healthBar.curHealth = health;
        healthBar.healthFill.fillAmount = healthBar.curHealth / healthBar.maxHealth;
    }

    public void Dash()
    {
        dashVfxPos = charPos;

        if (Input.GetKeyDown(KeyCode.Space) && canDash == true)
        {
            dashTimeCounter = dashTime;
            dashDurationCounter = dashDuration;
            canDash = false;
            if (direction.magnitude > 0)
            {
                charSpeed = 25;
                controller.center = new Vector3(0, 10, 0);
                ParticlePool.Play(dashVfx, dashVfxPos, Quaternion.LookRotation(-direction));
            }
        }

        if(TimeCounter(ref dashTimeCounter))
        {
            canDash = true;
        }
        if(TimeCounter(ref dashDurationCounter))
        {
            controller.center = Vector3.zero;
            charSpeed = charSpeedDefault;
        }
    }

    public override void AnimCondition()
    {
        if(health > 0)
        {
            if (MoveDirection().magnitude > 0.01f)
            {
                if (InRange())
                {
                    if (Vector3.Dot(GetClosestEnemyCollider(colliders) - charPos, MoveDirection()) < 0) //Check if player moving backward or forward from enemy
                    {
                        ChangeAnim(GameConstant.STEPBACKSHOOT_ANIM);
                    }
                    else
                    {
                        ChangeAnim(GameConstant.RUNSHOOT_ANIM);
                    }
                }
                else
                {
                    ChangeAnim(GameConstant.RUNSHOOT_ANIM);
                }
            }
            else
            {
                ShootAnim();
            }
        }        
    }

    public void ShootAnim()
    {
        if (curAnimName != null)
        {
            animator.ResetTrigger(curAnimName);
        }
    }

    public override void DieEffect()
    {
        base.DieEffect();
        if (TimeCounter(ref dieAnimTime))
        {
            LevelManager.Ins.levelStart = false;
            LevelManager.Ins.OnFail();
        }
    }
}
