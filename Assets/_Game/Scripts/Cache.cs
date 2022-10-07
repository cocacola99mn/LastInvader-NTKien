using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache
{
    public static Dictionary<GameObject, Bullet> bullet = new Dictionary<GameObject, Bullet>();
    public static Dictionary<Collider, Character> character = new Dictionary<Collider, Character>();
    public static Dictionary<GameObject, Spread> spread = new Dictionary<GameObject, Spread>();

    public static Bullet GetBullet(GameObject gameObject)
    {
        if (!bullet.ContainsKey(gameObject))
        {
            bullet.Add(gameObject, gameObject.GetComponent<Bullet>());
        }

        return bullet[gameObject];
    }

    public static Character GetCharacter(Collider collider)
    {
        if (!character.ContainsKey(collider))
        {
            character.Add(collider, collider.GetComponent<Character>());
        }

        return character[collider];
    }

    public static Spread GetSpread(GameObject gameObject)
    {
        if (!spread.ContainsKey(gameObject))
        {
            spread.Add(gameObject, gameObject.GetComponent<Spread>());
        }

        return spread[gameObject];
    }
}
