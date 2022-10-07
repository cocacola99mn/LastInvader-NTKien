using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Joystick joyStick;
    public Gun gun;

    public CharacterController controller;
    private Vector3 direction, moveDirection;

    private float targetAngle, angle, turnVelocity, turnTime, horizontal, vertical, dashDistance, dashTime, dashCooldown;
    private bool canDash;
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
        charSpeed = 5;
        turnTime = 0.1f;
        dashTime = 1;
        dashDistance = 10;
        dashCooldown = 2;
        canDash = true;
        heatlh = 60;
        healthBar.maxHealth = healthBar.curHealth = heatlh;
        healthBar.healthFill.fillAmount = healthBar.curHealth / healthBar.maxHealth;
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
        if (Input.GetKeyDown(KeyCode.Space) && canDash == true)
        {
            dashCooldown = Time.time + dashTime;
            canDash = false;
            if (direction.magnitude > 0)
            {
                controller.Move(charSpeed * Time.deltaTime * MoveDirection() * dashDistance);
            }
        }

        DashCoolDown();
    }

    public void DashCoolDown()
    {
        if(Time.time > dashCooldown)
        {
            canDash = true;
        }
    }
}
