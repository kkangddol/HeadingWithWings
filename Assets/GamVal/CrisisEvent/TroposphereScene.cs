using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroposphereScene: StageBase
{
    private void Start()
    {
        stage = 1;
        GameManager.Instance.stageEvents = OnCrisisEvent;
    }

    protected override void BossEvent()
    {
        base.BossEvent();
        Debug.Log("BOSS!");
    }

    protected override void BigwaveEvent()
    {
        base.BigwaveEvent();
        Debug.Log("BIGWAVE!");
    }
}
