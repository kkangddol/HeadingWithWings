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

    private static DataManager data;// = new DataManager();
    public static DataManager Data { get { return data; } }

    public StageManager stageManager;


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
    private int playingTimeMinute;
    public int PlayingTimeMinute
    {
        get{return playingTimeMinute;}
        set
        {
            playingTimeMinute = value;
            stageManager.SetWave(value);
        }
    }
    [SerializeField]
    TMPro.TextMeshProUGUI playingTimeText;


    const string PLAYER = "PLAYER";
    const string TIMETEXT = "TIMETEXT";
    const string KILLCOUNT = "KILLCOUNT";
    const string HEIGHTBAR = "HEIGHTBAR";
    const string PICKUI = "PICKUI";
    public static PlayerInfo playerInfo;
    public static Transform playerTransform;
    public static Rigidbody2D playerRigidbody;

    public float heightSilverDropRate;
    public float heightGoldDropRate;
    public float healItemDropRate;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        //GameStartInit();

        if(Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        isGameOver = false;

        data = new DataManager();
        // Data 불러오기
        data.Init();
        // Debug.Log(data.MonsterDict[100].monsterName);
        // Debug.Log(data.AttackEquipDict[10101].equipName);
        // Debug.Log(data.WingEquipDict[20101].equipName);
        // Debug.Log(data.EquipDescriptionDict[10101].description);
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
        heightSilverDropRate = 70;
        heightGoldDropRate = 20;
        healItemDropRate = 10;
        playerInfo = GameObject.FindWithTag(PLAYER).GetComponent<PlayerInfo>();
        playerTransform = GameObject.FindWithTag(PLAYER).GetComponent<Transform>();
        playerRigidbody = GameObject.FindWithTag(PLAYER).GetComponent<Rigidbody2D>();
        heightBar = GameObject.FindWithTag(HEIGHTBAR).GetComponent<HeightBar>();
        heightBar.SetMaxValue(PickHeight);
        playingTimeText = GameObject.FindWithTag(TIMETEXT).GetComponent<TMPro.TextMeshProUGUI>();
        killCountText = GameObject.FindWithTag(KILLCOUNT).GetComponent<TMPro.TextMeshProUGUI>();
        StartCoroutine(TimeFlow());
    }

    int pickHeight = 2;
    public int PickHeight
    {
        get{return pickHeight;}
        set
        {
            pickHeight = value;
            heightBar.SetMaxValue(value);
        }
    } 
    private void Update()
    {

        if(playerHeight >= pickHeight)
        {
            PickStart();
            PickHeight++;
        }


        // if(playerHeight > 5 && eventCall == false)
        // {
        //     eventCall = true;
        //     stageEvents.Invoke(1);
        // }
    }

    IEnumerator TimeFlow()
    {
        while(!isGameOver)
        {
            yield return null;
            playingTime += Time.deltaTime;
            playingTimeText.text = string.Format("{0:D2}:{1:D2}", PlayingTimeMinute, (int)playingTime);

            if((int)playingTime > 59)
            {
                playingTime = 0;
                PlayingTimeMinute++;
            }
        }
    }

    private static GameManager Create()
    {
        return Instantiate(Resources.Load<GameManager>("Manager/GameManager"));
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
        PickManager.Instance.StartPickSequence();
    }

    #region 임시
    public void ForDevelopTime()
    {
        Time.timeScale = 10;
        Invoke("TEST",10);
    }
    
    void TEST()
    {
        Time.timeScale = 1;
    }
    #endregion
}