// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EnemySpawn : MonoBehaviour
// {
//     private Transform playerPos;
//     private Transform enemyParent;
//     private bool spawnDegree = false;
//     // Coroutine ������ �迭
//     private List<Coroutine> coSpawn = new List<Coroutine>();

//     private void Start()
//     {
//         playerPos = GameObject.Find("PlayerController").GetComponent<Transform>();
//         // enemyParent = GameObject.Find("Enemies").GetComponent<Transform>();
//         // for Test
//         StartCoroutine(Spawn());
//     }

//     IEnumerator Spawn()
//     {
//         while(true)
//         {
//             StageMonsterGenerate stageMonsterinfo = GameManager.Data.StageMonsterGenerateDict[1];
//             Vector3 spawnPos = RandomCirclePosition(20);
//             for (int i = 0; i < stageMonsterinfo.monsterGroupGenerateInfo.amount.Length; i++)
//             {
//                 GameObject go = Instantiate(Resources.Load<GameObject>($"Enemy/{stageMonsterinfo.monsterGroupGenerateInfo.id[i]}"));
//                 go.name = go.name.Substring(0, go.name.IndexOf("(Clone)"));
//                 Vector2 offset = 3 * Random.insideUnitCircle;
//                 go.transform.position = playerPos.position + spawnPos + new Vector3(offset.x, 0, offset.y);
//             }
//             yield return new WaitForSeconds(5.0f);
//         }
//     }

//     // �÷��̾ �������� ���Ʒ��� �����ư��鼭 ���Ͱ� ������ �� �ֵ��� ����
//     private Vector3 RandomCirclePosition(int range)
//     {
//         Vector3 spawnPos = Vector3.zero;
//         float degree = 0.0f;

//         if(spawnDegree)
//         {
//             degree = Random.Range(0, Mathf.PI);
//         }
//         else
//         {
//             degree = Random.Range(Mathf.PI, 2 * Mathf.PI);
//         }
//         spawnPos.x = range * Mathf.Cos(degree);
//         spawnPos.z = range * Mathf.Sin(degree);

//         spawnDegree = !spawnDegree;
//         return spawnPos;
//     }
    
//     // ���� �� ����
//     private void CircleFormulaPosition(int stage, int range, int points = 50)
//     {
//         float radian = 360 / points * Mathf.PI / 180;
//         for(float i = 0; i <= 2 * Mathf.PI; i += radian)
//         {
//             GameObject go = Instantiate(Resources.Load<GameObject>($"Enemy/{0}"));
//             go.name = go.name.Substring(0, go.name.IndexOf("(Clone)"));
//             go.transform.position = playerPos.position + range * (new Vector3(Mathf.Cos(i), 0, Mathf.Sin(i)));
//             go.transform.SetParent(enemyParent);
//         }
//     }

//     // stage�� �´� ������ �� �� �����Ŵ������� �ѱ�, test�� �׽�Ʈ�� stage = 2�� ���� ���
//     public void SpawnCluster(int stage)
//     {
//         StageMonsterGenerate stageMonsterinfo = GameManager.Data.StageMonsterGenerateDict[stage];
//         StageMonsterGenerate testInfo = GameManager.Data.StageMonsterGenerateDict[2];
//         coSpawn.Add(StartCoroutine(CoSpawnBasicManager(stageMonsterinfo)));

//         if (stageMonsterinfo.specialGenerateInfo.id[0] != -1)
//         {
//             coSpawn.Add(StartCoroutine(CoSpawnSpecialManager(stageMonsterinfo)));
//         }

//         // Test
//         if(testInfo.bigwaveGenerateInfo.monsterGenerateInfo.id[0] != -1)
//         {
//             coSpawn.Add(StartCoroutine(CoSpawnBigwaveManager(testInfo)));
//             //if (Random.Range(0, 2) == 1)
//             //{
//             //    CircleFormulaPosition(1, 20);
//             //}
//         }
//     }
//     // ���� ���� ������ ��� ������ �����Ǿ�� ������ ��� �ڷ�ƾ�� �������ش�.
//     // Ȥ�ó� ���������� ������ Coroutine�� �������� ����, StopAllCoroutines�� ������� �ʾҽ��ϴ�.
//     public void StopSpawnCluster()
//     {
//         foreach(Coroutine co in coSpawn)
//         {
//             StopCoroutine(co);
//         }
//     }

