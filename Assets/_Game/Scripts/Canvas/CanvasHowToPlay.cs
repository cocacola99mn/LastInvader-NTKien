using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHowToPlay : UICanvas
{
    public void BackButton()
    {
        Close();
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
    }
}
