using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//const string ENEMY = "ENEMY";

public abstract class Wings : MonoBehaviour
{
    protected PlayerInfo playerInfo;
    protected const string ENEMY = "ENEMY";

    void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
    }
}   
