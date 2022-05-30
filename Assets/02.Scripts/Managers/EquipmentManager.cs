using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackEquipmentsNumber
{
    Feather
}

public enum AbilityEquipmentsNumber
{
}

public enum WingEquipmentsNumber
{
}

public class EquipmentManager : MonoBehaviour
{
    PlayerInfo playerInfo;

    public int[] attackEquipmentsLevel;
    public int[] abilityEquipmentsLevel;
    public int[] wingEquipmentsLevel;

    public GameObject[] attackEquipmentObjects;
    public GameObject[] abilityEquipmentObjects;
    public GameObject[] wingEquipmentObjects;
    public GameObject[] wingModels;

    public Sprite[] attackEquipmentSprites;
    public Sprite[] abilityEquipmentSprites;
    public Sprite[] wingEquipmentSprites;

    //220528 이 설명들도 DataManager로 관리하면 좋을듯
    public string[] attackEquipmentDescriptions;
    public string[] abilityEquipmentDescriptions;
    public string[] wingEquipmentDescriptions;

    public int attackEquipmentsCount;
    public int abilityEquipmentsCount;
    public int wingEquipmentsCount;


    private void Start() {
        Initialize();
    }

    void Initialize()
    {
        attackEquipmentsCount = System.Enum.GetValues(typeof(AttackEquipmentsNumber)).Length;
        abilityEquipmentsCount = System.Enum.GetValues(typeof(AbilityEquipmentsNumber)).Length;
        wingEquipmentsCount = System.Enum.GetValues(typeof(WingEquipmentsNumber)).Length;

        attackEquipmentsLevel = new int[attackEquipmentsCount];     
        abilityEquipmentsLevel = new int[abilityEquipmentsCount];        
        wingEquipmentsLevel = new int[wingEquipmentsCount];

        // attackEquipmentObjects = new GameObject[attackEquipmentsCount];
        // abilityEquipmentObjects = new GameObject[abilityEquipmentsCount];
        // wingEquipmentObjects = new GameObject[wingEquipmentsCount];
        // wingModels = new GameObject[wingEquipmentsCount];

        // attackEquipmentSprites = new Sprite[attackEquipmentsCount];     
        // abilityEquipmentSprites = new Sprite[abilityEquipmentsCount];        
        // wingEquipmentSprites = new Sprite[wingEquipmentsCount];

        // attackEquipmentDescriptions = new string[attackEquipmentsCount];
        // abilityEquipmentDescriptions = new string[abilityEquipmentsCount];
        // wingEquipmentDescriptions = new string[wingEquipmentsCount];

        playerInfo = GameManager.playerInfo;

        //임시
        TakeAttackEquipment((int)AttackEquipmentsNumber.Feather);
        //attackEquipmentDescriptions[(int)AttackEquipmentsNumber.Feather] = $"공격력의 100% 의 피해 \n 공격주기의 100% 의 주기";
        //임시끝
    }
    
    public void TakeAttackEquipment(int EquipmentNumber)
    {       
        if(attackEquipmentsLevel[EquipmentNumber] == 0)
        {
            //신규 장착
            attackEquipmentsLevel[EquipmentNumber] = 1;

            GameObject equipment = Instantiate(attackEquipmentObjects[EquipmentNumber], playerInfo.attackEquipmentsParent);
            playerInfo.attackEquipments[EquipmentNumber] = equipment;
            equipment.GetComponent<Equipment>().SetLevel(1);
            
        }
        else if(0 < attackEquipmentsLevel[EquipmentNumber] && attackEquipmentsLevel[EquipmentNumber] < 5)
        {
            //레벨업
            int newLevel = attackEquipmentsLevel[EquipmentNumber] + 1;
            attackEquipmentsLevel[EquipmentNumber] = newLevel;

            playerInfo.attackEquipments[EquipmentNumber].GetComponent<Equipment>().SetLevel(newLevel);
        }
        else
        {
            //레벨 5 이상 처리
            //아직 레벨 5 이상은 계획에 없음
        }
    }
    public void TakeAbilityItem(int EquipmentNumber)
    {
        abilityEquipmentsLevel[EquipmentNumber] += 1;
        playerInfo.abilityEquipments[EquipmentNumber] += 1;
        foreach(var change in abilityEquipmentObjects[EquipmentNumber].GetComponents<AbilityChange>())
        {
            change.ApplyChange();
        }
    }
    public void TakeWingItem(int EquipmentNumber)
    {
        if(playerInfo.wingNumber != EquipmentNumber)
        {
            //신규 장착
            Destroy(playerInfo.wingEquipmentParent.GetChild(0));
            Destroy(playerInfo.wingModelParent.GetChild(0));
            wingEquipmentsLevel[playerInfo.wingNumber] = 0;

            GameObject equipment = Instantiate(wingEquipmentObjects[EquipmentNumber], playerInfo.wingEquipmentParent);
            GameObject model = Instantiate(wingModels[EquipmentNumber], playerInfo.wingModelParent);

            equipment.GetComponent<Wing>().SetLevel(1);
            playerInfo.wingEquipment = equipment;

            wingEquipmentsLevel[EquipmentNumber] += 1;
            playerInfo.wingNumber = EquipmentNumber;

        }
        else if(0 < wingEquipmentsLevel[EquipmentNumber] && wingEquipmentsLevel[EquipmentNumber] < 5)
        {
            //레벨업
            int newLevel = wingEquipmentsLevel[EquipmentNumber] + 1;
            wingEquipmentsLevel[EquipmentNumber] = newLevel;

            playerInfo.wingEquipment.GetComponent<Wing>().SetLevel(newLevel);
        }
        else
        {
            //레벨 5 이상 처리
            //아직 레벨 5 이상은 계획에 없음
        }
    }
}
