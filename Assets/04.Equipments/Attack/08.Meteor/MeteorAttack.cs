using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorAttack : Equipment
{
    PlayerInfo playerInfo;
    const int equipID = 10800;
    public float damageMultiplier;
    public float attackDelayMultiplier;
    public float attackRange;
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
        playerInfo = GameObject.FindWithTag(PLAYER).GetComponent<PlayerInfo>();
    }

    public void Fire()
    {
        Bullet bullet = MeteorBulletPool.Instance.GetObject().GetComponentInChildren<Bullet>();
        bullet.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        bullet.damage = playerInfo.damage * damageMultiplier;
        ((Bullet_Meteor)bullet).dotDamage = bullet.damage * 0.1f;
        bullet.knockbackSize = knockbackSize;
        ((Bullet_Meteor)bullet).isGTAEMeteor = isGTAEMeteor;
        bullet.transform.parent.position = (Vector2)this.transform.position + Random.insideUnitCircle * attackRange;
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
        damageMultiplier = GameManager.Data.AttackEquipDict[equipID + this.level].damageMultiplier;
        attackDelayMultiplier = GameManager.Data.AttackEquipDict[equipID + this.level].delayMultiplier;
        knockbackSize = GameManager.Data.AttackEquipDict[equipID + this.level].knockBackSize;
        attackRange = GameManager.Data.AttackEquipDict[equipID + this.level].attackRange;
        meteorCount = GameManager.Data.AttackEquipDict[equipID + this.level].meteorCount;
        isGTAEMeteor = GameManager.Data.AttackEquipDict[equipID + this.level].isGTAEMeteor;
    }
}
