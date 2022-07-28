using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperAttack : Equipment
{
    PlayerInfo playerInfo;
    DetectEnemy detectEnemy;
    const int equipID = 10300;
    const string ENEMY = "ENEMY";
    public Bullet bullet;
    public float damageMultiplier;
    public float attackDelayMultiplier;
    public float attackRange;
    public float knockbackSize;
    public float bulletSpeed;
    public float headShotChance = 0;
    public float headShotDamageMultiplier;
    AudioSource audioSource;
    public AudioClip[] audioClips;


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
        audioSource = GetComponent<AudioSource>();
    }

    void Fire()
    {
        Bullet newBullet = Instantiate(bullet,transform.position,transform.rotation);
        newBullet.transform.LookAt(targetTransform);
        newBullet.damage = playerInfo.damage * damageMultiplier;
        newBullet.knockbackSize = knockbackSize;
        ((Bullet_Sniper)newBullet).headShotChance = headShotChance;
        ((Bullet_Sniper)newBullet).headShotChance = headShotDamageMultiplier;
        newBullet.GetComponent<Rigidbody2D>().AddForce(newBullet.transform.forward * bulletSpeed, ForceMode2D.Impulse);
        isCoolDown = true;
        audioSource.PlayOneShot(audioClips[0]);
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
        yield return new WaitForSeconds(playerInfo.attackDelay * attackDelayMultiplier - 2);
        audioSource.PlayOneShot(audioClips[1]);
        yield return new WaitForSeconds(2);
        isCoolDown = false;
    }

    public override void SetLevel(int newLevel)
    {
        this.level = newLevel;
        damageMultiplier = GameManager.Data.AttackEquipDict[equipID + this.level].damageMultiplier;
        attackDelayMultiplier = GameManager.Data.AttackEquipDict[equipID + this.level].delayMultiplier;
        knockbackSize = GameManager.Data.AttackEquipDict[equipID + this.level].knockBackSize;
        headShotChance = GameManager.Data.AttackEquipDict[equipID + this.level].headShotChance;
        headShotDamageMultiplier = GameManager.Data.AttackEquipDict[equipID + this.level].headShotDamageMultiplier;
    }
}
