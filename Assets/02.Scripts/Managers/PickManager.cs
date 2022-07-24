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

    public GameObject pickUI;

    public float attackDropRate;
    public float abilityDropRate;
    public float wingDropRate;

    public GameObject[] slots = new GameObject[3];

    public Sprite noImage;
    public string noDescription = "No Description.";

    private List<int> pickedAttack = new List<int>();
    private List<int> pickedAbility = new List<int>();
    private List<int> pickedWing = new List<int>();

    private static PickManager Create()
    {
        return Instantiate(Resources.Load<PickManager>("Manager\\PickManager"));
    }

     private void Awake() {
        Initialize();
     }

    public void Initialize()
    {
        //pickUI = GameObject.Find("GameCanvas").transform.Find("PickEquipmentUI").gameObject;
        pickUI = GameObject.FindWithTag("PICKUI");
        pickUI.SetActive(false);
        slots[0] = pickUI.transform.Find("ItemSlot1").gameObject;
        slots[1] = pickUI.transform.Find("ItemSlot2").gameObject;
        slots[2] = pickUI.transform.Find("ItemSlot3").gameObject;

        Button[] buttons = pickUI.GetComponentsInChildren<Button>();
        buttons[3].onClick.AddListener(delegate {ReRoll();});
        buttons[4].onClick.AddListener(delegate {Skip();});
    }

    public void StartPickSequence()
    {
        GameManager.Instance.PlayerHeight = 0;
        Time.timeScale = 0;
        pickUI.SetActive(true);
        SetSlot();
    }

    public void EndPickSequence()
    {
        pickUI.SetActive(false);
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
            Button button = slots[i].GetComponent<Button>();

            button.onClick.RemoveAllListeners();

            float[] probs = {attackDropRate, abilityDropRate, wingDropRate};
            int chosenEquip = Choose(probs);

            int randomEquip;

            switch(chosenEquip)
            {
                case 1:
                {
                    randomEquip = Random.Range(0,EquipmentManager.Instance.attackEquipmentObjects.Length);
                    
                    if(EquipmentManager.Instance.attackEquipmentSprites.Length <= randomEquip)
                    {
                        button.GetComponentsInChildren<Image>()[1].sprite = noImage;
                        button.GetComponentInChildren<TMPro.TMP_Text>().text = noDescription;
                    }
                    else
                    {
                        button.GetComponentsInChildren<Image>()[1].sprite = EquipmentManager.Instance.attackEquipmentSprites[randomEquip];
                        button.GetComponentInChildren<TMPro.TMP_Text>().text = EquipmentManager.Instance.attackEquipmentDescriptions[randomEquip];
                    }

                    button.onClick.AddListener(delegate {EquipmentManager.Instance.TakeAttackEquipment(randomEquip);});
                    break;
                }
                case 2:
                {
                    randomEquip = Random.Range(0,EquipmentManager.Instance.abilityEquipmentObjects.Length);

                    if(EquipmentManager.Instance.abilityEquipmentSprites.Length <= randomEquip)
                    {
                        button.GetComponentsInChildren<Image>()[1].sprite = noImage;
                        button.GetComponentInChildren<TMPro.TMP_Text>().text = noDescription;
                    }
                    else
                    {
                        button.GetComponentsInChildren<Image>()[1].sprite = EquipmentManager.Instance.abilityEquipmentSprites[randomEquip];
                        button.GetComponentInChildren<TMPro.TMP_Text>().text = EquipmentManager.Instance.abilityEquipmentDescriptions[randomEquip];
                    }

                    button.onClick.AddListener(delegate {EquipmentManager.Instance.TakeAbilityItem(randomEquip);});

                    break;
                }
                case 3:
                {
                    randomEquip = Random.Range(0,EquipmentManager.Instance.abilityEquipmentObjects.Length);

                    if(EquipmentManager.Instance.wingEquipmentSprites.Length <= randomEquip)
                    {
                        button.GetComponentsInChildren<Image>()[1].sprite = noImage;
                        button.GetComponentInChildren<TMPro.TMP_Text>().text = noDescription;
                    }
                    else
                    {
                        button.GetComponentsInChildren<Image>()[1].sprite = EquipmentManager.Instance.wingEquipmentSprites[randomEquip];
                        button.GetComponentInChildren<TMPro.TMP_Text>().text = EquipmentManager.Instance.wingEquipmentDescriptions[randomEquip];
                    }

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
        GameManager.playerInfo.HealthPoint = GameManager.playerInfo.HealthPoint * 0.8f;
        SetSlot();
    }

    public void Skip()
    {
        //높이만 올려주고 스킵
        float skipRewardHeight = GameManager.Instance.PickHeight * 0.3f;
        GameManager.Instance.PlayerHeight += skipRewardHeight;
        EndPickSequence();
    }
}
