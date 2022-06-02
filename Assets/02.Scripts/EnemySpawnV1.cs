using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class EnemySpawnV1 : MonoBehaviour
{
    private Transform playerTransform;
    public float spawnDelay = 1.0f;

    private void Start()
    {
        StartCoroutine("SpawnPrototype");
        playerTransform = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();
    }

    private Vector3 GetRandomPosition()
    {
        float radius = 10f;
        Vector3 playerPosition = playerTransform.position;
 
        float a = playerPosition.x;
        float b = playerPosition.z;
 
        float x = Random.Range(-radius + a, radius + a);
        float y_b = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x - a, 2));
        y_b *= Random.Range(0, 2) == 0 ? -1 : 1;
        float y = y_b + b;
 
        Vector3 randomPosition = new Vector3(x, 0, y);
 
        return randomPosition;
    }
    
    IEnumerator SpawnPrototype()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnDelay);
            var enemyPrototype = EnemyPrototypePool.GetObject();
            enemyPrototype.transform.position = GetRandomPosition();
            enemyPrototype.transform.SetParent(transform);
        }
    }
}
 