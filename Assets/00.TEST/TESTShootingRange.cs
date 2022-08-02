using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTShootingRange : MonoBehaviour
{
    public GameObject Player;
    public GameObject _Whale;
    public GameObject _Pegasus;

    private void Awake() {
        GameManager.Instance.GameStartInit();
    }

    public void RESETALL()
    {
        foreach(var temp in Player.GetComponent<PlayerInfo>().attackEquipments)
        {
            Destroy(temp);
        }

        Destroy(GameManager.playerInfo.wingEquipment);

        Player.GetComponent<PlayerInfo>().attackEquipments = new GameObject[EquipmentManager.Instance.attackEquipmentObjects.Length];
        EquipmentManager.Instance.attackEquipmentsLevel = new int[EquipmentManager.Instance.attackEquipmentObjects.Length];
        EquipmentManager.Instance.wingEquipmentsLevel = new int[EquipmentManager.Instance.wingEquipmentObjects.Length];
    }

    public void Feather()
    {
        EquipmentManager.Instance.TakeAttackEquipment(0);
    }

    public void Shotgun()
    {
        EquipmentManager.Instance.TakeAttackEquipment(1);
    }

    public void Sniper()
    {
        EquipmentManager.Instance.TakeAttackEquipment(2);
    }

    public void Icicle()
    {
        EquipmentManager.Instance.TakeAttackEquipment(3);
    }

    public void Archon()
    {
        EquipmentManager.Instance.TakeAttackEquipment(4);
    }

    public void IronWall()
    {
        EquipmentManager.Instance.TakeAttackEquipment(5);
    }

    public void Satellite()
    {
        EquipmentManager.Instance.TakeAttackEquipment(6);
    }

    public void Meteor()
    {
        EquipmentManager.Instance.TakeAttackEquipment(7);
    }

    public void Military()
    {
        EquipmentManager.Instance.TakeWingItem(0);
    }

    public void Devil()
    {
        EquipmentManager.Instance.TakeWingItem(1);
    }

    public void StarryNight()
    {
        EquipmentManager.Instance.TakeWingItem(2);
    }

    public void DragonFly()
    {
        EquipmentManager.Instance.TakeWingItem(3);
    }

    public void Whale()
    {
        if(_Whale.activeSelf == true)
            _Whale.SetActive(false);
        else
            _Whale.SetActive(true);
    }

    public void Pegasus()
    {
        if(_Pegasus.activeSelf == true)
            _Pegasus.SetActive(false);
        else
            _Pegasus.SetActive(true);
    }
}
