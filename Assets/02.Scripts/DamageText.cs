using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    private Canvas canvas;
    private Camera mainCamera;
    TextMeshPro textMeshPro;
    private int damageValue;
    public int damage
    {
        set { damageValue = value;}
    }

    void Start()
    {
        mainCamera = Camera.main;
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = mainCamera;
        transform.LookAt(transform.position + mainCamera.transform.forward);
        textMeshPro = GetComponentInChildren<TextMeshPro>();
        textMeshPro.text = damageValue.ToString();
        Destroy(gameObject, 1.15f);
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + mainCamera.transform.forward);
    }
}
