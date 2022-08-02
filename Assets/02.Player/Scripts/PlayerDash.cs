using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDash : MonoBehaviour
{
    PlayerInfo playerInfo;
    PlayerMoveController playerMoveController;
    Rigidbody2D rb;

    public GameObject dashButton;

    [SerializeField]
    private int maxDashCount = 3;
    public int MaxDashCount
    {
        get {return maxDashCount;}
        set
        {
            maxDashCount = value;
            coolingStack = MaxDashCount - DashCount;
        }
    }
    private int dashCount;
    public int DashCount
    {
        get{ return dashCount;}
        set
        {
            dashCount = value;
            dashButton.GetComponent<SkillStackHandler>().SetStackText(value);
        }
    }
    public float dashCoolTime = 1f;
    public float dashSpeed = 15f;
    private int coolingStack = 0;
    private bool isCooling = false;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerInfo = GetComponent<PlayerInfo>();
        playerMoveController = GetComponent<PlayerMoveController>();
        // dashButton = GameObject.FindWithTag("DASHBUTTON");
        // dashButton.GetComponent<Button>().onClick.AddListener(delegate {Dash();});
        DashCount = MaxDashCount;
    }

    public void Dash()
    {
        if(playerMoveController.IsStop) return;

        if(DashCount > 0)
        {
            DashCount--;
            rb.AddForce(playerInfo.headVector.normalized * dashSpeed, ForceMode2D.Impulse);
            coolingStack++;
        }
    }

    private void Update() {
        if(coolingStack <= 0 || isCooling) return;

        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        isCooling = true;
        coolingStack--;
        float coolTime = dashCoolTime;
        dashButton.GetComponent<SkillCoolTimeHandler>().SetCoolTime(dashCoolTime);
        dashButton.GetComponent<SkillCoolTimeHandler>().StartCoolTime();

        while(isCooling)
        {
            yield return null;
            coolTime -= Time.deltaTime;
            if(coolTime <= 0)
            {
                isCooling = false;
                if(DashCount < MaxDashCount)
                {
                    DashCount++;
                }
                break;
            }
        }
    }

}
