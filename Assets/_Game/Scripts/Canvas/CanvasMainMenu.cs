using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{

    public void StartButton()
    {
        LevelManager.Ins.SetLevelStart(true);
        Close();
    }

    public void HowToPlayButton()
    {
        GameManager.Ins.cameraScaler.matchWidthOrHeight = 0.4f;
        UIManager.Ins.OpenUI(UIID.UICHowToPlay);
        Close();
    }
}
