using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    public Camera gamecam;
    
    // Start is called before the first frame update
    void Start()
    {
        gamecam = Camera.main;
    }

    void LateUpdate()
	{
        // move camera
        if (gamecam)
        {
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, gamecam.transform.position.z);
            gamecam.transform.position = newPosition;
        }
	}
}
