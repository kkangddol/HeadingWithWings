using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject crow;
    public GameObject eagle;
    public GameObject s_harpy;
    public GameObject b_harpy;
    public GameObject snitch;
    public GameObject cupid;
    public GameObject B_Whale;
    public GameObject B_Pegasus;

    public GameObject[] min0Enemies;
    public GameObject[] min1Enemies;
    public GameObject[] min2Enemies;
    public GameObject[] min3Enemies;
    public GameObject[] min4Enemies;
    public GameObject[] min5Enemies;
    public GameObject[] min6Enemies;
    public GameObject[] min7Enemies;
    public GameObject[] min8Enemies;
    public GameObject[] min9Enemies;
    public GameObject[] min10Enemies;
    public GameObject[] min11Enemies;
    public GameObject[] min12Enemies;
    public GameObject[] min13Enemies;
    public GameObject[] min14Enemies;
    public GameObject[] min15Enemies;
    public GameObject[] min16Enemies;
    public GameObject[] min17Enemies;
    public GameObject[] min18Enemies;
    public GameObject[] min19Enemies;

    List<GameObject[]> list = new List<GameObject[]>();

    public Transform enemyParentObject;
    bool isStopSpwaning = false;
    float spawnCoolTime = 2.5f;
    public GameObject WhaleBoss;
    bool is5minBoss = false;





    // Start is called before the first frame update
    void Start()
    {
        list.Add(min0Enemies);
        list.Add(min1Enemies);
        list.Add(min2Enemies);
        list.Add(min3Enemies);
        list.Add(min4Enemies);
        list.Add(min5Enemies);
        list.Add(min6Enemies);
        list.Add(min7Enemies);
        list.Add(min8Enemies);
        list.Add(min9Enemies);
        list.Add(min10Enemies);
        list.Add(min11Enemies);
        list.Add(min12Enemies);
        list.Add(min13Enemies);
        list.Add(min14Enemies);
        list.Add(min15Enemies);
        list.Add(min16Enemies);
        list.Add(min17Enemies);
        list.Add(min18Enemies);
        list.Add(min19Enemies);

        enemyParentObject = GameObject.Find("Enemies").GetComponent<Transform>();
    }

    private void Update() {
        if(!isStopSpwaning)
        {
            SpawnWave(GameManager.Instance.playingTimeMinute);
        }

        if(GameManager.Instance.playingTimeMinute == 5 && !is5minBoss)
        {
            Spawn5minBoss();
        }
    }


    void SpawnWave(int wave)
    {
        isStopSpwaning = true;
        int randomNumber = Random.Range(0, list[wave].Length);
        var spawnedEnemy = Instantiate(list[wave][randomNumber], GetRandomPosition(), Quaternion.identity, enemyParentObject);
        Invoke("CoolingSpawner", spawnCoolTime);
    }

    void CoolingSpawner()
    {
        isStopSpwaning = false;
    }

    void Spawn5minBoss()
    {
        is5minBoss = true;
        var spawnedEnemy = Instantiate(WhaleBoss, GetRandomPosition(), Quaternion.identity, enemyParentObject);
    }

    private Vector2 GetRandomPosition()
    {
        float radius = 10f;
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

}
