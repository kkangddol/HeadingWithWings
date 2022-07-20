using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    PlayerInfo playerInfo;
    PlayerMoveController playerMoveController;
    Rigidbody2D rb;

    public int dashCount = 1;
    public float dashCoolTime = 1f;
    public float dashSpeed = 15f;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerInfo = GetComponent<PlayerInfo>();
        playerMoveController = GetComponent<PlayerMoveController>();
    }

    public void Dash()
    {
        if(playerMoveController.IsStop) return;

        if(dashCount > 0)
        {
            dashCount--;
            rb.AddForce(playerInfo.headVector.normalized * dashSpeed, ForceMode2D.Impulse);
            StartCoroutine(CoolDown());
        }
    }

    IEnumerator CoolDown()
    {
        float coolTime = dashCoolTime;
        bool isCoolDown = true;
        while(isCoolDown)
        {
            yield return null;
            coolTime -= Time.deltaTime;
            if(coolTime <= 0)
            {
                isCoolDown = false;
                dashCount++;
                break;
            }
        }
    }

}
