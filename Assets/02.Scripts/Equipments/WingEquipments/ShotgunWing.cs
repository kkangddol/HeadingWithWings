using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunWing : MonoBehaviour
{
    private PlayerInfo playerInfo;
    private WingDetectEnemy wingDetectEnemy;
    const string ENEMY = "ENEMY";
    public Bullet bullet;
    public int damage;
    public float fireDelay;
    public float knockbackSize;
    public float bulletSpeed;
    public float maxSpread;

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
        Transform targetEnemy = wingDetectEnemy.FindNearestEnemy(ENEMY);

        for(int i = 0; i < 4; i++)
        {
            Bullet newPellet = Instantiate(bullet,transform.position,transform.rotation);
            newPellet.damage = this.damage;
            newPellet.knockbackSize = this.knockbackSize;   //스크립터블 오브젝트로 처리할 예정
            newPellet.transform.LookAt(targetEnemy);
            Vector3 pelletDirection = newPellet.transform.forward + new Vector3(Random.Range(-maxSpread,maxSpread), 0, Random.Range(-maxSpread,maxSpread));
            newPellet.GetComponent<Rigidbody>().AddForce(pelletDirection * bulletSpeed, ForceMode.Impulse);
        }      
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
