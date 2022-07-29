using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickManager : MonoBehaviour
{
    //랜덤하게 항목 생성
    //항목 배치
    //선택한 항목 EquipmentManager에 전달하여 장착
    //ReRoll 기능
    //Skip 기능

    const int ATTACKID = 10000;
    const int WINGID = 20000;
    const int ABILITYID = 30000;

    private static PickManager instance;
    public static PickManager Instance
    {
        get
        {
            if(instance == null)
            {
                var obj = FindObjectOfType<PickManager>();
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

    public PickUIController pickUI;
    public GameObject levelUpEffect;

    public float attackDropRate;
    public float abilityDropRate;
    public float wingDropRate;

    public Sprite noImage;
    public string noDescription = "No Description.";
    public Sprite[] icons;

    private List<int> pickedAttack = new List<int>();
    private List<int> pickedAbility = new List<int>();
    private List<int> pickedWing = new List<int>();

    private static PickManager Create()
    {
        return Instantiate(Resources.Load<PickManager>("Manager/PickManager"));
    }

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
        //pickUI = GameObject.Find("GameCanvas").transform.Find("PickEquipmentUI").gameObject;
        pickUI = GameObject.FindWithTag("PICKUI").GetComponent<PickUIController>();
        pickUI.gameObject.SetActive(false);

        pickUI.rerollButton.onClick.AddListener(delegate {ReRoll();});
        pickUI.skipButton.onClick.AddListener(delegate {Skip();});
    }

    public void StartPickSequence()
    {
        pickUI.gameObject.SetActive(true);
        pickUI.Init();
        GameManager.Instance.PlayerHeight = 0;
        Time.timeScale = 0;
        SetSlot();
        if(GameManager.playerInfo.HealthPoint <= GameManager.playerInfo.MaxHealthPoint * 0.2f)
        {
            pickUI.BanReroll();
        }
    }

    public void EndPickSequence()
    {
        pickUI.PickUIExit();
        levelUpEffect.SetActive(false);
        Time.timeScale = 1;
    }

    int Choose (float[] probs) {

        float total = 0;

        foreach (float elem in probs) {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i= 0; i < probs.Length; i++) {
            if (randomPoint < probs[i]) {
                return i;
            }
            else {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }

    public void SetSlot()
    {
        //slot 별로 어떤 항목이 나올지 확률적으로 선택 (ex 공격장비 확률 55%, 능력치장비 확률 35%, 날개장비 확률 10%)
        //선택된 장비중 랜덤하게 하나 선택

        pickedAttack.Clear();
        pickedAbility.Clear();
        pickedWing.Clear();

        for(int i = 0; i < 3; i++)
        {
            Button button = pickUI.slotButtons[i];

            button.onClick.RemoveAllListeners();

            float[] probs = {attackDropRate, abilityDropRate, wingDropRate};
            int chosenEquip = Choose(probs);

            int randomEquip;

            switch(chosenEquip)
            {
                case 0:
                {
                    //Attack
                    randomEquip = Random.Range(0,EquipmentManager.Instance.attackEquipmentObjects.Length);
                    if(EquipmentManager.Instance.attackEquipmentsLevel[randomEquip] >= 5)
                    {
                        pickUI.SetSlotBan(i);
                        break;
                    }
                    int equipID = ATTACKID + ((randomEquip + 1) * 100) + EquipmentManager.Instance.attackEquipmentsLevel[randomEquip] + 1;
                    var equipData = GameManager.Data.EquipDescriptionDict[equipID];
                    
                    pickUI.slotImages[i].sprite = EquipmentManager.Instance.attackEquipmentSprites[randomEquip];
                    pickUI.slotLevel[i].text = "Lv." + equipData.level.ToString();
                    pickUI.slotName[i].text = equipData.equipName;

                    GameObject[] slotInfos = pickUI.slotInfos[i];
                    for(int j = 0; j < equipData.infoList.Count; j++)
                    {
                        GameObject infoSlot = slotInfos[j];
                        infoSlot.GetComponentsInChildren<Image>()[1].sprite = icons[ToInt(equipData.infoTitle[j])];
                        infoSlot.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = equipData.infoList[j];
                        slotInfos[j].SetActive(true);
                    }

                    string description = equipData.description + "\n" + equipData.extraDescription;
                    pickUI.infoButtons[i].onClick.AddListener(delegate{pickUI.SetInfoUIText(description);});

                    button.onClick.AddListener(delegate {EquipmentManager.Instance.TakeAttackEquipment(randomEquip);});
                    break;
                }
                case 1:
                {
                    //Ability
                    randomEquip = Random.Range(0,EquipmentManager.Instance.abilityEquipmentObjects.Length);
                    if(EquipmentManager.Instance.abilityEquipmentsLevel[randomEquip] >= 5)
                    {
                        pickUI.SetSlotBan(i);
                        break;
                    }
                    int equipID = ABILITYID + ((randomEquip + 1) * 100) + 1;
                    var equipData = GameManager.Data.EquipDescriptionDict[equipID];

                    pickUI.slotImages[i].sprite = EquipmentManager.Instance.abilityEquipmentSprites[randomEquip];
                    pickUI.slotLevel[i].text = "Lv." + equipData.level.ToString();
                    pickUI.slotName[i].text = equipData.equipName;

                    GameObject[] slotInfos = pickUI.slotInfos[i];
                    for(int j = 0; j < equipData.infoList.Count; j++)
                    {
                        GameObject infoSlot = slotInfos[j];
                        infoSlot.GetComponentsInChildren<Image>()[1].sprite = icons[ToInt(equipData.infoTitle[j])];
                        infoSlot.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = equipData.infoList[j];
                        slotInfos[j].SetActive(true);
                    }

                    string description = equipData.description + "\n" + equipData.extraDescription;
                    pickUI.infoButtons[i].onClick.AddListener(delegate{pickUI.SetInfoUIText(description);});

                    button.onClick.AddListener(delegate {EquipmentManager.Instance.TakeAbilityItem(randomEquip);});
                    break;
                }
                case 2:
                {
                    //Wing
                    randomEquip = Random.Range(0,EquipmentManager.Instance.wingEquipmentObjects.Length);
                    if(EquipmentManager.Instance.wingEquipmentsLevel[randomEquip] >= 5)
                    {
                        pickUI.SetSlotBan(i);
                        break;
                    }
                    int equipID = WINGID + ((randomEquip + 1) * 100) + EquipmentManager.Instance.wingEquipmentsLevel[randomEquip] + 1;
                    var equipData = GameManager.Data.EquipDescriptionDict[equipID];

                    pickUI.slotImages[i].sprite = EquipmentManager.Instance.wingEquipmentSprites[randomEquip];
                    pickUI.slotLevel[i].text = "Lv." + equipData.level.ToString();
                    pickUI.slotName[i].text = equipData.equipName;

                    GameObject[] slotInfos = pickUI.slotInfos[i];
                    for(int j = 0; j < equipData.infoList.Count; j++)
                    {
                        GameObject infoSlot = slotInfos[j];
                        infoSlot.GetComponentsInChildren<Image>()[1].sprite = icons[ToInt(equipData.infoTitle[j])];
                        infoSlot.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = equipData.infoList[j];
                        slotInfos[j].SetActive(true);
                    }

                    string description = equipData.description + "\n" + equipData.extraDescription;
                    pickUI.infoButtons[i].onClick.AddListener(delegate{pickUI.SetInfoUIText(description);});

                    button.onClick.AddListener(delegate {EquipmentManager.Instance.TakeWingItem(randomEquip);});
                    break;
                }
                default:
                {
                    button.onClick.AddListener(EndPickSequence);
                    break;
                }
            }
            button.onClick.AddListener(EndPickSequence);
        }
    }

    public void ReRoll()
    {
        //대가 지불하고 (대가ex 랜덤한 장비 삭제, 체력감소, ReRoll아이템이 있어야함 등등)
        //SetSlot 다시.
        if(GameManager.playerInfo.HealthPoint <= GameManager.playerInfo.MaxHealthPoint * 0.2f)
        {
            pickUI.BanReroll();
            return;
        }
        GameManager.playerInfo.HealthPoint -= GameManager.playerInfo.MaxHealthPoint * 0.2f;
        //GameManager.playerInfo.gameObject
        SetSlot();
    }

    public void Skip()
    {
        //높이만 올려주고 스킵
        float skipRewardHeight = GameManager.Instance.PickHeight * 0.3f;
        GameManager.Instance.PlayerHeight += skipRewardHeight;
        EndPickSequence();
    }

    int ToInt(string str)
    {
        switch(str)
        {
            case "damage"   :   return 0;
            case "delay"    :   return 1;
            case "range"    :   return 2;
            case "speed"    :   return 3;
            case "pelletCount"  :   return 4;
            case "headshotDamage"   :   return 5;
            case "headshotChance"   :   return 6;
            case "slow" :   return 7;
            case "slowTime" :   return 8;
            case "healAmount"   :   return 9;
            case "skillTime"    :   return 10;
            default: return 0;
        }
    } 
}
