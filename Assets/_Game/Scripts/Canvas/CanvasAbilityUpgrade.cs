using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAbilityUpgrade : UICanvas
{
    LevelManager levelIns;

    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        levelIns = LevelManager.Ins;
    }

    public void AtkUpButton()
    {
        levelIns.player.gun.charDamage += 5;
        levelIns.NextWave();
        Close();        
    }

    public void HpUpButton()
    {
        levelIns.player.heatlh += 30;
        levelIns.player.healthBar.maxHealth += 30;
        levelIns.player.healthBar.curHealth = levelIns.player.heatlh;
        levelIns.player.healthBar.healthFill.fillAmount = levelIns.player.healthBar.curHealth / levelIns.player.healthBar.maxHealth;
        levelIns.NextWave();
        Close();
    }

    public void SpdUpButton()
    {
        levelIns.player.charSpeed += 1;
        levelIns.NextWave();
        Close();
    }
}
