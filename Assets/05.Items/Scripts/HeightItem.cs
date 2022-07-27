using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightItem : MonoBehaviour, Item
{
    public float riseAmount;
    public void UseItem()
    {
        GameManager.Instance.PlayerHeight += riseAmount;
        Destroy(gameObject);
    }
}
