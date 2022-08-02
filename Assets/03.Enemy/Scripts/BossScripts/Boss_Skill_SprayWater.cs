using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skill_SprayWater : BossSkillBase, IBoss_Skill
{
    Boss_Skill_Manager skillManager;
    EnemyInfo enemyInfo;
    public Transform sprayTransform;
    public float shotInterval;
    WaitForSeconds waitForInterval;
    public float maxSpread;
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
        skillManager.currentSkill = this;
        StartCoroutine(SprayWater());
    }

    IEnumerator SprayWater()
    {
        skillManager.audioSource.PlayOneShot(audioClips[0], 0.5f);
        stopHandler.StopMove();
        Vector2 direction;

        for(int i = 0; i < projectileCount; i++)
        {
            direction = Vector2.up + new Vector2(Random.Range(-maxSpread,maxSpread), Random.Range(-maxSpread,maxSpread));
            EnemyProjectile projectile = GetProjectile(WhaleProjectilePool.Instance);
            projectile.transform.SetPositionAndRotation(sprayTransform.position, sprayTransform.rotation);
            projectile.damage = skillManager.skillProjectileDamage;
            projectile.GetComponent<Rigidbody2D>().AddForce(direction * skillManager.skillProjectileSpeed, ForceMode2D.Impulse);
            yield return waitForInterval;
        }
        yield return new WaitForSeconds(1);
        Boss_Skill_Manager.isSkillEnd = true;
        Boss_Skill_Manager.animator.SetTrigger("reset");
        stopHandler.ResumeMove();
        skillManager.audioSource.Stop();
    }
}
