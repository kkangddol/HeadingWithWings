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

    private List<int> pickedAttack = new List<int>();
    private List<int> pickedAbility = new List<int>();
    private List<int> pickedWing = new List<int>();

    // private void Awake() {
    //     Initialize();
    // }

    public void Initialize()
    {
        equipmentManager = GameManager.Instance.GetComponent<EquipmentManager>(); 
        pickUI = GameObject.Find("GameCanvas").transform.Find("PickEquipmentUI").gameObject;
        slots[0] = pickUI.transform.Find("ItemSlot1").gameObject;
        slots[1] = pickUI.transform.Find("ItemSlot2").gameObject;
        slots[2] = pickUI.transform.Find("ItemSlot3").gameObject;
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

    int preventionCount = 0;
    void PreventionInfiniteLoop(string str)
    {
            preventionCount++;
            if(preventionCount > 5000)
            {
                UnityEditor.EditorApplication.isPlaying = false;
                Debug.Log(str + " 에서 무한루프터짐");
            }
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
            float abilityAccumulateRate = attackDropRate + abilityDropRate;
            int randomEquipment = Random.Range(0,100);

            Button button = slots[i].GetComponent<Button>();

            button.onClick.RemoveAllListeners();

            if(0 <= randomEquipment && randomEquipment <= attackDropRate)
            {
                //공격장비
                int randomAttack;
                do
                {
                    PreventionInfiniteLoop("공격장비 생성");
                    randomAttack = Random.Range(0, equipmentManager.attackEquipmentsCount);
                }while(pickedAttack.Contains(randomAttack));

                pickedAttack.Add(randomAttack);

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
                int randomAbility;
                do
                {
                    PreventionInfiniteLoop("능력치장비 생성");
                    randomAbility = Random.Range(0, equipmentManager.abilityEquipmentsCount);
                }while(pickedAbility.Contains(randomAbility));
                
                pickedAbility.Add(randomAbility);

                if(equipmentManager.abilityEquipmentSprites.Length <= randomAbility)
                {
                    button.GetComponentsInChildren<Image>()[1].sprite = noImage;
                    button.GetComponentInChildren<TMPro.TMP_Text>().text = noDescription;
                }
                else
                {
                    button.GetComponentsInChildren<Image>()[1].sprite = equipmentManager.abilityEquipmentSprites[randomAbility];
                    button.GetComponentInChildren<TMPro.TMP_Text>().text = equipmentManager.abilityEquipmentDescriptions[randomAbility];
                }

                button.onClick.AddListener(delegate {equipmentManager.TakeAbilityItem(randomAbility);});
            }
            else
            {
                //날개장비

                //임시 시작
                if(equipmentManager.wingEquipmentsCount == 0)
                {
                    i--;
                    continue;
                }//임시끝
                
                int randomWing = Random.Range(0, equipmentManager.wingEquipmentsCount);
                do
                {
                    PreventionInfiniteLoop("날개장비 생성");
                    randomWing = Random.Range(0, equipmentManager.wingEquipmentsCount);
                }while(pickedWing.Contains(randomWing));
                
                pickedWing.Add(randomWing);
                

                if(equipmentManager.wingEquipmentSprites.Length <= randomWing)
                {
                    button.GetComponentsInChildren<Image>()[1].sprite = noImage;
                    button.GetComponentInChildren<TMPro.TMP_Text>().text = noDescription;
                }
                else
                {
                    button.GetComponentsInChildren<Image>()[1].sprite = equipmentManager.wingEquipmentSprites[randomWing];
                    button.GetComponentInChildren<TMPro.TMP_Text>().text = equipmentManager.wingEquipmentDescriptions[randomWing];
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
        //220530 대가지불 아직 구현하지 않았음.
    }

    public void Skip()
    {
        //높이만 올려주고 스킵
        GameManager.Instance.PlayerHeight += skipRewardHeight;
        EndPickSequence();
    }
}
