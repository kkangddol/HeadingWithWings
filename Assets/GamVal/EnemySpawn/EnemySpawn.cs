using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private Transform playerPos;
    private Transform enemyParent;
    private bool spawnDegree = false;
    // Coroutine 관리용 배열
    private List<Coroutine> coSpawn = new List<Coroutine>();

    private void Start()
    {
        playerPos = GameObject.Find("PlayerController").GetComponent<Transform>();
        // enemyParent = GameObject.Find("Enemies").GetComponent<Transform>();
        // for Test
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            StageMonsterGenerate stageMonsterinfo = GameManager.Data.StageMonsterGenerateDict[1];
            Vector3 spawnPos = RandomCirclePosition(20);
            for (int i = 0; i < stageMonsterinfo.monsterGroupGenerateInfo.amount.Length; i++)
            {
                GameObject go = Instantiate(Resources.Load<GameObject>($"Enemy/{stageMonsterinfo.monsterGroupGenerateInfo.id[i]}"));
                go.name = go.name.Substring(0, go.name.IndexOf("(Clone)"));
                Vector2 offset = 3 * Random.insideUnitCircle;
                go.transform.position = playerPos.position + spawnPos + new Vector3(offset.x, 0, offset.y);
            }
            yield return new WaitForSeconds(10.0f);
        }
    }

    // 플레이어를 기준으로 위아래로 번갈아가면서 몬스터가 스폰될 수 있도록 설정
    private Vector3 RandomCirclePosition(int range)
    {
        Vector3 spawnPos = Vector3.zero;
        float degree = 0.0f;

        if(spawnDegree)
        {
            degree = Random.Range(0, Mathf.PI);
        }
        else
        {
            degree = Random.Range(Mathf.PI, 2 * Mathf.PI);
        }
        spawnPos.x = range * Mathf.Cos(degree);
        spawnPos.z = range * Mathf.Sin(degree);

        spawnDegree = !spawnDegree;
        return spawnPos;
    }
    
    // 원형 적 생성
    private void CircleFormulaPosition(int stage, int range, int points = 50)
    {
        float radian = 360 / points * Mathf.PI / 180;
        for(float i = 0; i <= 2 * Mathf.PI; i += radian)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>($"Enemy/{0}"));
            go.name = go.name.Substring(0, go.name.IndexOf("(Clone)"));
            go.transform.position = playerPos.position + range * (new Vector3(Mathf.Cos(i), 0, Mathf.Sin(i)));
            go.transform.SetParent(enemyParent);
        }
    }

    // stage에 맞는 정보를 각 적 생성매니저에게 넘김, test는 테스트용 stage = 2의 값을 사용
    public void SpawnCluster(int stage)
    {
        StageMonsterGenerate stageMonsterinfo = GameManager.Data.StageMonsterGenerateDict[stage];
        StageMonsterGenerate testInfo = GameManager.Data.StageMonsterGenerateDict[2];
        coSpawn.Add(StartCoroutine(CoSpawnBasicManager(stageMonsterinfo)));

        if (stageMonsterinfo.specialGenerateInfo.id[0] != -1)
        {
            coSpawn.Add(StartCoroutine(CoSpawnSpecialManager(stageMonsterinfo)));
        }

        // Test
        if(testInfo.bigwaveGenerateInfo.monsterGenerateInfo.id[0] != -1)
        {
            coSpawn.Add(StartCoroutine(CoSpawnBigwaveManager(testInfo)));
            //if (Random.Range(0, 2) == 1)
            //{
            //    CircleFormulaPosition(1, 20);
            //}
        }
    }
    // 보스 몬스터 생성시 모든 스폰이 중지되어야 함으로 모든 코루틴을 중지해준다.
    // 혹시나 지속적으로 생성될 Coroutine이 있을지도 몰라, StopAllCoroutines를 사용하지 않았습니다.
    public void StopSpawnCluster()
    {
        foreach(Coroutine co in coSpawn)
        {
            StopCoroutine(co);
        }
    }

    // 기본 몬스터 정보만 받아서 사용
    private void SpawnBasic(MonsterGroupGenerateInfo monsterInfo)
    {
        // need range
        Vector3 spawnPos = RandomCirclePosition(20);
        for (int i = 0; i < monsterInfo.amount.Length; i++)
        {
            for(int j = 0; j < monsterInfo.amount[i]; j++)
            {
                GameObject go = Instantiate(Resources.Load<GameObject>($"Enemy/{monsterInfo.id[i]}"));
                go.name = go.name.Substring(0, go.name.IndexOf("(Clone)"));
                Vector2 offset = 3 * Random.insideUnitCircle;
                go.transform.position = playerPos.position + spawnPos + new Vector3(offset.x, 0, offset.y);
                go.transform.SetParent(enemyParent);
            }
        }
    }
    // 특수 몬스터 정보만 받아서 사용
    private void SpawnSpecial(StageMonsterGenerate stageMonsterInfo)
    {
        Vector3 spawnPos = RandomCirclePosition(20);
        for (int i = 0; i < stageMonsterInfo.monsterGroupGenerateInfo.amount.Length; i++)
        {
            for (int j = 0; j < stageMonsterInfo.monsterGroupGenerateInfo.amount[i]; j++)
            {
                GameObject go = Instantiate(Resources.Load<GameObject>($"Enemy/{stageMonsterInfo.monsterGroupGenerateInfo.id[i]}"));
                go.name = go.name.Substring(0, go.name.IndexOf("(Clone)"));
                Vector2 offset = 3 * Random.insideUnitCircle;
                go.transform.position = playerPos.position + spawnPos + new Vector3(offset.x, 0, offset.y);
                go.transform.SetParent(enemyParent);
            }
        }
    }
    private void SpawnBigWave(BigwaveGenerateInfo bigwaveInfo)
    {
        int clusterIndex = bigwaveInfo.monsterGenerateInfo.amount.Length - 1;
        int clusterAmount = bigwaveInfo.monsterGenerateInfo.amount[clusterIndex];
        Vector3 spawnPos = RandomCirclePosition(20);

        // 3중첩 for문은 지양해야해서 이 부분은 다른 알고리즘으로 개선이 필요하다고 생각합니다.
        for (int i = 0; i < clusterAmount; i++)
        {
            // for(int j = 0; j < bigwaveInfo.monsterGenerateInfo.id.Length; j++)
            for(int j = 0; j < bigwaveInfo.monsterGenerateInfo.id.Length - 1; j++)
            {
                for(int k = 0; k < bigwaveInfo.monsterGenerateInfo.amount[j]; k++)
                {
                    GameObject go = Instantiate(Resources.Load<GameObject>($"Enemy/{bigwaveInfo.monsterGenerateInfo.id[j]}"));
                    go.name = go.name.Substring(0, go.name.IndexOf("(Clone)"));
                    Vector2 offset = 3 * Random.insideUnitCircle;
                    go.transform.position = playerPos.position + spawnPos + new Vector3(offset.x, 0, offset.y);
                    go.transform.SetParent(enemyParent);
                }
            }
            spawnPos = RandomCirclePosition(20);
        }
    }

    // Coroutine 함수는 앞에 Co를 붙여줘 식별할 수 있게 해주었습니다. (Rookiss 강사님이 추천해주신 방법입니다)
    IEnumerator CoSpawnBasicManager(StageMonsterGenerate stageMonsterInfo)
    {
        for(int i  = 0; i < stageMonsterInfo.monsterGroupGenerateInfo.groupAmount; i++)
        {
            SpawnBasic(stageMonsterInfo.monsterGroupGenerateInfo);
            yield return new WaitForSeconds(stageMonsterInfo.monsterGenerateSec);
        }
    }
    IEnumerator CoSpawnSpecialManager(StageMonsterGenerate stageMonsterInfo)
    {
        for(int i = 0; i < stageMonsterInfo.specialGenerateInfo.amount.Length; i++)
        {
            SpawnSpecial(stageMonsterInfo);
            yield return null;
        }
    }
    IEnumerator CoSpawnBigwaveManager(StageMonsterGenerate stageMonsterInfo)
    {
        for(int i = 0; i < stageMonsterInfo.bigwaveGenerateInfo.total; i++)
        {
            // yield return new WaitForSeconds(stageMonsterInfo.bigwaveGenerateInfo.generateSec);
            yield return new WaitForSeconds(10);
            SpawnBigWave(stageMonsterInfo.bigwaveGenerateInfo);
        }
    }
}
