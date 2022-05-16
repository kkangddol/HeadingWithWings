using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                var obj = FindObjectOfType<GameManager>();
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
    [SerializeField]
    private GameObject gameoverUI;
    [SerializeField]
    private HeightBar heightBar;
    public HeightBar HeightBar
    {
        get
        {
            return heightBar;
        }
        set
        {
            heightBar = value;
            //일단 임시로 게임 재시작하면 0으로 초기화 해줌. GameManager를 게임때만 사용해야할지 고민해봐야함
            time = 0;
        }
    }
    private bool isGameOver;

    public int enemyKillCount;
    private float playerHeight;
    public float PlayerHeight { get { return playerHeight; } }

    
    private float time;

    private void Awake()
    {
        if(Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        isGameOver = false;
    }
    
    private void Update()
    {
        time += Time.deltaTime;
        heightBar.SetHeight(time);
    }

    private static GameManager Create()
    {
        return Instantiate(Resources.Load<GameManager>("GameManager"));
    }

    public void OnGameOver()
    {
        isGameOver = true;
        PopUpUI();
        Time.timeScale = 0;
    }

    private void PopUpUI()
    {
        Instantiate(gameoverUI);
    }
}