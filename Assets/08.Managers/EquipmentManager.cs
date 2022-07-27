using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{

    private static EquipmentManager instance;
    public static EquipmentManager Instance
    {
        get
        {
            if(instance == null)
            {
                var obj = FindObjectOfType<EquipmentManager>();
                if(obj != null)
                {
                    instance = obj;
                }
                else
                {
                    instance = Create();
                }
            }
            return instance;
        }
    }

    public int[] attackEquipmentsLevel;
    public GameObject[] attackEquipmentObjects;
    public Sprite[] attackEquipmentSprites;
    public string[] attackEquipmentDescriptions;

    public int[] abilityEquipmentsLevel;
    public GameObject[] abilityEquipmentObjects;
    public Sprite[] abilityEquipmentSprites;
    public string[] abilityEquipmentDescriptions;

    public int[] wingEquipmentsLevel;
    public GameObject[] wingEquipmentObjects;
    public Sprite[] wingEquipmentSprites;
    public string[] wingEquipmentDescriptions;

    public GameObject skillButton;


    private void Awake() {
        if(Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Initialize();
    }

    public void Initialize()
    {
        attackEquipmentsLevel = new int[attackEquipmentObjects.Length];     
        abilityEquipmentsLevel = new int[abilityEquipmentObjects.Length];        
        wingEquipmentsLevel = new int[wingEquipmentObjects.Length];

        skillButton = GameObject.FindWithTag("SKILLBUTTON");
        skillButton.SetActive(false);

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

        //임시
        //TakeAttackEquipment((int)AttackEquipmentsNumber.Feather);
        //attackEquipmentDescriptions[(int)AttackEquipmentsNumber.Feather] = $"공격력의 100% 의 피해 \n 공격주기의 100% 의 주기";
        //임시끝
    }

    private static EquipmentManager Create()
    {
        return Instantiate(Resources.Load<EquipmentManager>("Manager/EquipmentManager"));
    }
    
    public void TakeAttackEquipment(int EquipmentNumber)
    {       
        if(attackEquipmentsLevel[EquipmentNumber] == 0)
        {
            //신규 장착
            attackEquipmentsLevel[EquipmentNumber] = 1;

            GameObject equipment = Instantiate(attackEquipmentObjects[EquipmentNumber], GameManager.playerInfo.attackEquipmentsParent);
            GameManager.playerInfo.attackEquipments[EquipmentNumber] = equipment;

            equipment.GetComponent<Equipment>().SetLevel(1);
        }
        else if(0 < attackEquipmentsLevel[EquipmentNumber] && attackEquipmentsLevel[EquipmentNumber] < 5)
        {
            //레벨업
            int newLevel = attackEquipmentsLevel[EquipmentNumber] + 1;
            attackEquipmentsLevel[EquipmentNumber] = newLevel;

            GameManager.playerInfo.attackEquipments[EquipmentNumber].GetComponent<Equipment>().SetLevel(newLevel);
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
        GameManager.playerInfo.abilityEquipments[EquipmentNumber] += 1;
        foreach(var change in abilityEquipmentObjects[EquipmentNumber].GetComponents<AbilityChange>())
        {
            change.ApplyChange();
        }
    }
    public void TakeWingItem(int EquipmentNumber)
    {
        if(GameManager.playerInfo.wingNumber != EquipmentNumber)
        {
            Debug.Log("날개장착~");
            //신규 장착
            if(GameManager.playerInfo.wingEquipmentParent.childCount != 0)
            {
                Destroy(GameManager.playerInfo.wingEquipment);
                Destroy(GameManager.playerInfo.wingModel);
            }
            if(GameManager.playerInfo.wingNumber != -1)
            {
                wingEquipmentsLevel[GameManager.playerInfo.wingNumber] = 0;
            }

            //모델링 오면 추가로 달아줘야 함

            GameObject equipment = Instantiate(wingEquipmentObjects[EquipmentNumber], GameManager.playerInfo.wingEquipmentParent);

            equipment.GetComponent<Equipment>().SetLevel(1);
            GameManager.playerInfo.wingEquipment = equipment;

            wingEquipmentsLevel[EquipmentNumber] += 1;
            GameManager.playerInfo.wingNumber = EquipmentNumber;

            skillButton.GetComponent<SkillCoolTimeHandler>().ResetCool();
            skillButton.GetComponent<SkillStackHandler>().ResetStack();

            skillButton.SetActive(true);

            equipment.GetComponent<ActiveWing>().SetButton(skillButton);

            Image[] skillImages = skillButton.GetComponentsInChildren<Image>();
            foreach(var img in skillImages)
            {
                img.sprite = wingEquipmentSprites[EquipmentNumber];
            }
            skillButton.GetComponent<Button>().onClick.AddListener(delegate {GameManager.playerInfo.wingEquipment.GetComponent<ActiveWing>().ActivateSkill();});
        }
        else if(0 < wingEquipmentsLevel[EquipmentNumber] && wingEquipmentsLevel[EquipmentNumber] < 5)
        {
            //레벨업
            int newLevel = wingEquipmentsLevel[EquipmentNumber] + 1;
            wingEquipmentsLevel[EquipmentNumber] = newLevel;

            GameManager.playerInfo.wingEquipment.GetComponent<Equipment>().SetLevel(newLevel);
        }
        else
        {
            //레벨 5 이상 처리
            //아직 레벨 5 이상은 계획에 없음
        }
    }
}
