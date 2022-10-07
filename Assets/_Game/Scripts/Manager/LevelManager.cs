using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    public Player player;
    public Spawner spawner;

    public GameObject spawnerObject, playerObject, UIGameplay;
    public Text pointText, bulletText, waveNum;
    
    public int point, wave, enemyLeft, enemyNum;

    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        point = 0;
        wave = 1;
        waveNum.text = wave.ToString();
        enemyNum = 10;
        enemyLeft = enemyNum;
    }

    public void GainScore()
    {
        point += 50;
        pointText.text = point.ToString();
    }

    public void NextWave()
    {
        enemyNum += 5;
        enemyLeft = enemyNum;
        wave++;
        waveNum.text = wave.ToString();
        spawner.enemyNum = enemyNum;
        if(wave > 3)
        {
            OnVictory();
        }
    }

    public void SetLevelStart(bool state)
    {
        spawnerObject.SetActive(state);
        playerObject.SetActive(state);
        UIGameplay.SetActive(state);
    }

    public void OnVictory()
    {
        SetLevelStart(false);
        UIManager.Ins.OpenUI(UIID.UICVictory);
    }

    public void OnFail()
    {
        SetLevelStart(false);
        UIManager.Ins.OpenUI(UIID.UICFail);
    }
}
