using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilitaryGirlWing : Equipment, ActiveWing
{
    PlayerInfo playerInfo;
    public GameObject skillObject;
    public float damageMultiplier;
    public float skillDelayMultiplier;
    public float knockbackSize;
    public float skillTime;
    private bool isCoolDown = false;
    public float coolTime;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        playerInfo = GameManager.playerInfo;
    }

    private void LateUpdate() {
        transform.eulerAngles = new Vector3(0,0,playerInfo.headAngle);
    }

    public void ActivateSkill()
    {
        //집중 포화
        if(isCoolDown) return;
        
        skillObject.GetComponent<MilitaryGirlSkill>().damage = playerInfo.damage * (damageMultiplier / 100f);
        skillObject.GetComponent<MilitaryGirlSkill>().knockbackSize = knockbackSize;
        ConcentrateFire();
    }

    void ConcentrateFire()
    {
        isCoolDown = true;
        skillObject.SetActive(true);
        skillObject.GetComponent<MilitaryGirlSkill>().Init();
        StartCoroutine(StopSkill());
    }

    IEnumerator StopSkill()
    {
        yield return new WaitForSeconds(skillTime);
        skillObject.SetActive(false);
        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        isCoolDown = true;
        coolTime = playerInfo.skillDelay * (skillDelayMultiplier / 100f);
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
