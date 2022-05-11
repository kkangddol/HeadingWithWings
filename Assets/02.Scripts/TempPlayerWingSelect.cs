using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerWingSelect : MonoBehaviour
{
    ShootingController shootingController;
    TempUziWing tempUziWing;

    private void Start()
    {
        shootingController = GetComponent<ShootingController>();
        tempUziWing = GetComponent<TempUziWing>();
    }

    public void OnClickRevolver()
    {
        shootingController.enabled = true;
        tempUziWing.enabled = false;
    }

    public void OnClickUzi()
    {
        shootingController.enabled = false;
        tempUziWing.enabled = true;
    }
}
