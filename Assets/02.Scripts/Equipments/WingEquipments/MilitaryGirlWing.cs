using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilitaryGirlWing : Equipment, ActiveWing
{
    PlayerInfo playerInfo;
    public GameObject skillEffect;
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

    public void ActivateSkill()
    {
        //집중 포화
        if(isCoolDown) return;
        ConcentrateFire();
    }

    void ConcentrateFire()
    {
        isCoolDown = true;
        skillEffect.SetActive(true);
        StartCoroutine(StopSkill());
    }

    IEnumerator StopSkill()
    {
        yield return new WaitForSeconds(skillTime);
        skillEffect.SetActive(false);
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
