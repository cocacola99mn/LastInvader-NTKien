using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Joystick joyStick;
    public Gun gun;

    public ParticleSystem dashVfx;
    public CharacterController controller;
    private Vector3 direction, moveDirection, dashVfxPos;

    //Variable for Movement
    private float targetAngle, angle, turnVelocity, turnTime, horizontal, vertical;
    private bool canDash;
    //Variable for Dash
    private float dashTime, dashCooldown, dashDuration, dashDurationTime;
    
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
        atkRange = 7;
        charSpeed = 5;
        turnTime = 0.1f;
        dashTime = 1;
        dashDuration = 0.15f;
        canDash = true;
        heatlh = 60;
        healthBar.maxHealth = healthBar.curHealth = heatlh;
        healthBar.healthFill.fillAmount = healthBar.curHealth / healthBar.maxHealth;
        gun.charDamage = 10;
        gun.fireRate = 2;
        gun.coolDown = 1 / gun.fireRate;
    }

    public override void Action()
    {
        base.Action();
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
        if(heatlh <= 0)
        {
            LevelManager.Ins.OnFail();
        }
    }

    public void Dash()
    {
        dashVfxPos = charPos;
        dashVfxPos += direction * 2;

        if (Input.GetKeyDown(KeyCode.Space) && canDash == true)
        {
            dashCooldown = Time.time + dashTime;
            dashDurationTime = Time.time + dashDuration;
            canDash = false;
            if (direction.magnitude > 0)
            {
                charSpeed = 15;
                ParticlePool.Play(dashVfx, dashVfxPos, Quaternion.LookRotation(-direction));
            }
        }

        DashCoolDown();
        DashDuration();
    }

    public void DashCoolDown()
    {
        if(Time.time > dashCooldown)
        {
            canDash = true;
        }
    }

    public void DashDuration()
    {
        if (Time.time > dashDurationTime)
        {
            charSpeed = 5;
        }
    }
}
