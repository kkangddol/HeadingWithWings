using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopup : MonoBehaviour
{
    TextMeshPro textMeshPro;

    void Awake()
    {
        textMeshPro = GetComponentInChildren<TextMeshPro>();
    }

    public void SetDamage(int damage)
    {
        textMeshPro.text = damage.ToString();
        textMeshPro.color = Color.white;
    }
    public void SetHealAmount(int healAmount)
    {
        textMeshPro.text = healAmount.ToString();
        textMeshPro.color = Color.green;
    }
    public void SetOxygenAmount(int oxygenAmount)
    {
        textMeshPro.text = oxygenAmount.ToString();
        textMeshPro.color = Color.cyan;
    }

    public void Init()
    {
        Invoke("ReturnObject", 1.15f);
    }

    void ReturnObject()
    {
        ObjectPool.Instance.ReturnTextObject(gameObject);
    }
}
