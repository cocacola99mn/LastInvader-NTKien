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
        UIManager.Ins.OpenUI(UIID.UICHowToPlay);
        Close();
    }
}
