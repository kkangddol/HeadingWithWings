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
    private SpriteState[] bgmSpriteStates = new SpriteState[2];
    private SpriteState[] sfxSpriteStates = new SpriteState[2];

    private void Start() {
        if(buttons == null)  return;
        for (int i = 0; i < buttons.Length; i++)
        {
            SetSpriteState(i, buttons[i].GetComponent<ButtonSprites>());
        }
    }

    private void SetSpriteState(int index, ButtonSprites buttonSprite)
    {
        if(index == 0)
        {
            for (int i = 0; i < 2; i++)
            {
                bgmSpriteStates[i].highlightedSprite = buttonSprite.unpressed[i];
                bgmSpriteStates[i].selectedSprite = buttonSprite.unpressed[i];
                bgmSpriteStates[i].pressedSprite = buttonSprite.pressed[i];
                bgmSpriteStates[i].disabledSprite = buttonSprite.disable[i];
            }

            return;
        }

        for (int i = 0; i < 2; i++)
        {
            sfxSpriteStates[i].highlightedSprite = buttonSprite.unpressed[i];
            sfxSpriteStates[i].selectedSprite = buttonSprite.unpressed[i];
            sfxSpriteStates[i].pressedSprite = buttonSprite.pressed[i];
            sfxSpriteStates[i].disabledSprite = buttonSprite.disable[i];
        }
    }

    public void OnClickBGM()
    {
        listeners[0].mute = !(listeners[0].mute);
        // Mute 기준으로 스프라이트 변경
        SwitchSprites(true, buttons[0], listeners[0].mute);
    }

    public void OnClickSFX()
    {
        listeners[1].mute = !(listeners[1].mute);
        // Mute 기준으로 스프라이트 변경
        SwitchSprites(false, buttons[1], listeners[1].mute);
    }

    private void SwitchSprites(bool bBGM, GameObject go, bool bMute)
    {
        int iMute = Convert.ToInt32(bMute);
        Button tempBtn = go.GetComponent<Button>();
        Image tempImg = go.GetComponent<Image>();

        if(bBGM)
        {
            tempImg.sprite = bgmSpriteStates[iMute].highlightedSprite;
            tempBtn.spriteState = bgmSpriteStates[iMute];
            return;
        }

        tempImg.sprite = sfxSpriteStates[iMute].highlightedSprite;
        tempBtn.spriteState = sfxSpriteStates[iMute];
    }
}