//     // �⺻ ���� ������ �޾Ƽ� ���
//     private void SpawnBasic(MonsterGroupGenerateInfo monsterInfo)
//     {
//         // need range
//         Vector3 spawnPos = RandomCirclePosition(20);
//         for (int i = 0; i < monsterInfo.amount.Length; i++)
//         {
//             for(int j = 0; j < monsterInfo.amount[i]; j++)
//             {
//                 GameObject go = Instantiate(Resources.Load<GameObject>($"Enemy/{monsterInfo.id[i]}"));
//                 go.name = go.name.Substring(0, go.name.IndexOf("(Clone)"));
//                 Vector2 offset = 3 * Random.insideUnitCircle;
//                 go.transform.position = playerPos.position + spawnPos + new Vector3(offset.x, 0, offset.y);
//                 go.transform.SetParent(enemyParent);
//             }
//         }
//     }
//     // Ư�� ���� ������ �޾Ƽ� ���
//     private void SpawnSpecial(StageMonsterGenerate stageMonsterInfo)
//     {
//         Vector3 spawnPos = RandomCirclePosition(20);
//         for (int i = 0; i < stageMonsterInfo.monsterGroupGenerateInfo.amount.Length; i++)
//         {
//             for (int j = 0; j < stageMonsterInfo.monsterGroupGenerateInfo.amount[i]; j++)
//             {
//                 GameObject go = Instantiate(Resources.Load<GameObject>($"Enemy/{stageMonsterInfo.monsterGroupGenerateInfo.id[i]}"));
//                 go.name = go.name.Substring(0, go.name.IndexOf("(Clone)"));
//                 Vector2 offset = 3 * Random.insideUnitCircle;
//                 go.transform.position = playerPos.position + spawnPos + new Vector3(offset.x, 0, offset.y);
//                 go.transform.SetParent(enemyParent);
//             }
//         }
//     }
//     private void SpawnBigWave(BigwaveGenerateInfo bigwaveInfo)
//     {
//         int clusterIndex = bigwaveInfo.monsterGenerateInfo.amount.Length - 1;
//         int clusterAmount = bigwaveInfo.monsterGenerateInfo.amount[clusterIndex];
//         Vector3 spawnPos = RandomCirclePosition(20);

//         // 3��ø for���� �����ؾ��ؼ� �� �κ��� �ٸ� �˰��������� ������ �ʿ��ϴٰ� �����մϴ�.
//         for (int i = 0; i < clusterAmount; i++)
//         {
//             // for(int j = 0; j < bigwaveInfo.monsterGenerateInfo.id.Length; j++)
//             for(int j = 0; j < bigwaveInfo.monsterGenerateInfo.id.Length - 1; j++)
//             {
//                 for(int k = 0; k < bigwaveInfo.monsterGenerateInfo.amount[j]; k++)
//                 {
//                     GameObject go = Instantiate(Resources.Load<GameObject>($"Enemy/{bigwaveInfo.monsterGenerateInfo.id[j]}"));
//                     go.name = go.name.Substring(0, go.name.IndexOf("(Clone)"));
//                     Vector2 offset = 3 * Random.insideUnitCircle;
//                     go.transform.position = playerPos.position + spawnPos + new Vector3(offset.x, 0, offset.y);
//                     go.transform.SetParent(enemyParent);
//                 }
//             }
//             spawnPos = RandomCirclePosition(20);
//         }
//     }

//     // Coroutine �Լ��� �տ� Co�� �ٿ��� �ĺ��� �� �ְ� ���־����ϴ�. (Rookiss ������� ��õ���ֽ� ����Դϴ�)
//     IEnumerator CoSpawnBasicManager(StageMonsterGenerate stageMonsterInfo)
//     {
//         for(int i  = 0; i < stageMonsterInfo.monsterGroupGenerateInfo.groupAmount; i++)
//         {
//             SpawnBasic(stageMonsterInfo.monsterGroupGenerateInfo);
//             yield return new WaitForSeconds(stageMonsterInfo.monsterGenerateSec);
//         }
//     }
//     IEnumerator CoSpawnSpecialManager(StageMonsterGenerate stageMonsterInfo)
//     {
//         for(int i = 0; i < stageMonsterInfo.specialGenerateInfo.amount.Length; i++)
//         {
//             SpawnSpecial(stageMonsterInfo);
//             yield return null;
//         }
//     }
//     IEnumerator CoSpawnBigwaveManager(StageMonsterGenerate stageMonsterInfo)
//     {
//         for(int i = 0; i < stageMonsterInfo.bigwaveGenerateInfo.total; i++)
//         {
//             // yield return new WaitForSeconds(stageMonsterInfo.bigwaveGenerateInfo.generateSec);
//             yield return new WaitForSeconds(10);
//             SpawnBigWave(stageMonsterInfo.bigwaveGenerateInfo);
//         }
//     }
// }
