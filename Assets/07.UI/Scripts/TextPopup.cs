using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopup : MonoBehaviour
{
    private Camera mainCamera;
    TextMeshPro textMeshPro;
    private int textValue;
    private Color color;

    public void SetDamage(int damage)
    {
        textValue = damage;
        color = Color.white;
    }
    public void SetHealAmount(int healAmount)
    {
        textValue = healAmount;
        color = Color.green;
    }
    public void SetOxygenAmount(int oxygenAmount)
    {
        textValue = oxygenAmount;
        color = Color.cyan;
    }

    void Start()
    {
        mainCamera = Camera.main;
        textMeshPro = GetComponentInChildren<TextMeshPro>();
        textMeshPro.text = textValue.ToString();
        textMeshPro.color = color;
    }

    public void Init()
    {
        Invoke("ReturnObject", 1.15f);
        textValue = 0;
    }

    void ReturnObject()
    {
        ObjectPool.Instance.ReturnTextObject(gameObject);
    }
}
