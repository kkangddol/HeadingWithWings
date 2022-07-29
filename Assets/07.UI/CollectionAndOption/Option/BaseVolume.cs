using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseVolume : MonoBehaviour
{
    protected AudioSource bgm;
    protected Slider volume;
    protected int amount = 1;

    protected void Init(string name)
    {
        volume = GetComponent<Slider>();
        GameObject go = GameObject.Find(name);
        if (go == null)
        {
            bgm = null;
            volume.interactable = false;
            return;
        }
        bgm = go.GetComponent<AudioSource>();

        volume.value = (int)(bgm.volume * volume.maxValue);
    }

    public void OnVolumeChanged()
    {
        if(bgm == null)
        {
            return;
        }

        bgm.volume = volume.value / volume.maxValue;
    }
    public void OnClickVolumeDown()
    {
        if (volume.value == volume.minValue || bgm == null)
        {
            return;
        }
        volume.value -= amount;
    }
    public void OnClickVolumeUp()
    {
        if (volume.value == volume.maxValue || bgm == null)
        {
            return;
        }
        volume.value += amount;
    }
}
