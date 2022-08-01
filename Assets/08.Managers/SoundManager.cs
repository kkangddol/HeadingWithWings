using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if(instance == null)
            {
                var obj = FindObjectOfType<SoundManager>();
                if(obj != null)
                {
                    instance = obj;
                }
                else
                {
                    instance = Create();
                }
            }
            return instance;
        }
    }
    private Dictionary<AudioClip, float> soundOneShot = new Dictionary<AudioClip, float>();
    private Dictionary<AudioClip, List<float>> soundLimit = new Dictionary<AudioClip, List<float>>();
    private int MaxDuplicateClips = 30;

    private static SoundManager Create()
    {
        return Instantiate(Resources.Load<SoundManager>("Manager/SoundManager"));
    }

    private void Awake()
    {
        instance = this;
    }

    public void TryPlayOneShot(AudioSource source, AudioClip clip, float volume)
    {
        float nowTime = GameManager.Instance.PlayingTime;
        if(!soundOneShot.ContainsKey(clip))
        {
            soundOneShot[clip] = nowTime;
            soundLimit[clip] = new List<float>() { volume };
        }
        else
        {
            if(soundOneShot.TryGetValue(clip, out float playedTime)) return;

            int count = soundLimit[clip].Count;
            if(count == MaxDuplicateClips) return;

            soundOneShot[clip] = nowTime;
            soundLimit[clip].Add(volume);
        }
        source.PlayOneShot(clip, volume);
        StartCoroutine(RemoveVolumeFromClip(clip, nowTime));
        StartCoroutine(RemoveVolumeFromClip2(clip,volume));
    }

    private IEnumerator RemoveVolumeFromClip(AudioClip clip, float nowTime)
    {
        // 재생 시간동안기다리고 그후에 저장된 값을 지운다
        yield return new WaitForSeconds(clip.length * 0.005f);

        if (soundOneShot.TryGetValue(clip, out float babo))
        {
            soundOneShot.Remove(clip);
        }
    }
    private IEnumerator RemoveVolumeFromClip2(AudioClip clip, float volume)
    {
        // 재생 시간동안기다리고 그후에 저장된 값을 지운다
        yield return new WaitForSeconds(clip.length);

        List<float> volumes;
        if (soundLimit.TryGetValue(clip, out volumes))
        {
            volumes.Remove(volume);
        }
    }
}
