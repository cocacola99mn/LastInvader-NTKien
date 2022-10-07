using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit, IHit
{
    public HealthBar healthBar;

    public Transform charTransform;
    public LayerMask targetLayer;
    public Vector3 charPos;
    public Collider[] colliders;

    public int heatlh;
    public float atkRange, charSpeed;

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(charPos, atkRange);
    }

#endif

    public virtual void OnInit() { }

    public virtual void Action()
    {
        Movement();
        Sensor();
    }

    public virtual void Movement() { }

    public void Sensor()
    {
        charPos = charTransform.position;
        colliders = Physics.OverlapSphere(charPos, atkRange, targetLayer);
    }

    public virtual void OnGetHit(int damage)
    {
        heatlh -= damage;
        healthBar.curHealth = heatlh;
        healthBar.healthFill.fillAmount = healthBar.curHealth / healthBar.maxHealth;
    }

    public bool InRange()
    {
        if (Physics.CheckSphere(charPos, atkRange, targetLayer))
        {
            return true;
        }

        return false;
    }
}
