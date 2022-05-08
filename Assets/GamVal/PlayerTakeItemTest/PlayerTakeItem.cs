using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeItem : MonoBehaviour
{
    PlayerInfo playerInfo;
    public HealthBar healthBar;
    public TextPopup TextPopup;
    [SerializeField]
    float healAmount = 300.0f;
    [SerializeField]
    float oxygenAmount = 300.0f;

    void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HEALITEM"))
        {
            TextPopup textPopup = Instantiate<TextPopup>(TextPopup, other.transform.position, other.transform.rotation);
            textPopup.GetComponent<TextPopup>().SetHealAmount((int)healAmount);
            playerInfo.hp += healAmount;
            if (playerInfo.hp > 1000.0f)
            {
                playerInfo.hp = 1000.0f;
            }
            healthBar.SetHealth((int)playerInfo.hp);

            Destroy(other.gameObject);
        }
        else if (other.CompareTag("OXYGENITEM"))
        {
            TextPopup textPopup = Instantiate<TextPopup>(TextPopup, other.transform.position, other.transform.rotation);
            textPopup.GetComponent<TextPopup>().SetOxygenAmount((int)oxygenAmount);
            playerInfo.oxygen += oxygenAmount;
            if (playerInfo.oxygen > 1000.0f)
            {
                playerInfo.oxygen = 1000.0f;
            }

            Destroy(other.gameObject);
        }
        else if (other.CompareTag("RANDOMITEM"))
        {
            Destroy(other.gameObject);
        }
    }
}
