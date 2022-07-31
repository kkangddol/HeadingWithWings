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
    private Dictionary<AudioClip, List<float>> soundOneShot = new Dictionary<AudioClip, List<float>>();
    private int MaxDuplicateClips = 20;

    private static SoundManager Create()
    {
        return Instantiate(Resources.Load<SoundManager>("Manager/SoundManager"));
    }

    private void Awake()
    {
        instance = this;
    }

    public void PlayWithCheck(AudioSource source, AudioClip clip, float volumeScale)
    {
        //해당 클립당 재생되고 잇는 사운드 수를 계산하기위해 아래와같이 처리한다
        // 재생수가 max 만큼이면 재생안한다
        if(!soundOneShot.ContainsKey(clip))
        {
            soundOneShot[clip] = new List<float>() { volumeScale };
        }
        else
        {
            int count = soundOneShot[clip].Count;
            //한클립당 현재 재생수가 30개 넘으면 리턴한다
            if (count == MaxDuplicateClips) return;
            soundOneShot[clip].Add(volumeScale);
        }
        int count1 = soundOneShot[clip].Count;
        Debug.Log(clip.name + " 재생갯수 : " + count1);
      
        source.PlayOneShot(clip, volumeScale);
        StartCoroutine(RemoveVolumeFromClip(clip, volumeScale));
    }

    private IEnumerator RemoveVolumeFromClip(AudioClip clip, float volume)
    {
        // 재생 시간동안기다리고 그후에 저장된 값을 지운다
        yield return new WaitForSeconds(clip.length);

        List<float> volumes;
        if (soundOneShot.TryGetValue(clip, out volumes))
        {
            volumes.Remove(volume);
        }
    }
}
