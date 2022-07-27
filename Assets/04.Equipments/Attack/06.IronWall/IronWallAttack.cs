using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronWallAttack : Equipment
{
    PlayerInfo playerInfo;
    DetectEnemy detectEnemy;
    const int equipID = 600;
    const string ENEMY = "ENEMY";
    public Bullet bulletFront;
    public Bullet bulletBack;
    public float damageMultiplier;
    public float attackDelayMultiplier;
    public float attackRange;
    public float knockbackSize;
    public float bulletSpeed;
    public int collisionCount;

    private bool isPair = false;
    private int wallCount = 0;
    private Transform targetTransform;
    private bool isCoolDown = false;
    Coroutine coroutine;


    private void Start()
    {
        Initialize();
        //StartCoroutine(FireCycle());
    }

    void Initialize()
    {
        playerInfo = GameObject.FindWithTag("PLAYER").GetComponent<PlayerInfo>();
        detectEnemy = GetComponent<DetectEnemy>();
    }
    
    private void Update()
    {
        this.transform.rotation = Quaternion.AngleAxis(playerInfo.headAngle, Vector3.forward);
        if(!isCoolDown)
        {
            Fire();
        } 
    }

    void Fire()
    {
        isCoolDown = true;
        if(bulletFront.gameObject.activeSelf == false)
        {
            bulletFront.gameObject.SetActive(true);
            wallCount++;
            bulletFront.damage = playerInfo.damage * damageMultiplier;
            bulletFront.knockbackSize = knockbackSize;
            ((Bullet_IronWall)bulletFront).collisionCount = collisionCount;
            ((Bullet_IronWall)bulletFront).ironWallAttack = this;
        }

        if(bulletBack.gameObject.activeSelf == false && isPair)
        {
            wallCount++;
            bulletBack.gameObject.SetActive(true);
            bulletBack.damage = playerInfo.damage * damageMultiplier;
            bulletBack.knockbackSize = knockbackSize;
            ((Bullet_IronWall)bulletBack).collisionCount = collisionCount;
            ((Bullet_IronWall)bulletBack).ironWallAttack = this;
        }
        
    }

    // IEnumerator FireCycle()
    // {
    //     while (true)
    //     {
    //         yield return null;

    //         if (isCoolDown) continue;

    //         Fire();
    //     }
    // }

    public IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(playerInfo.attackDelay * attackDelayMultiplier);
        isCoolDown = false;
        wallCount = 0;
    }

    public IEnumerator CoolDown(float time)
    {
        bulletFront.GetComponent<Bullet_IronWall>().Broken();
        yield return new WaitForSeconds(time);
        isCoolDown = false;
        wallCount = 0;
    }

    public void WallBroken()
    {
        if(--wallCount <= 0)
        {
            coroutine = StartCoroutine(CoolDown());
        }
    }

    public override void SetLevel(int newLevel)
    {
        this.level = newLevel;
        damageMultiplier = GameManager.Data.AttackEquipDict[equipID + this.level].damageMultiplier;
        attackDelayMultiplier = GameManager.Data.AttackEquipDict[equipID + this.level].delayMultiplier;
        knockbackSize = GameManager.Data.AttackEquipDict[equipID + this.level].knockBackSize;
        collisionCount = GameManager.Data.AttackEquipDict[equipID + this.level].collisionCount;
        isPair = GameManager.Data.AttackEquipDict[equipID + this.level].isPair;
        CoolDown(1.5f);
    }
}
