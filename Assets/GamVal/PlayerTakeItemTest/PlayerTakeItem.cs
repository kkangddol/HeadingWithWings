using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeItem : MonoBehaviour
{
    PlayerInfo playerInfo;
    public HealthBar healthBar;
    public TextPopup TextPopup;
    [SerializeField]
    float healAmount;
    [SerializeField]
    float oxygenAmount;

    void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HEALITEM"))
        {
            TextPopup textPopup = Instantiate<TextPopup>(TextPopup, other.transform.position + Vector3.up, other.transform.rotation);
            textPopup.GetComponent<TextPopup>().SetHealAmount((int)healAmount);
            playerInfo.HealthPoint += healAmount;
            if (playerInfo.HealthPoint > playerInfo.MaxHealthPoint)
            {
                playerInfo.HealthPoint = playerInfo.MaxHealthPoint;
            }
            healthBar.SetHealth((int)playerInfo.HealthPoint);

            Destroy(other.gameObject);
        }
        else if (other.CompareTag("OXYGENITEM"))
        {
            TextPopup textPopup = Instantiate<TextPopup>(TextPopup, other.transform.position + Vector3.up, other.transform.rotation);
            textPopup.GetComponent<TextPopup>().SetOxygenAmount((int)oxygenAmount);
            playerInfo.oxygen += oxygenAmount;
            if (playerInfo.oxygen > playerInfo.maxOxygen)
            {
                playerInfo.oxygen = playerInfo.maxOxygen;
            }

            Destroy(other.gameObject);
        }
        else if (other.CompareTag("RANDOMITEM"))
        {
            Destroy(other.gameObject);
        }
    }
}
