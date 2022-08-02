using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skill_SprayFeather : BossSkillBase, IBoss_Skill
{
    //플레이어 방향 또는 전 방향으로 페가수스의 깃털을 흩뿌립니다. (탄막패턴)
    //깃털을 흩뿌리는것은 ShotgunAttack.cs 스크립트와 MilitaryGirlWing.cs 의 공격 코드를 참고하시면 좋을 것 같습니다!
    public int featherCount = 50;
    public Transform bossSprite = null;
    private EnemyInfo enemyInfo = null;
    private Boss_Skill_Manager skillManager = null;
    private float angle = 0f;
    private bool isAnim = false;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        angle = 180f / featherCount;
        enemyInfo = GetComponent<EnemyInfo>();
        skillManager = GetComponent<Boss_Skill_Manager>();
    }

    public void ActivateSkill()
    {
        skillManager.currentSkill = this;
        SprayFeather();
    }

    private void Update()
    {
        if(isAnim)
        {
            RotateAtPlayer2D();
        }
    }
    private void RotateAtPlayer2D()
    {
        float relativeX = transform.position.x - enemyInfo.playerTransform.position.x;

        if (relativeX > 0)
            bossSprite.localRotation = Quaternion.AngleAxis(-45f, Vector3.forward);
        else if (relativeX < 0)
            bossSprite.localRotation = Quaternion.AngleAxis(45f, Vector3.forward);
    }

    private void EndSequence()
    {
        isAnim = false;
        bossSprite.localRotation = Quaternion.identity;
        Boss_Skill_Manager.isSkillEnd = true;
        Boss_Skill_Manager.animator.SetTrigger("reset");
    }

    public void SprayFeather()
    {
        isAnim = true;
        int random = Random.Range(0, 3);
        switch(random)
        {
            case 0:
                SpreadAttack();
                break;
            case 1:
                StartCoroutine(PingPongAttack());
                break;
            case 2:
                StartCoroutine(ConcentrateAttack());
                break;
        }
    }

    private void Fire(float angle)
    {
        EnemyProjectile projectile = GetProjectile(BasicProjectilePool.Instance);
        projectile.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
        projectile.damage = skillManager.skillProjectileDamage;
        projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        projectile.GetComponent<Rigidbody2D>().AddForce(projectile.transform.right * skillManager.skillProjectileSpeed, ForceMode2D.Impulse);
    }

    private void SpreadAttack()
    {
        float toPlayerAngle = Utilities.GetAngle(this.transform, enemyInfo.playerTransform);
        toPlayerAngle -= 90f;

        for (float i = 0f; i <= 180f; i += angle)
        {
            Fire(toPlayerAngle + i);
        }

        EndSequence();
    }

    IEnumerator PingPongAttack()
    {
        yield return new WaitForSeconds(1f);

        int flip = 1;
        for (float i = 0f; i >= 0; i += angle * flip)
        {
            float toPlayerAngle = Utilities.GetAngle(this.transform, enemyInfo.playerTransform);
            toPlayerAngle -= 90f;

            Fire(toPlayerAngle + i);
            if(i >= 180)
            {
                flip = -1;
                i -= angle;
            }  
            yield return new WaitForSeconds(0.1f);
        }

        EndSequence();
    }

    IEnumerator ConcentrateAttack()
    {
        int firedFeather = 0;

        while(firedFeather < featherCount)
        {
            Fire(Utilities.GetAngle(this.transform, enemyInfo.playerTransform));
            firedFeather++;
            yield return new WaitForSeconds(0.15f);
        }

        EndSequence();
    }
}
