using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesosphereScene : StageBase
{
    private void Start()
    {
        stage = 3;
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
