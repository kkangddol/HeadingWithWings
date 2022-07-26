using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorAttack : Equipment
{
    PlayerInfo playerInfo;
    const string ENEMY = "ENEMY";
    const int MAXMETEOR = 10;
    public GameObject bullet;
    public float damageMultiplier;
    public float attackDelayMultiplier;
    public float attackRange = 2f;
    public float knockbackSize;
    // public float bulletSpeed;

    public int meteorCount = 3;
    // GTAE = Ground Target Area of Effect
    private float meteorDelay = 0.3f;
    private bool isGTAEMeteor = false;
    private bool isCoolDown = false;

    private void Start()
    {
        Initialize();
        StartCoroutine(FireCycle());
    }

    void Initialize()
    {
        playerInfo = GameObject.FindWithTag("PLAYER").GetComponent<PlayerInfo>();
    }

    public void Fire()
    {
        Bullet newBullet = Instantiate(bullet, transform.position, transform.rotation).GetComponentInChildren<Bullet>();
        newBullet.damage = playerInfo.damage * damageMultiplier;
        ((Bullet_Meteor)newBullet).dotDamage = newBullet.damage * 0.1f;
        newBullet.knockbackSize = knockbackSize;
        ((Bullet_Meteor)newBullet).isGTAEMeteor = isGTAEMeteor;
        newBullet.transform.parent.position = (Vector2)this.transform.position + Random.insideUnitCircle * attackRange;
    }
    
    IEnumerator MeteorFire()
    {
        isCoolDown = true;
        for (int i = 0; i < meteorCount; i++)
        {
            Fire();
            yield return new WaitForSeconds(meteorDelay);
        }

        StartCoroutine(CoolDown());
    }

    IEnumerator FireCycle()
    {
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            yield return null;

            if (!isCoolDown)
            {
                StartCoroutine(MeteorFire());
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

        //220527 하드코딩이므로 DataManager 이용할 것.
        switch (level)
        {
            case 1:
                {
                    damageMultiplier = 0.35f;
                    attackDelayMultiplier = 5.00f;
                    meteorCount = 3;
                    break;
                }
            case 2:
                {
                    damageMultiplier = 0.40f;
                    attackDelayMultiplier = 4.90f;
                    meteorCount = 4;
                    break;
                }
            case 3:
                {
                    damageMultiplier = 0.45f;
                    attackDelayMultiplier = 4.80f;
                    meteorCount = 5;
                    break;
                }
            case 4:
                {
                    damageMultiplier = 0.50f;
                    attackDelayMultiplier = 4.70f;
                    meteorCount = 6;
                    break;
                }
            case 5:
                {
                    isGTAEMeteor = true;
                    damageMultiplier = 0.55f;
                    attackDelayMultiplier = 4.60f;
                    meteorCount = MAXMETEOR;
                    break;
                }
            default:
                break;
        }
    }
}
