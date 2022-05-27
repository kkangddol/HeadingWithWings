using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroposphereScene: StageBase
{
    EnemySpawn spawner;
    private void Start()
    {
        stage = 1;
        GameManager.Instance.stageEvents = OnCrisisEvent;
        spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawn>();
        // 처음에 바로 초 단위로 시작을 하는데 이것과 별개로 그냥 Event로 따로 height를 받는 방식으로 해도 될 것 같습니다.
        // 또는 그냥 GameManager의 Height와 연동해서 처리되도록 해주어도 될 것 같습니다.
        spawner.SpawnCluster(stage);
    }

    protected override void BossEvent()
    {
        base.BossEvent();
        spawner.StopSpawnCluster();
        Debug.Log("BOSS!");
    }

    protected override void BigwaveEvent()
    {
        base.BigwaveEvent();
        Debug.Log("BIGWAVE!");
    }
}
