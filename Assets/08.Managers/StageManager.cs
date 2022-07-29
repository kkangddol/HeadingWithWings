using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum referenceID
{
    crow = 100,
    eagle = 200,
    s_Harpy = 300,
    b_Harpy = 400,
    snitch = 500,
    cupid = 600,
    B_Whale = 10000,
    B_Pegasus = 10100,
}

enum swarmID
{
    crow_Swarm = 120,
    eagle_Swarm = 220,
    snitch_Swarm = 520,
}

public class StageManager : MonoBehaviour
{
    public GameObject crow;
    public GameObject crow_Swarm;
    public GameObject eagle;
    public GameObject eagle_Swarm;
    public GameObject s_Harpy;
    public GameObject b_Harpy;
    public GameObject snitch;
    public GameObject cupid;
    public GameObject B_Whale;
    public GameObject B_Pegasus;

    public Transform enemyParentObject;
    bool isStopSpwaning = false;

    int _wave;


    void Start()
    {
        enemyParentObject = GameObject.Find("Enemies").GetComponent<Transform>();
        GameManager.Instance.stageManager = this;
        GameManager.Instance.PlayingTimeMinute = 0;
    }

    public void SetWave(int wave)
    {
        _wave = wave;

        switch(_wave)
        {
            case 0:
            {
                StartCoroutine(SpawnEnemy((int)referenceID.crow, crow, 0.5f));
                break;
            }
            case 1:
            {
                StartCoroutine(SpawnEnemy((int)referenceID.eagle, eagle, 1));
                break;
            }
            case 3:
            {
                StartCoroutine(SpawnEnemy((int)referenceID.s_Harpy, s_Harpy, 1));
                SpawnSwarm(20, (int)swarmID.crow_Swarm, crow_Swarm);
                break;
            }
            case 5:
            {
                SpawnBoss((int)referenceID.B_Whale, _wave, B_Whale);
                break;
            }
            case 6:
            {
                StartCoroutine(SpawnEnemy((int)referenceID.b_Harpy, b_Harpy, 1.5f));
                break;
            }
            case 8:
            {
                SpawnSwarm(20, (int)swarmID.eagle_Swarm, eagle_Swarm);
                break;
            }
            case 10:
            {
                SpawnBoss((int)referenceID.B_Pegasus, _wave, B_Pegasus);
                break;
            }
            case 11:
            {
                SpawnSwarm(20, (int)swarmID.snitch_Swarm, snitch);
                StartCoroutine(SpawnEnemy((int)referenceID.snitch, snitch, 1));
                StartCoroutine(SpawnEnemy((int)referenceID.cupid, cupid, 1.5f));
                break;
            }
            case 15:
            {
                SpawnBoss((int)referenceID.B_Whale, _wave, B_Whale);
                SpawnBoss((int)referenceID.B_Pegasus, _wave, B_Pegasus);
                break;
            }
            default:
            break;            
        }
    }

    IEnumerator SpawnEnemy(int referenceID, GameObject prefab, float coolTime)
    {
        WaitForSeconds waitCoolTime = new WaitForSeconds(coolTime);
        while(!isStopSpwaning)
        {
            var spawnedEnemy = Instantiate(prefab, GetRandomPosition(), Quaternion.identity, enemyParentObject);
            spawnedEnemy.GetComponent<EnemyInfo>().SetID(referenceID + _wave);
            spawnedEnemy.GetComponent<EnemyInfo>().DataInit();
            yield return waitCoolTime;
        }
    }
    
    void SpawnSwarm(int swarmSize, int swarmID, GameObject swarmPrefab)
    {
        Vector2 swarmPosition = GetRandomPosition();
        for(int i = 0; i < swarmSize; i++)
        {
            var spawnedEnemy = Instantiate(swarmPrefab, swarmPosition + new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f)), Quaternion.identity, enemyParentObject);
            spawnedEnemy.GetComponent<EnemyInfo>().SetID(swarmID);
            spawnedEnemy.GetComponent<EnemyInfo>().DataInit();
        }
    }

    void SpawnBoss(int referenceID, int wave, GameObject bossPrefab)
    {
        var spawnedEnemy = Instantiate(bossPrefab, GetRandomPosition(), Quaternion.identity, enemyParentObject);
        spawnedEnemy.GetComponent<EnemyInfo>().SetID(referenceID + wave);
        spawnedEnemy.GetComponent<EnemyInfo>().DataInit();
        spawnedEnemy.GetComponent<Boss_Skill_Manager>().DataInit();
    }

    private Vector2 GetRandomPosition()
    {
        float radius = 20f;
        Vector2 playerPosition = GameManager.playerRigidbody.position;
 
        float a = playerPosition.x;
        float b = playerPosition.y;
 
        float x = Random.Range(-radius + a, radius + a);
        float y_b = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x - a, 2));
        y_b *= Random.Range(0, 2) == 0 ? -1 : 1;
        float y = y_b + b;
 
        Vector2 randomPosition = new Vector2(x, y);
 
        return randomPosition;
    }

    public void ResumeSpawning()
    {
        isStopSpwaning = false;
    }
    public void StopSpawning()
    {
        isStopSpwaning = true;
    }

}
