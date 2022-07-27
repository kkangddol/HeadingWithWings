using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainbowText : MonoBehaviour
{
    float duration = 5;
    float smoothness = 0.02f;
    int next = 0;

    TMPro.TextMeshProUGUI text;
    private void Start() {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        StartCoroutine(ChangeColor(next));
    }

    IEnumerator ChangeColor(int type)
    {
        float progress = 0;
        float increment = smoothness / duration;
        while(progress < 1)
        {
            switch(type)
            {
                case 0:
                    text.color = Color.Lerp(Color.red, Color.green, progress);
                    break;
                case 1:
                    text.color = Color.Lerp(Color.green, Color.blue, progress);
                    break;
                case 2:
                    text.color = Color.Lerp(Color.blue, Color.red, progress);
                    break;
                default:
                break;
            }

            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }
        next++;
        next %= 3;
        StartCoroutine(ChangeColor(next)); 
    }
}
