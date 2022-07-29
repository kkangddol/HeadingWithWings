using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherAttack : Equipment
{
    PlayerInfo playerInfo;
    DetectEnemy detectEnemy;
    const int equipID = 10100;
    const string ENEMY = "ENEMY";
    public Bullet bullet;
    public Bullet FeatherBullet;
    public Bullet PenetrateFeatherBullet;
    public float damageMultiplier;
    public float attackDelayMultiplier;
    public float attackRange;
    public float knockbackSize;
    public float bulletSpeed;

    private Transform targetTransform;
    private bool isCoolDown = false;


    private void Start()
    {
        Initialize();
        StartCoroutine(FireCycle());
    }

    void Initialize()
    {
        playerInfo = GameObject.FindWithTag("PLAYER").GetComponent<PlayerInfo>();
        detectEnemy = GetComponent<DetectEnemy>();
    }

    void Fire()
    {
        Bullet newBullet = Instantiate(bullet,transform.position,transform.rotation);
        newBullet.damage = playerInfo.damage * damageMultiplier;
        newBullet.knockbackSize = knockbackSize;
        newBullet.transform.rotation = Utilities.LookAt2(this.transform, targetTransform);
        newBullet.GetComponent<Rigidbody2D>().AddForce((targetTransform.position - transform.position).normalized * bulletSpeed, ForceMode2D.Impulse);
        isCoolDown = true;
        StartCoroutine(CoolDown());
    }

    IEnumerator FireCycle()
    {
        while(true)
        {
            yield return null;
            targetTransform = detectEnemy.FindNearestEnemy(ENEMY);

            if(targetTransform == transform) continue;

            if(Vector2.Distance(transform.position, targetTransform.position) > attackRange) continue;

            if(!isCoolDown)
            {
                Fire();
            }
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(playerInfo.attackDelay * attackDelayMultiplier);
        isCoolDown = false;
    }

    public override void SetLevel(int newLevel)
    {
        this.level = newLevel;
        damageMultiplier = GameManager.Data.AttackEquipDict[equipID + this.level].damageMultiplier;
        attackDelayMultiplier = GameManager.Data.AttackEquipDict[equipID + this.level].delayMultiplier;
        knockbackSize = GameManager.Data.AttackEquipDict[equipID + this.level].knockBackSize;
        // attackRange = GameManager.Data.AttackEquipDict[equipID + this.level].attackRange;
        // bulletSpeed = GameManager.Data.AttackEquipDict[equipID + this.level].bulletSpeed;
    }
}