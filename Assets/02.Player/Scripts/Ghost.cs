using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float ghostDelay;
    private float ghostDelaySeconds;
    public GameObject ghost;
    public bool makeGhost = false;

    private void Start()
    {
        ghostDelaySeconds = ghostDelay;
    }


    private void Update()
    {
        if(!makeGhost)  return;

        if(ghostDelaySeconds > 0)
        {
            ghostDelaySeconds -= Time.deltaTime;
            return;
        }

        SpriteRenderer currentGhost = Instantiate(ghost, transform.position, transform.rotation).GetComponent<SpriteRenderer>();
        SpriteRenderer currentSpriteSetting = GetComponent<SpriteRenderer>();
        currentGhost.sprite = currentSpriteSetting.sprite;
        currentGhost.flipX = currentSpriteSetting.flipX;
        ghostDelaySeconds = ghostDelay;
        Destroy(currentGhost.gameObject, 1f);
    }
}
