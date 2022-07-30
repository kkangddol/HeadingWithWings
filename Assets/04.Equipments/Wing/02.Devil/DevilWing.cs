using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilWing : Equipment, ActiveWing
{
    const int EQUIPID = 20200;
    PlayerInfo playerInfo;
    public Bullet[] skillObjects;
    public float damageMultiplier;
    public float skillDelayMultiplier;
    public float knockbackSize;
    private bool isCoolDown = false;
    private float coolTime;
    public GameObject skillButton;
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
        playerInfo = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<PlayerInfo>();
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

        foreach(Bullet skillObject in skillObjects)
        {
            audioSource.PlayOneShot(audioClips[audioIndex++]);
            skillObject.gameObject.SetActive(true);
            SetSkillInfo(skillObject);

            yield return new WaitForSeconds(0.25f);

            // skillTime 및 StopSkill 고려
        }

        yield return new WaitForSeconds(0.3f);
        SetActiveFalseAll();
        StartCoroutine(CoolDown());
        audioIndex = 0;
    }
    private void SetSkillInfo(Bullet bullet)
    {
        bullet.damage = playerInfo.damage * damageMultiplier;
        bullet.knockbackSize = knockbackSize;
    }

    private void SetActiveFalseAll()
    {
        foreach(Bullet skillObject in skillObjects)
        {
            skillObject.gameObject.SetActive(false);
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
