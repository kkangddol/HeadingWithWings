using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skill_SprayWater : MonoBehaviour, IBoss_Skill
{
    Boss_Skill_Manager skillManager;
    EnemyInfo enemyInfo;
    public Transform sprayTransform;
    public EnemyProjectile projectile;
    public float damage;
    public float shotInterval;
    WaitForSeconds waitForInterval;
    public float maxSpread;
    public float projectileSpeed;
    public int projectileCount;
    public GameObject effect;
    IEnemyStopHandler stopHandler;
    public AudioClip[] audioClips;

    private void Start() {
        enemyInfo = GetComponent<EnemyInfo>();
        stopHandler = GetComponent<IEnemyStopHandler>();
        waitForInterval = new WaitForSeconds(shotInterval);
        skillManager = GetComponent<Boss_Skill_Manager>();
    }

    public void ActivateSkill()
    {
        StartCoroutine(SprayWater());
    }

    IEnumerator SprayWater()
    {
        skillManager.audioSource.PlayOneShot(audioClips[0]);
        stopHandler.StopMove();
        Vector2 direction;

        for(int i = 0; i < projectileCount; i++)
        {
            direction = Vector2.up + new Vector2(Random.Range(-maxSpread,maxSpread), Random.Range(-maxSpread,maxSpread));
            EnemyProjectile newProjectile = Instantiate<EnemyProjectile>(projectile, sprayTransform.position, sprayTransform.rotation);
            newProjectile.damage = skillManager.skillProjectileDamage;
            newProjectile.GetComponent<Rigidbody2D>().AddForce(direction * projectileSpeed, ForceMode2D.Impulse);
            yield return waitForInterval;
        }
        yield return new WaitForSeconds(1);
        Boss_Skill_Manager.animator.SetTrigger("reset");
        stopHandler.ResumeMove();
        skillManager.audioSource.Stop();
    }
}
