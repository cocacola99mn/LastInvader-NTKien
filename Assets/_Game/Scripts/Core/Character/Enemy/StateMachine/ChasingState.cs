using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingState : IState<Boss>
{
    public void OnEnter(Boss boss)
    {
        boss.atkRange = 4;
    }

    public void OnExecute(Boss boss)
    {
        boss.navMeshAgent.destination = LevelManager.Ins.player.charPos;
        boss.OnContact();
    }

    public void OnExit(Boss boss)
    {

    }
}
