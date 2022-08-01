using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    CanvasGroup canvasGroup;

    public TMP_Text survivalTimeTxt = null;
    public TMP_Text heightTxt = null;
    public TMP_Text killCountTxt = null;
    public TMP_Text totalDamageTxt = null;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(Fade());
    }

    public void SetData(string survivalTime, float height, int killCount, float totalDamage)
    {
        survivalTimeTxt.text = survivalTime;
        heightTxt.text = height + " 미터";
        killCountTxt.text = killCount + " 마리";
        totalDamageTxt.text = totalDamage + " 데미지";
    }

    private IEnumerator Fade()
    {
        GetComponent<AudioSource>().Play();
        float timer = 0f;
        while(timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timer);
        }
    }
}
