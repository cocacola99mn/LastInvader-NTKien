using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    public Player player;
    public Spawner spawner;
    public DamageDisplay damageDisplay;
    Boss boss;

    public GameObject playerObject, UIGameplay, waveDisplayTextObject, bossHealthBar;
    public Text pointText, bulletText, waveNum, waveDisplayText;
    WaitForSeconds displayTime;
    
    public int point, wave, enemyLeft, enemyNum;
    private float timer, secondsFloatTimer, spawnWaitTime;

    public bool levelStart;
    bool playerDie;

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        Timer();
        SpawnEnemyWithCondition();
    }

    public void OnInit()
    {
        point = 0;
        wave = 1;
        waveNum.text = wave.ToString();
        enemyNum = 40;
        enemyLeft = enemyNum;
        spawnWaitTime = 0.5f;
        displayTime = new WaitForSeconds(2);
        playerDie = false;
        spawner.SpawnHealthBox();
    }

    public void GainScore()
    {
        point += 50;
        pointText.text = point.ToString();
    }

    public void NextWave()
    {
        wave++;
        if(wave == 3)
        {
            boss = SimplePool.Spawn<Boss>(spawner.boss, Vector3.zero, Quaternion.identity);
        }
        else
        {
            enemyNum += 20;
            enemyLeft = enemyNum;
            spawner.enemyNum = enemyNum;
        }
        waveNum.text = wave.ToString();
        waveDisplayText.text = "WAVE " + wave.ToString();
        StartCoroutine(WaveDisplay());
    }

    public void SetLevelStart(bool state)
    {
        UIGameplay.SetActive(state);
        levelStart = state;
    }

    public void SpawnEnemyWithCondition()
    {
        if (secondsFloatTimer > spawnWaitTime && enemyNum > 0 && levelStart == true)
        {
            spawner.SpawnEnemy();
            ResetTimer();
            enemyNum--;
        }
    }

    public void OnEnemyDie()
    {
        enemyLeft--;
        if (enemyLeft == 0 && playerDie == false)
        {
            UIManager.Ins.OpenUI(UIID.UICAbilityUpgrade);
        }
    }

    public void OnVictory()
    {
        UIGameplay.SetActive(false);
        levelStart = false;
        player.ChangeAnim(GameConstant.WIN_ANIM);
        UIManager.Ins.OpenUI(UIID.UICVictory);
    }

    public void OnFail()
    {
        SetLevelStart(false);
        playerDie = true;
        if(boss != null)
        {
            boss.healthBarTop.SetActive(false);
        }
        UIManager.Ins.OpenUI(UIID.UICFail);
    }

    public void Timer()
    {
        timer += Time.deltaTime;
        secondsFloatTimer = (float)(timer % 60);
    }

    public void ResetTimer()
    {
        timer = 0.0f;
    }

    IEnumerator WaveDisplay()
    {
        waveDisplayTextObject.SetActive(true);
        yield return displayTime;
        waveDisplayTextObject.SetActive(false);
    }
}
