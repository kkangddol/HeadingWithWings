using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilitaryGirlWing : Equipment, ActiveWing
{
    PlayerInfo playerInfo;
    PlayerMoveController playerMoveController;
    private bool isCoolDown = false;
    [HideInInspector] public float coolTime;
    
    public Bullet militaryBullet;
    public float damageMultiplier;
    public float skillDelayMultiplier;
    public float knockbackSize = 0.1f;
    public int fireCount = 10;
    public float fireInterval = 0.5f;
    WaitForSeconds waitInterval;
    public int bulletCount = 7;
    public float bulletSpeed = 5;
    public float bulletSpread = 30;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        playerInfo = GameObject.FindWithTag("PLAYER").GetComponent<PlayerInfo>();
        playerMoveController = GameObject.FindWithTag("PLAYER").GetComponent<PlayerMoveController>();
        waitInterval = new WaitForSeconds(fireInterval);
    }

    private void LateUpdate() {
        transform.eulerAngles = new Vector3(0, 0, playerInfo.headAngle);
    }

    public void ActivateSkill()
    {
        //집중 포화
        if(isCoolDown) return;

        StartCoroutine(ConcentrateFire());
    }

    IEnumerator ConcentrateFire()
    {
        isCoolDown = true;
        playerMoveController.StopMove();

        for(int i = 0; i < fireCount; i++)
        {
            for(int j = 0; j < bulletCount; j++)
            {
                Bullet newBullet = Instantiate(militaryBullet, transform.position, transform.rotation);
                newBullet.damage = playerInfo.damage * damageMultiplier;
                newBullet.knockbackSize = this.knockbackSize;
                //Vector2 bulletDirection = new Vector2()
                newBullet.transform.Rotate(0, 0, j * (bulletSpread / bulletCount) - (bulletSpread / 2));
                newBullet.GetComponent<Rigidbody2D>().AddForce(newBullet.transform.right * bulletSpeed, ForceMode2D.Impulse);
            }
            yield return waitInterval;
        }

        playerMoveController.ResumeMove();
        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        coolTime = playerInfo.skillDelay * skillDelayMultiplier;
        while(isCoolDown)
        {
            yield return null;
            coolTime -= Time.deltaTime;
            if(coolTime <= 0)
            {
                isCoolDown = false;
                break;
            }
        }
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
