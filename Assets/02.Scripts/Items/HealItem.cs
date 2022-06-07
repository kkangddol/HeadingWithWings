using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour, Item
{
    public TextPopup textPopup;
    public void UseItem()
    {
        GameManager.playerInfo.HealthPoint += GameManager.playerInfo.healAmount;

        TextPopup newTextPopup = Instantiate<TextPopup>(textPopup, transform.position + Vector3.up, transform.rotation);
        newTextPopup.GetComponent<TextPopup>().SetHealAmount((int)GameManager.playerInfo.healAmount);

        Destroy(gameObject);
    }
}
