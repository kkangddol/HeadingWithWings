using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionBeltWing : MonoBehaviour
{
    PlayerInfo playerInfo;
    WingDetectEnemy wingDetectEnemy;
    const string ENEMY = "ENEMY";
    public Bullets bullet;
    public int damage;
    public float fireDelay;
    public float bulletSpeed;

    private void Start()
    {
        Initialize();
        StartCoroutine(FireCylce());
    }

    void Initialize()
    {
        playerInfo = GetComponent<PlayerInfo>();
        wingDetectEnemy = GetComponent<WingDetectEnemy>();
    }

    public void Fire()
    {
        Transform targetTransform = wingDetectEnemy.FindNearestEnemy(ENEMY);
        Bullets newBullet = Instantiate(bullet,transform.position,transform.rotation);
        newBullet.transform.LookAt(targetTransform);
        newBullet.damage = this.damage;
        newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    public IEnumerator FireCylce()
    {
        while(true)
        {
            yield return new WaitForSeconds(fireDelay);
            Fire();
        }
    }
}
