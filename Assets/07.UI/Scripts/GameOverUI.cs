using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    CanvasGroup canvasGroup;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(Fade());
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
