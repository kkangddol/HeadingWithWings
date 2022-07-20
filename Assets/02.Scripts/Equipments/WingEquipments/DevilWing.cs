using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilWing : Equipment, ActiveWing
{
    PlayerInfo playerInfo;
    public GameObject[] skillObjects;
    public float damageMultiplier;
    public float skillDelayMultiplier;
    public float knockbackSize;
    public float skillTime;
    private bool isCoolDown = false;
    private float coolTime;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        this.transform.rotation = Quaternion.AngleAxis(playerInfo.headAngle, Vector3.forward);
    }

    void Initialize()
    {
        playerInfo = GameManager.playerInfo;
    }

    public void ActivateSkill()
    {
        //집중 포화
        if (isCoolDown) return;

        StartCoroutine(ClawAttack());
    }

    IEnumerator ClawAttack()
    {
        isCoolDown = true;

        foreach(GameObject skillObject in skillObjects)
        {
            skillObject.SetActive(true);
            SetSkillInfo(skillObject);

            yield return new WaitForSeconds(0.25f);

            // skillTime 및 StopSkill 고려
        }

        yield return new WaitForSeconds(0.3f);
        SetActiveFalseAll();
        StartCoroutine(CoolDown());
    }
    private void SetSkillInfo(GameObject go)
    {
        DevilWingSkill temp = go.GetComponent<DevilWingSkill>();
        temp.damage = playerInfo.damage * damageMultiplier;
        temp.knockbackSize = knockbackSize;
        temp.Init();
    }

    private void SetActiveFalseAll()
    {
        foreach(GameObject skillObject in skillObjects)
        {
            skillObject.SetActive(false);
        }
    }

    IEnumerator CoolDown()
    {
        isCoolDown = true;
        coolTime = playerInfo.skillDelay * skillDelayMultiplier;
        while (isCoolDown)
        {
            yield return null;
            coolTime -= Time.deltaTime;
            if (coolTime <= 0)
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
        switch (level)
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
