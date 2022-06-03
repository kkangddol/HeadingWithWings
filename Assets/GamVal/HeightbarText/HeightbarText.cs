using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeightbarText : MonoBehaviour
{
    private TMP_Text heightbarText;
    private void Start()
    {
        heightbarText = GetComponent<TMP_Text>();
    }
    private void LateUpdate()
    {
        float playerHeight = GameManager.Instance.PlayerHeight;
        heightbarText.text = $"{(int)playerHeight}m >";
    }
}
