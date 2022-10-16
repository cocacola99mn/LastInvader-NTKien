using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingState : IState<Boss>
{
    public void OnEnter(Boss boss)
    {
        boss.ChangeAnim(GameConstant.IDLE_ANIM);
    }

    public void OnExecute(Boss boss)
    {
        boss.gun.OnAttack();
    }

    public void OnExit(Boss boss)
    {

    }
}
