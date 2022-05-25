using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityChange : MonoBehaviour
{
    const string PLAYER = "PLAYER";
    protected PlayerInfo playerInfo;
    public float changeMultiplier;
    private void Awake()
    {
        playerInfo = GameObject.FindWithTag(PLAYER).GetComponent<PlayerInfo>();
    }
    abstract public void ApplyChange();
}
