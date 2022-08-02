using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDropItems : MonoBehaviour
{
    EnemyInfo enemyInfo;
    public GameObject silverPrefab;
    public GameObject goldPrefab;
    Transform itemParent;
    public float silverInterval;
    public float goldInterval;
    WaitForSeconds waitSilver;
    WaitForSeconds waitGold;
    private void Start() {
        enemyInfo = GetComponent<EnemyInfo>();
        enemyInfo.boss += delegate {BossDropStart();};
        itemParent = GameObject.FindWithTag("ITEMPARENT").GetComponent<Transform>();
        waitSilver = new WaitForSeconds(silverInterval);
        waitGold = new WaitForSeconds(goldInterval);
    }

    void BossDropStart()
    {
        StartCoroutine(ShootItem(50, silverPrefab, waitSilver));
        StartCoroutine(ShootItem(10, goldPrefab, waitGold));
    }


    IEnumerator ShootItem(int count, GameObject prefab, WaitForSeconds wait)
    {
        Vector2 direction;
        for(int i = 0; i < count; i++)
        {
            direction = Vector2.up + new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f));
            var coin = Instantiate(prefab, transform.position, Quaternion.identity, itemParent);
            coin.GetComponent<Rigidbody2D>().AddForce(direction * 20, ForceMode2D.Impulse);
            yield return wait;
        }
    }
}
