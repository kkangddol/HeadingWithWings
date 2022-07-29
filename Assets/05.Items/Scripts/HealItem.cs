using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour, Item
{
    PlayerInfo playerInfo;
    public TextPopup textPopup;

    private void Start() {
        playerInfo = GameObject.FindWithTag("PLAYER").GetComponent<PlayerInfo>();
    }
    public void UseItem()
    {
        playerInfo.HealthPoint += playerInfo.healAmount;

        //TextPopup newTextPopup = Instantiate<TextPopup>(textPopup, transform.position + Vector3.up, transform.rotation);
        GameObject newTextPopup = ObjectPool.Instance.GetTextObject();
        newTextPopup.transform.position = transform.position + (Vector3.up / 2);
        newTextPopup.GetComponent<TextPopup>().SetHealAmount((int)playerInfo.healAmount);

        Destroy(gameObject);
    }
}
