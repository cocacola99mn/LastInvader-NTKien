using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    Scene scene;
    public CanvasScaler cameraScaler;
    protected void Awake()
    {
        OnInit();
    }

    public void OnInit()
    {
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;

        if (Screen.currentResolution.height > maxScreenHeight)
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);

        scene = SceneManager.GetActiveScene();
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
       
    }

    public void Restart()
    {
        SceneManager.LoadScene(scene.name);
        SimplePool.ReleaseAll();
        ParticlePool.ReleaseAll();
    }
}
