using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButtonManager : MonoBehaviour
{
    // index 0 is BGM, index 1 is SFX
    public AudioSource[] listeners = null;
    public GameObject[] buttons = null;

    private Color[] colors = new Color[2];

    private void Start()
    {
        colors[0] = Color.white;
        colors[1] = new Color(0.5f, 0.5f, 0.5f, 1f);
    }

    public void OnClickBGM()
    {
        listeners[0].mute = !(listeners[0].mute);
        // Mute 기준으로 스프라이트 변경
        SwitchSprites(buttons[0], listeners[0].mute);
    }

    public void OnClickSFX()
    {
        listeners[1].mute = !(listeners[1].mute);
        // Mute 기준으로 스프라이트 변경
        SwitchSprites(buttons[1], listeners[1].mute);
    }

    private void SwitchSprites(GameObject go, bool bMute)
    {
        int iMute = Convert.ToInt32(bMute);

        go.GetComponent<Image>().color = colors[iMute];
    }
}
