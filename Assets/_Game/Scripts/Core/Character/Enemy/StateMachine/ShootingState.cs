using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingState : IState<Boss>
{
    public void OnEnter(Boss boss)
    {

    }

    public void OnExecute(Boss boss)
    {
        boss.gun.OnAttack();
    }

    public void OnExit(Boss boss)
    {

    }
}
