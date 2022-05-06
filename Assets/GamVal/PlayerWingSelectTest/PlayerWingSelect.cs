using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWingSelect : MonoBehaviour
{
    public struct WingSlots
    {
        public WingTypes wingType;
        public Image wingImage;
    }
    public WingSlots[] wingSlots = new WingSlots[3];
    public Image[] wingImages;
    public enum WingTypes
    {
        Normal,
        Shotgun,
        Summons,
    }

    void Start()
    {
        wingImages = this.gameObject.GetComponentsInChildren<Image>();
        for (int i = 0; i < 3; i++)
        {
            wingSlots[i].wingImage = wingImages[i + 1];
            wingSlots[i].wingType = WingTypes.Normal;
            int index = i;
            wingImages[i + 1].gameObject.GetComponent<Button>().onClick.AddListener(() => OnWingSelect(index));
        }
    }
    
    void OnWingSelect(int slotNum)
    {
        Debug.Log($"It's {wingSlots[slotNum].wingType.ToString()} Type Wing!");
    }
}
