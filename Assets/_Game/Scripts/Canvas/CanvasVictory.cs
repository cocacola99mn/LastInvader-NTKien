using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasVictory : UICanvas
{
    public void MenuButton()
    {
        GameManager.Ins.Restart();
    }
}
