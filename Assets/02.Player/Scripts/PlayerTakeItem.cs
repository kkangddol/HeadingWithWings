using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeItem : MonoBehaviour
{
    public AudioClip coinAudio;
    AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Item item = other.GetComponent<Item>();
        if(item != null)
        {
            item.UseItem();
            audioSource.PlayOneShot(coinAudio);
        }
    }
}
