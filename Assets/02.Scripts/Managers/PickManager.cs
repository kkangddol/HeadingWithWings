using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickManager : MonoBehaviour
{
    //랜덤하게 항목 생성
    //항목 배치
    //선택한 항목 EquipmentManager에 전달하여 장착
    //ReRoll 기능
    //Skip 기능

    int attackEquipmentsCount;
    int abilityEquipmentsCount;
    int wingEquipmentsCount;

    public GameObject[] slots = new GameObject[3];

    private void Start() {
        Initialize();
    }

    void Initialize()
    {
        attackEquipmentsCount = System.Enum.GetValues(typeof(AttackEquipmentsNumber)).Length;
        abilityEquipmentsCount = System.Enum.GetValues(typeof(AbilityEquipmentsNumber)).Length;
        wingEquipmentsCount = System.Enum.GetValues(typeof(WingEquipmentsNumber)).Length;
    }

    public void SetSlot()
    {
        //slot 별로 어떤 항목이 나올지 확률적으로 선택 (ex 공격장비 확률 55%, 능력치장비 확률 35%, 날개장비 확률 10%)
        //선택된 장비중 랜덤하게 하나 선택

    }

    public void ReRoll()
    {
        //대가 지불하고
        //SetSlot 다시.
    }

    public void Skip()
    {
        //높이만 올려주고 스킵
    }
}
