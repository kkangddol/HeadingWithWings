using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipment : MonoBehaviour
{
    public const string PLAYER = "PLAYER";
    public const string ENEMY = "ENEMY";

    public int level;
    public abstract void SetLevel(int newLevel);

    public Bullet GetBullet(ObjectPoolBase pool)
    {
        return pool.GetObject().GetComponent<Bullet>();
    }
}
