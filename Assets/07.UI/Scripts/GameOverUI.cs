using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    CanvasGroup canvasGroup;

    public Image playerImage = null;
    public TMP_Text survivalTimeTxt = null;
    public TMP_Text levelTxt = null;
    public TMP_Text killCountTxt = null;
    public TMP_Text totalDamageTxt = null;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(Fade());
    }

    public void SetData(Sprite playerSprite, string survivalTime, float height, int killCount, float totalDamage)
    {
        playerImage.sprite = playerSprite;
        survivalTimeTxt.text = survivalTime;
        levelTxt.text = height.ToString();
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
