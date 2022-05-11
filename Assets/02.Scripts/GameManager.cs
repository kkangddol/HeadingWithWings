using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public HeightBar heightBar;
    
    private float time;

    void Awake()
    {
        instance = this;
    }
    
    private void Update()
    {
        time += Time.deltaTime;
        heightBar.SetHeight(time);
    }
}
