using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public HeightBar heightBar;

    private float time;
    private void Update()
    {
        time += Time.deltaTime;
        heightBar.SetHeight(time);
    }
}
