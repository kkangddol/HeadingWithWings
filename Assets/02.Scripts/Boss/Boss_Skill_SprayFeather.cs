using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skill_SprayFeather : MonoBehaviour, IBoss_Skill
{
    //플레이어 방향 또는 전 방향으로 페가수스의 깃털을 흩뿌립니다. (탄막패턴)
    //깃털을 흩뿌리는것은 ShotgunAttack.cs 스크립트와 MilitaryGirlWing.cs 의 공격 코드를 참고하시면 좋을 것 같습니다!
    public int featherCount = 50;
    public Transform featherPrefab = null;
    public float bulletSpeed = 7f;

    private float angle = 0f;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        angle = 180f / featherCount;
    }

    public void ActivateSkill()
    {
        Boss_Skill_Manager.animator.SetTrigger("reset");

        SprayFeather();
    }

    public void SprayFeather()
    {
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
        Transform temp = Instantiate(featherPrefab, this.transform.position, Quaternion.identity);
        temp.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        temp.GetComponent<Rigidbody2D>().AddForce(temp.right * bulletSpeed, ForceMode2D.Impulse);
    }

    private void SpreadAttack()
    {
        float toPlayerAngle = Utilities.GetAngle(this.transform, GameManager.playerTransform);
        toPlayerAngle -= 90f;

        for (float i = 0f; i <= 180f; i += angle)
        {
            Fire(toPlayerAngle + i);
        }
    }

    IEnumerator PingPongAttack()
    {
        yield return new WaitForSeconds(1f);

        int flip = 1;
        for (float i = 0f; i >= 0; i += angle * flip)
        {
            float toPlayerAngle = Utilities.GetAngle(this.transform, GameManager.playerTransform);
            toPlayerAngle -= 90f;

            Fire(toPlayerAngle + i);
            if(i >= 180)
            {
                flip = -1;
                i -= angle;
            }  
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator ConcentrateAttack()
    {
        int firedFeather = 0;

        while(firedFeather < featherCount)
        {
            Fire(Utilities.GetAngle(this.transform, GameManager.playerTransform));
            firedFeather++;
            yield return new WaitForSeconds(0.15f);
        }
    }
}
