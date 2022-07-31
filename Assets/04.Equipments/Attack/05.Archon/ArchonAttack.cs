using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchonAttack : Equipment
{
    PlayerInfo playerInfo;
    DetectEnemy detectEnemy;
    const int equipID = 10500;
    public Bullet bullet;
    public float damageMultiplier;
    public float attackDelayMultiplier;
    public float attackRange;
    public float knockbackSize;
    public float bulletSpeed;
    public float splashRange;

    private Vector3 toTargetNormal = Vector3.zero;
    private float toTargetAngle = 0.0f;
    private Vector3 Scaling = Vector3.one;
    private Transform targetTransform;
    private bool isCoolDown = false;
    AudioSource audioSource;
    public AudioClip[] audioClips;


    private void Start()
    {
        Initialize();
        StartCoroutine(FireCycle());
    }

    void Initialize()
    {
        playerInfo = GameObject.FindWithTag(PLAYER).GetComponent<PlayerInfo>();
        detectEnemy = GetComponent<DetectEnemy>();
        audioSource = GetComponent<AudioSource>();
    }

    void Fire()
    {
        bullet.damage = playerInfo.damage * damageMultiplier;
        bullet.knockbackSize = knockbackSize;
        ((Bullet_Archon)bullet).splashRange = splashRange;
        StartCoroutine(BulletScaling());
        ((Bullet_Archon)bullet).SplashDamage(targetTransform);
        isCoolDown = true;
        audioSource.PlayOneShot(audioClips[0]);
        StartCoroutine(CoolDown());
    }

    IEnumerator BulletScaling()
    {
        toTargetNormal = (targetTransform.position - this.transform.position).normalized;
        toTargetAngle = Mathf.Atan2(toTargetNormal.y, toTargetNormal.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(toTargetAngle, Vector3.forward);
        Scaling.x = Vector2.Distance(targetTransform.position, this.transform.position);
        transform.localScale = Scaling;

        yield return new WaitForSeconds(0.2f);

        transform.localScale = Vector3.zero;
    }

    IEnumerator FireCycle()
    {
        while (true)
        {
            yield return null;

            if(isCoolDown)  continue;

            targetTransform = detectEnemy.FindNearestEnemy(ENEMY);

            if (targetTransform == transform)  continue;

            if (Vector2.Distance(transform.position, targetTransform.position) > attackRange)  continue;

            Fire();
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
        splashRange = GameManager.Data.AttackEquipDict[equipID + this.level].splashRange;
    }
}

