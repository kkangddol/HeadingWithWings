using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeItem : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if(item != null)
        {
            item.UseItem();
        }
    }
}
