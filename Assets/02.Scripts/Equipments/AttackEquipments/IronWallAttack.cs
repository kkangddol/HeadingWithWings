using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronWallAttack : Equipment
{
    PlayerInfo playerInfo;
    DetectEnemy detectEnemy;
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
    private Transform targetTransform;
    private bool isCoolDown = false;


    private void Start()
    {
        Initialize();
        StartCoroutine(FireCycle());
    }

    void Initialize()
    {
        playerInfo = GameManager.playerInfo;
        detectEnemy = GetComponent<DetectEnemy>();
    }
    
    private void Update()
    {
        this.transform.rotation = Quaternion.AngleAxis(playerInfo.headAngle, Vector3.forward);
    }

    void Fire()
    {
        isCoolDown = true;
        if(bulletFront.gameObject.activeSelf == false)
        {
            bulletFront.gameObject.SetActive(true);
            bulletFront.damage = playerInfo.damage * damageMultiplier;
            bulletFront.knockbackSize = knockbackSize;
            ((Bullet_IronWall)bulletFront).collisionCount = collisionCount;
            ((Bullet_IronWall)bulletFront).ironWallAttack = this;
        }

        if(bulletBack.gameObject.activeSelf == false && isPair)
        {
            bulletBack.gameObject.SetActive(true);
            bulletBack.damage = playerInfo.damage * damageMultiplier;
            bulletBack.knockbackSize = knockbackSize;
            ((Bullet_IronWall)bulletBack).collisionCount = collisionCount;
            ((Bullet_IronWall)bulletBack).ironWallAttack = this;
        }
        
    }

    IEnumerator FireCycle()
    {
        while (true)
        {
            yield return null;

            if (isCoolDown) continue;

            Fire();
        }
    }

    public IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(playerInfo.attackDelay * attackDelayMultiplier);
        isCoolDown = false;
    }

    public override void SetLevel(int newLevel)
    {
        this.level = newLevel;

        //220527 하드코딩이므로 DataManager 이용할 것.
        switch (level)
        {
            case 1:
                {
                    damageMultiplier = 0.50f;
                    attackDelayMultiplier = 1.50f;
                    collisionCount = 5;
                    break;
                }
            case 2:
                {
                    damageMultiplier = 0.55f;
                    attackDelayMultiplier = 1.40f;
                    collisionCount = 6;
                    break;
                }
            case 3:
                {
                    damageMultiplier = 0.60f;
                    attackDelayMultiplier = 1.30f;
                    collisionCount = 7;
                    break;
                }
            case 4:
                {
                    damageMultiplier = 0.65f;
                    attackDelayMultiplier = 1.20f;
                    collisionCount = 8;
                    break;
                }
            case 5:
                {
                    damageMultiplier = 0.70f;
                    attackDelayMultiplier = 1.10f;
                    isPair = true;
                    collisionCount = 10;
                    break;
                }
            default:
                break;
        }
    }
}
