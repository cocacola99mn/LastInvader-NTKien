using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit, IHit
{
    public HealthBar healthBar;

    public Animator animator;
    public Transform charTransform;
    public LayerMask targetLayer;
    public Vector3 charPos;
    public Collider charCollider;
    public Collider[] colliders;
    public ParticleSystem explodeVfx;
    public DamageDisplay damageUI;

    public int health;
    public float atkRange, charSpeed, charSpeedDefault, dieAnimTime;
    public bool isBoss = false;
    protected string curAnimName;

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
        Die();
    }

    public virtual void Movement() { }

    public void Sensor()
    {
        charPos = charTransform.position;
        colliders = Physics.OverlapSphere(charPos, atkRange, targetLayer);
    }

    public virtual void OnGetHit(int damage)
    {
        health -= damage;
    }

    public void Die()
    {
        if (health <= 0)
        {
            ChangeAnim(GameConstant.DIE_ANIM);
            DieEffect();
        }
    }

    public virtual void DieEffect()
    {
        atkRange = 0;
        charCollider.enabled = false;
    }

    public bool InRange()
    {
        if (Physics.CheckSphere(charPos, atkRange, targetLayer))
        {
            return true;
        }

        return false;
    }

    //Use for Animation time or any delay time
    public bool TimeCounter(ref float delayTime)
    {
        delayTime -= Time.deltaTime;
        if (delayTime <= 0)
        {
            return true;
        }
        return false;
    }

    #region ANIMATORREGION
    public void ChangeAnim(string animName)
    {
        ResetAnim();
        curAnimName = animName;
        animator.SetTrigger(curAnimName);
    }

    public void ResetAnim()
    {
        if (curAnimName != null)
        {
            animator.ResetTrigger(curAnimName);
        }
    }

    public virtual void AnimCondition() { }
    #endregion
}
