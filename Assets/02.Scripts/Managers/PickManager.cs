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

    EquipmentManager equipmentManager;

    public GameObject pickUI;

    public float attackDropRate;
    public float abilityDropRate;
    public float wingDropRate;

    public float skipRewardHeight;

    public GameObject[] slots = new GameObject[3];

    public Sprite noImage;
    public string noDescription = "No Description.";

    private void Awake() {
        Initialize();
    }

    void Initialize()
    {
        equipmentManager = GameManager.Instance.GetComponent<EquipmentManager>(); 
    }

    public void StartPickSequence()
    {
        Time.timeScale = 0;
        pickUI.SetActive(true);
        SetSlot();
    }

    public void EndPickSequence()
    {
        pickUI.SetActive(false);
        Time.timeScale = 1;
    }


    public void SetSlot()
    {
        //slot 별로 어떤 항목이 나올지 확률적으로 선택 (ex 공격장비 확률 55%, 능력치장비 확률 35%, 날개장비 확률 10%)
        //선택된 장비중 랜덤하게 하나 선택
        for(int i = 0; i < 3; i++)
        {
            float abilityAccumulateRate = attackDropRate + abilityDropRate;
            int randomEquipment = Random.Range(0,100);

            Button button = slots[i].GetComponent<Button>();

            button.onClick.RemoveAllListeners();

            if(0 <= randomEquipment && randomEquipment <= attackDropRate)
            {
                //공격장비
                int randomAttack = Random.Range(0, equipmentManager.attackEquipmentsCount);

                if(equipmentManager.attackEquipmentSprites.Length <= randomAttack)
                {
                    button.GetComponentsInChildren<Image>()[1].sprite = noImage;
                    button.GetComponentInChildren<TMPro.TMP_Text>().text = noDescription;
                }
                else
                {
                    button.GetComponentsInChildren<Image>()[1].sprite = equipmentManager.attackEquipmentSprites[randomAttack];
                    button.GetComponentInChildren<TMPro.TMP_Text>().text = equipmentManager.attackEquipmentDescriptions[randomAttack];
                }

                button.onClick.AddListener(delegate {equipmentManager.TakeAttackEquipment(randomAttack);});
            }
            else if(attackDropRate < randomEquipment && randomEquipment <= abilityAccumulateRate)
            {
                //능력치장비
                int randomAbility = Random.Range(0, equipmentManager.abilityEquipmentsCount);

                if(equipmentManager.abilityEquipmentSprites.Length <= randomAbility)
                {
                    button.GetComponentsInChildren<Image>()[1].sprite = noImage;
                    button.GetComponentInChildren<TMPro.TMP_Text>().text = noDescription;
                }
                else
                {
                    button.GetComponentsInChildren<Image>()[1].sprite = equipmentManager.attackEquipmentSprites[randomAbility];
                    button.GetComponentInChildren<TMPro.TMP_Text>().text = equipmentManager.attackEquipmentDescriptions[randomAbility];
                }

                button.onClick.AddListener(delegate {equipmentManager.TakeAbilityItem(randomAbility);});
            }
            else
            {
                //날개장비
                int randomWing = Random.Range(0, equipmentManager.wingEquipmentsCount);

                if(equipmentManager.wingEquipmentSprites.Length <= randomWing)
                {
                    button.GetComponentsInChildren<Image>()[1].sprite = noImage;
                    button.GetComponentInChildren<TMPro.TMP_Text>().text = noDescription;
                }
                else
                {
                    button.GetComponentsInChildren<Image>()[1].sprite = equipmentManager.attackEquipmentSprites[randomWing];
                    button.GetComponentInChildren<TMPro.TMP_Text>().text = equipmentManager.attackEquipmentDescriptions[randomWing];
                }

                button.onClick.AddListener(delegate {equipmentManager.TakeWingItem(randomWing);});
            }

            button.onClick.AddListener(EndPickSequence);
        }
    }

    public void ReRoll()
    {
        //대가 지불하고 (대가ex 랜덤한 장비 삭제, 체력감소, ReRoll아이템이 있어야함 등등)
        //SetSlot 다시.
        SetSlot();
        //2205030 대가지불 아직 구현하지 않았음.
    }

    public void Skip()
    {
        //높이만 올려주고 스킵
        GameManager.Instance.PlayerHeight += skipRewardHeight;
        EndPickSequence();
    }
}
