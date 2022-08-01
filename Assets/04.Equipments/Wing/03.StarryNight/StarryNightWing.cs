using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarryNightWing : Equipment, ActiveWing
{
    const int EQUIPID = 20300;
    PlayerInfo playerInfo;
    public GameObject skillObject;
    public float damageMultiplier;
    public float skillDelayMultiplier;
    public float skillTime;
    private bool isCoolDown = false;
    private float coolTime;

    private const float SKILLSPEED = 1.0f;

    public GameObject skillButton;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        playerInfo = GameObject.FindWithTag("PLAYER").GetComponent<PlayerInfo>();
    }
    public void SetButton(GameObject button)
    {
        skillButton = button;
    }

    public void ActivateSkill()
    {
        if (isCoolDown) return;

        isCoolDown = true;
        Fire();
        StartCoroutine(StopSkill());
    }

    private void Fire()
    {
        //GameObject go = Instantiate(skillObject, this.transform.right * 1.1f, Quaternion.identity);
        GameObject go = Instantiate(skillObject, this.transform.position, Quaternion.identity);
        //go.transform.rotation = Quaternion.AngleAxis(playerInfo.headAngle, Vector3.forward);
        go.transform.eulerAngles = new Vector3(0, 0, playerInfo.headAngle);
        go.GetComponent<StarryNightSkill>().damage = playerInfo.damage * damageMultiplier;
        go.GetComponent<Rigidbody2D>().AddForce(go.transform.right, ForceMode2D.Impulse);
        Destroy(go, skillTime);
    }

    IEnumerator StopSkill()
    {
        yield return new WaitForSeconds(skillTime);
        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        isCoolDown = true;
        coolTime = playerInfo.skillDelay * skillDelayMultiplier;

        skillButton.GetComponent<SkillCoolTimeHandler>().SetCoolTime(coolTime);
        skillButton.GetComponent<SkillCoolTimeHandler>().StartCoolTime();

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
        damageMultiplier = GameManager.Data.WingEquipDict[EQUIPID + this.level].damageMultiplier;
        skillDelayMultiplier = GameManager.Data.WingEquipDict[EQUIPID + this.level].delayMultiplier;
        skillTime = GameManager.Data.WingEquipDict[EQUIPID + this.level].skillTime;
    }
}
