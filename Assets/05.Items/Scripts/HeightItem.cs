using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightItem : MonoBehaviour, Item
{
    public float riseAmount;
    public int UseItem()
    {
        GameManager.Instance.PlayerHeight += riseAmount;
        Destroy(gameObject);
        return 1;
    }
}
