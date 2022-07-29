using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUIController : MonoBehaviour
{
    public Button[] slotButtons;
    public Image[] slotImages;
    public TMPro.TextMeshProUGUI[] slotLevel;
    public TMPro.TextMeshProUGUI[] slotName;
    public GameObject[] slotBan;
    [SerializeField] private GameObject[] firstSlotInfos;
    [SerializeField] private GameObject[] secondSlotInfos;
    [SerializeField] private GameObject[] thirdSlotInfos;
    public List<GameObject[]> slotInfos = new List<GameObject[]>();
    public Button[] infoButtons;
    public Button rerollButton;
    public GameObject rerollBan;
    public Button skipButton;
    public GameObject infoUI;

    public void Init(){
        slotInfos.Add(firstSlotInfos);
        slotInfos.Add(secondSlotInfos);
        slotInfos.Add(thirdSlotInfos);
        GetComponent<AudioSource>().Play();
    }

    public void InfoUIOpen()
    {
        infoUI.SetActive(true);
    }

    public void InfoUIExit()
    {
        infoUI.SetActive(false);
    }

    public void SetInfoUIText(string str)
    {
        infoUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = str;
    }

    public void BanReroll()
    {
        rerollButton.gameObject.SetActive(false);
        rerollBan.SetActive(true);
    }
    public void SetSlotBan(int index)
    {
        slotBan[index].SetActive(true);
    }
    
    public void PickUIExit()
    {
        int count = firstSlotInfos.Length;
        for(int i = 0; i < count; i++)
        {
            firstSlotInfos[i].SetActive(false);
            secondSlotInfos[i].SetActive(false);
            thirdSlotInfos[i].SetActive(false);
        }
        for(int i = 0; i < 3; i++)
        {
            slotBan[i].SetActive(false);
        }
        rerollButton.gameObject.SetActive(true);
        rerollBan.SetActive(false);
        gameObject.SetActive(false);
    }
}
