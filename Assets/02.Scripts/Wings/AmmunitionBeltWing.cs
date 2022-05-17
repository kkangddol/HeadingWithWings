using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AmmunitionBeltWing : Wings, IWingProjectile, IWingDetectEnemy
{
    public int damage;
    public Bullets bullet;
    public float fireDelay;

    private void Start()
    {
        StartCoroutine(FireCylce());
    }
    public void Fire()
    {
        Bullets newBullet = Instantiate(bullet,transform.position,transform.rotation);
        newBullet.transform.LookAt(FindNearestEnemy(ENEMY));
        newBullet.damage = this.damage;
        newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * 15f, ForceMode.Impulse);
    }

    public IEnumerator FireCylce()
    {
        while(true)
        {
            yield return new WaitForSeconds(fireDelay);
            Fire();
        }

    }

    public Transform FindNearestEnemy(string tag)
    {
        // 탐색할 오브젝트 목록을 List 로 저장합니다.
        var objects = GameObject.FindGameObjectsWithTag(tag).ToList();

        // LINQ 메소드를 이용해 가장 가까운 적을 찾습니다.
        var neareastObject = objects
        .OrderBy(obj =>
        {
            return Vector3.Distance(transform.position, obj.transform.position);
        })
        .FirstOrDefault();

        return neareastObject.transform;
    }
}
