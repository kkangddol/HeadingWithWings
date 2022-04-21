using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    TextMeshPro text;
    Color alpha;
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        alpha = text.color;
        text.text = damage.ToString();
        Destroy(gameObject, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
