using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAttack : Equipment
{
    PlayerInfo playerInfo;
    DetectEnemy detectEnemy;
    const int equipID = 200;
    const string ENEMY = "ENEMY";
    public Bullet bullet;
    public float damageMultiplier;
    public float attackDelayMultiplier;
    public float attackRange;
    public float knockbackSize;
    public float bulletSpeed;
    public int pelletCount;
    public float pelletSpread = 30;

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

    public void Fire()
    {
        Vector2 enemyDirection = (targetTransform.position - transform.position).normalized;
        for(int i = 0; i < pelletCount; i++)
        {
            Bullet newPellet = Instantiate(bullet,transform.position,transform.rotation);
            newPellet.damage = playerInfo.damage * damageMultiplier;
            newPellet.knockbackSize = this.knockbackSize;
            newPellet.transform.right = enemyDirection;
            newPellet.transform.Rotate(0, 0, i * (pelletSpread / pelletCount) - (pelletSpread / 2));
            newPellet.GetComponent<Rigidbody2D>().AddForce(newPellet.transform.right * bulletSpeed, ForceMode2D.Impulse);
        }
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
        pelletCount = GameManager.Data.AttackEquipDict[equipID + this.level].pelletCount;
    }
}
