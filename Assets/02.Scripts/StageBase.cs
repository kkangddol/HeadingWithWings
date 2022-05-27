using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBase : MonoBehaviour
{
    protected enum CrisisType
    {
        Bigwave,
        Boss,
    }

    protected int stage = 0;
    protected AudioSource bgm;
    public AudioClip bgmClip;


    private void Awake()
    {
        bgm = GameObject.Find("TempBGM").GetComponent<AudioSource>();
    }


    protected void OnCrisisEvent(int crisisType)
    {
        bgm.clip = bgmClip;
        bgm.Stop();
        bgm.Play();

        switch (crisisType)
        {
            case (int)CrisisType.Boss:
                BossEvent();
                break;
            case (int)CrisisType.Bigwave:
                BigwaveEvent();
                break;
        }
    }

    protected virtual void BossEvent()
    {

    }
    protected virtual void BigwaveEvent()
    {

    }
}
