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

    public void  SetDamage(int damage)
    {
        textValue = damage;
        color = Color.white;
    }
    public void  SetHealAmount(int healAmount)
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
        transform.LookAt(transform.position + mainCamera.transform.forward);
        textMeshPro = GetComponentInChildren<TextMeshPro>();
        textMeshPro.text = textValue.ToString();
        textMeshPro.color = color;
        Destroy(gameObject, 1.15f);
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + mainCamera.transform.forward);
    }
}
