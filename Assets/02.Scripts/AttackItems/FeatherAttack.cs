using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherAttack : MonoBehaviour
{
    PlayerInfo playerInfo;
    DetectEnemy detectEnemy;
    const string ENEMY = "ENEMY";
    public Bullets bullet;
    public float damageMultiplier;
    public float attackDelayMultiplier;
    public float bulletSpeed;

    private void Start()
    {
        Initialize();
        StartCoroutine(FireCycle());
    }

    void Initialize()
    {
        playerInfo = GetComponentInParent<PlayerInfo>();
        detectEnemy = GetComponent<DetectEnemy>();
    }

    void Fire()
    {
        Transform targetTransform = detectEnemy.FindNearestEnemy(ENEMY);
        if(targetTransform == transform)
        {
            return;
        }
        Bullets newBullet = Instantiate(bullet,transform.position,transform.rotation);
        newBullet.transform.LookAt(targetTransform);
        newBullet.damage = playerInfo.damage * (damageMultiplier / 100f);
        newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    IEnumerator FireCycle()
    {
        while(true)
        {
            yield return new WaitForSeconds(playerInfo.attackDelay * (attackDelayMultiplier / 100f));
            Fire();
        }
    }
}
