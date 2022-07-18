using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarryNightWing : Equipment, ActiveWing
{
    PlayerInfo playerInfo;
    public GameObject skillObject;
    public float skillDelayMultiplier;
    public float skillTime;
    private bool isCoolDown = false;
    public float coolTime;

    private const float SKILLSPEED = 1.0f;

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
        if (isCoolDown) return;

        isCoolDown = true;
        Fire();
        StartCoroutine(StopSkill());
    }

    private void Fire()
    {
        GameObject go = Instantiate(skillObject, this.transform.position, Quaternion.identity);
        go.transform.rotation = Quaternion.AngleAxis(playerInfo.headAngle, Vector3.forward);
        go.GetComponent<Rigidbody2D>().AddForce(Vector2.right, ForceMode2D.Impulse);
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
