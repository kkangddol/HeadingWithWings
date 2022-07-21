using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemMagnet : MonoBehaviour
{
    PlayerInfo playerInfo;

    public float magnetStrength = 5f;

    private void Start() {
        playerInfo = GetComponent<PlayerInfo>();
    }

    private void FixedUpdate() {
        Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, playerInfo.itemTakeRange);
        foreach(var item in items)
        {
            if(item.CompareTag("ITEM"))
            {
                Vector2 direction = transform.position - item.transform.position;
                float distance = Vector2.Distance(transform.position, item.transform.position);
                float speed = (playerInfo.itemTakeRange / distance) * magnetStrength;
                item.GetComponent<Rigidbody2D>().AddForce(direction * speed);
            }
        }
    }
}
