using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private DataManager data = new DataManager();
    public static DataManager Data { get { return Instance.data; } }

    private int stage = 1;
    public Action<int> stageEvents;
    public bool eventCall = false;

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
    public float PlayerHeight { 
        set { playerHeight = value; }
        get { return playerHeight; } 
    }

    
    private float time;
    public float tempTime{
        get {return time;}
    }

    private void Awake()
    {
        if(Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        isGameOver = false;

        // Data 불러오기
        instance.data.Init();
        // Debug.Log(instance.data.WingDict[3].damagePerLevels[0]);
    }
    
    private void Update()
    {
        time += Time.deltaTime;
        playerHeight = time;

        heightBar.SetHeight(playerHeight);

        // if(playerHeight >= GameManager.Data.StageDict[stage].endHeight && eventCall == false)
        //if (playerHeight >= 5 && eventCall == false)
        //{
        //    eventCall = true;
        //    stageEvents.Invoke(0);
        //}
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