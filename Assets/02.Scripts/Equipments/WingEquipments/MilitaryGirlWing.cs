using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilitaryGirlWing : Equipment
{
    PlayerInfo playerInfo;
    DetectEnemy detectEnemy;
    const string ENEMY = "ENEMY";
    public Bullet bullet;
    public float damageMultiplier;
    public float skillDelayMultiplier;
    public float attackRange;
    public float knockbackSize;
    public float bulletSpeed;

    private Transform targetTransform;
    private bool isCoolDown = false;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        playerInfo = GameManager.playerInfo;
        detectEnemy = GetComponent<DetectEnemy>();
    }

    public void ActivateSkill()
    {
        //집중 포화
        if(isCoolDown) return;

        Bullet newBullet = Instantiate(bullet,transform.position,transform.rotation);

        isCoolDown = true;
        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(playerInfo.skillDelay * (skillDelayMultiplier / 100f));
        isCoolDown = false;
    }


    public override void SetLevel(int newLevel)
    {
        this.level = newLevel;

        //220527 하드코딩이므로 DataManager 이용할 것.
        switch(level)
        {
            case 1:
            {
                break;
            }
            case 2:
            {
                break;
            }
            case 3:
            {
                break;
            }
            case 4:
            {
                break;
            }
            case 5:
            {
                break;
            }
            default:
                break;
        }
    }
}
