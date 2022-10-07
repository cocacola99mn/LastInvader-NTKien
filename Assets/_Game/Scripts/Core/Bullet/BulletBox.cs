using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    NORMAL = 0,
    BOUNCE = 1,
    PIERCE = 2,
    SPREAD = 3,
    EXPLOSIVE = 4
}
public class BulletBox : MonoBehaviour
{
    public BulletType type;
    public List<Bullet> bullet;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GameConstant.PLAYER_TAG))
        {
            Player player = other.GetComponent<Player>();
            player.gun.bullet = bullet[(int)type];
            LevelManager.Ins.bulletText.text = type.ToString();
        }
    }
}
