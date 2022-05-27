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
        // ó���� �ٷ� �� ������ ������ �ϴµ� �̰Ͱ� ������ �׳� Event�� ���� height�� �޴� ������� �ص� �� �� �����ϴ�.
        // �Ǵ� �׳� GameManager�� Height�� �����ؼ� ó���ǵ��� ���־ �� �� �����ϴ�.
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
