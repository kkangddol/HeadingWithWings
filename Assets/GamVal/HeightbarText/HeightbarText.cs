using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeightbarText : MonoBehaviour
{
    private TMP_Text heightbarText;
    private float playerHeight = 0.0f;
    private void Start()
    {
        heightbarText = GetComponent<TMP_Text>();
        playerHeight = GameManager.Instance.PlayerHeight;
    }
    private void Update()
    {

        heightbarText.text = $"{playerHeight}m >";
    }
}
