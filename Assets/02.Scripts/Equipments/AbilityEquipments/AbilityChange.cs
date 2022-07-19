using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityChange : MonoBehaviour
{
    public static PlayerInfo playerInfo;
    public float changeMultiplier;
    private void Start()
    {
        playerInfo = GameObject.FindWithTag("PLAYER").GetComponent<PlayerInfo>();
    }
    abstract public void ApplyChange();
}
