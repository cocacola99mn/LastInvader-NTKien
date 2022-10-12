using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageDisplay : GameUnit
{
    public Transform damageDisplay;
    public Text damageText;
    public Color  textColor;
    float timer, floatTimer, waitTime;

    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        waitTime = 1.2f;
    }

    void Update()
    {
        Timer();
        TextAppear();
    }

    public void TextAppear()
    {
        damageDisplay.position += new Vector3(0, 0.05f, -0.05f);
        textColor = damageText.color;
        textColor.a += 0.04f;
        damageText.color = textColor;
        damageText.text = "-" + LevelManager.Ins.player.gun.charDamage.ToString();
        DespawnTime();
    }

    public void Timer()
    {
        timer += Time.deltaTime;
        floatTimer = (float)(timer % 60);
    }

    public void ResetTimer()
    {
        timer = 0.0f;
    }
    
    public void DespawnTime()
    {
        if(floatTimer >= waitTime)
        {
            OnDespawn();
        }
    }

    private void OnDespawn()
    {
        SimplePool.Despawn(this);
        ResetTimer();
    }
}
