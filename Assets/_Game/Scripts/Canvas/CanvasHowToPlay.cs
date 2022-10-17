using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHowToPlay : UICanvas
{
    public void BackButton()
    {
        GameManager.Ins.cameraScaler.matchWidthOrHeight = 1;
        Close();
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
    }
}
