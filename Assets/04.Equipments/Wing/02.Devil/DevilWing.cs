using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilWing : Equipment, ActiveWing
{
    const int EQUIPID = 20200;
    PlayerInfo playerInfo;
    public GameObject[] skillObjects;
    public float damageMultiplier;
    public float skillDelayMultiplier;
    public float knockbackSize;
    private bool isCoolDown = false;
    private float coolTime;
    private GameObject skillButton;
    AudioSource audioSource;
    public AudioClip[] audioClips;
    int audioIndex = 0;

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
        audioSource = GetComponent<AudioSource>();
    }

    public void SetButton(GameObject button)
    {
        skillButton = button;
    }

    public void ActivateSkill()
    {
        if (isCoolDown) return;

        StartCoroutine(ClawAttack());
    }

    IEnumerator ClawAttack()
    {
        isCoolDown = true;

        foreach(GameObject skillObject in skillObjects)
        {
            audioSource.PlayOneShot(audioClips[audioIndex++]);
            skillObject.SetActive(true);
            SetSkillInfo(skillObject);

            yield return new WaitForSeconds(0.25f);

            // skillTime 및 StopSkill 고려
        }

        yield return new WaitForSeconds(0.3f);
        SetActiveFalseAll();
        StartCoroutine(CoolDown());
        audioIndex = 0;
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
        knockbackSize = GameManager.Data.WingEquipDict[EQUIPID + this.level].knockBackSize;
    }


}
