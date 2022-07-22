using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStackHandler : MonoBehaviour
{
    TMPro.TextMeshProUGUI stackText;
    public int _stackCount;

    private void Start() {
        stackText = GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1];
    }

    public void SetStackText(int stackCount)
    {
        _stackCount = stackCount;
        stackText.text = _stackCount.ToString();
    }

    public void ResetStack()
    {
        stackText.text = "";
    }
}
