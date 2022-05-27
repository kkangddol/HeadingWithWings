using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private DataManager data = new DataManager();
    public static DataManager Data { get { return Instance.data; } }

    public Action<int> stageEvents;
    private bool eventCall = false;

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
        set
        {
            playerHeight = value;
            Debug.Log(playerHeight);
        }
        get { return playerHeight; } 
    }

    
    private float time;
    public float tempTime{
        get {return time;}
    }

    const string PLAYER = "PLAYER";
    public static PlayerInfo playerInfo;

    private void Awake()
    {
        playerInfo = GameObject.FindWithTag(PLAYER).GetComponent<PlayerInfo>();

        if(Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        isGameOver = false;

        // Data 불러오기
        instance.data.Init();
        // Debug.Log(instance.data.StageMonsterGenerateDict[3].bigwaveGenerateInfo.monsterGenerateInfo.id[3]);
    }
    
    private void Update()
    {
        time += Time.deltaTime;
        playerHeight += Time.deltaTime;

        heightBar.SetHeight(playerHeight);

        if(playerHeight > 5 && eventCall == false)
        {
            eventCall = true;
            stageEvents.Invoke(1);
        }
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