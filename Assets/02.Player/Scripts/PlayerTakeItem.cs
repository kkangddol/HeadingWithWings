using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeItem : MonoBehaviour
{
    public AudioClip[] audioClips;
    AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Item item = other.GetComponent<Item>();
        if(item != null)
        {
            int index = item.UseItem();
            switch(index)
            {
                case 0:
                    SoundManager.Instance.TryPlayOneShot(audioSource, audioClips[index], 1f);
                    break;
                case 1:
                    SoundManager.Instance.TryPlayOneShot(audioSource, audioClips[index], 0.5f);
                    break;
            }
            
        }
    }
}
