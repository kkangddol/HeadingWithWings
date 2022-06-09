using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
        }
    }
    public bool isGameOver;


    TMPro.TextMeshProUGUI killCountText;
    private int killCount = 0;
    public int KillCount
    {
        get {return killCount;}
        set
        {
            killCount = value;
            killCountText.text = killCount.ToString();
        }
    }
    private float playerHeight;
    public float PlayerHeight { 
        set
        {
            playerHeight = value;
            heightBar.SetHeight(playerHeight);
        }
        get { return playerHeight; } 
    }

    
    [SerializeField]
    private float playingTime;
    public float PlayingTime{
        get {return playingTime;}
    }
    [SerializeField]
    TMPro.TextMeshProUGUI playingTimeText;


    const string PLAYER = "PLAYER";
    const string TIMETEXT = "TIMETEXT";
    const string KILLCOUNT = "KILLCOUNT";
    const string HEIGHTBAR = "HEIGHTBAR";
    const string PICKUI = "PICKUI";
    public static PlayerInfo playerInfo;
    PickManager pickManager;

    public float heightItemDropRate;
    public float healItemDropRate;

    private void Awake()
    {
        //GameStartInit();
        pickManager = GetComponent<PickManager>();

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

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "MainGameScene")
        {
            GameStartInit();
            Debug.Log("게임시작");
        }
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void GameStartInit()
    {
        isGameOver = false;
        playingTime = 0;
        killCount = 0;
        playerHeight = 0;
        heightItemDropRate = 90;
        healItemDropRate = 5;
        playerInfo = GameObject.FindWithTag(PLAYER).GetComponent<PlayerInfo>();
        heightBar = GameObject.FindWithTag(HEIGHTBAR).GetComponent<HeightBar>();
        playingTimeText = GameObject.FindWithTag(TIMETEXT).GetComponent<TMPro.TextMeshProUGUI>();
        killCountText = GameObject.FindWithTag(KILLCOUNT).GetComponent<TMPro.TextMeshProUGUI>();
        GetComponent<PickManager>().Initialize();
        GetComponent<EquipmentManager>().Initialize();
        StartCoroutine(TimeFlow());
    }

    public int tempPickHeight = 10;
    private void Update()
    {

        if(playerHeight >= tempPickHeight)
        {
            PickStart();
        }


        // if(playerHeight > 5 && eventCall == false)
        // {
        //     eventCall = true;
        //     stageEvents.Invoke(1);
        // }
    }

    IEnumerator TimeFlow()
    {
        int playingTimeMinute = 0;
        while(!isGameOver)
        {
            yield return null;
            playingTime += Time.deltaTime;
            playingTimeText.text = string.Format("{0:D2}:{1:D2}", playingTimeMinute, (int)playingTime);

            if((int)playingTime > 59)
            {
                playingTime = 0;
                playingTimeMinute++;
            }
        }
    }

    private static GameManager Create()
    {
        return Instantiate(Resources.Load<GameManager>("GameManager"));
    }

    public void OnGameOver()
    {
        StopCoroutine(TimeFlow());
        isGameOver = true;
        PopUpUI();
        Time.timeScale = 0;
    }

    private void PopUpUI()
    {
        Instantiate(gameoverUI);
    }

    public void PickStart()
    {
        //tempPickHeight += tempPickHeight + 2;
        pickManager.StartPickSequence();
    }
}