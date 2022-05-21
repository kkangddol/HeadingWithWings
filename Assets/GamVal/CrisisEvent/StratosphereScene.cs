using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StratosphereScene : StageBase
{
    private void Start()
    {
        stage = 2;
        GameManager.Instance.stageEvents = OnCrisisEvent;
    }

    protected override void BossEvent()
    {
        base.BossEvent();

    }

    protected override void BigwaveEvent()
    {
        base.BigwaveEvent();

    }
}
