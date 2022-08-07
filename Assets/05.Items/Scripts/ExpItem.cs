using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItem : MonoBehaviour, Item
{
    public float riseAmount;
    public int UseItem()
    {
        GameManager.Instance.PlayerExp += riseAmount;
        Destroy(gameObject);
        return 1;
    }
}
