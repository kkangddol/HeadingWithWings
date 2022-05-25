using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirenWing : MonoBehaviour
{
    PlayerInfo playerInfo;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        playerInfo = GetComponent<PlayerInfo>();
    }
}
