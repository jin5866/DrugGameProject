using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * 게임 매니저
 * 게임이 끝났는지 안끝났는지 관리
 * 점수 관리
 * 
 * 2017-07-17 jin5866
 */ 

public class GameManager : MonoBehaviour {
    public static GameManager gameManager { get; private set; }
    public static UIManager uiManager { get; private set; }
    public static MapManager mapManager { get; private set; }


    public string GameOverString = "앙 쥬금";

    public Text scoreText;

    public int scorePerSecond = 10;

    public int scorePerDrug = 30;

    [HideInInspector] public bool isPlaying = true;

    private PlayerState playerState;


    private float score;

    private static Transform _player;

    public AudioSource BGM;
    public AudioClip normal; //일반 배경음
    public AudioClip america; //fever 배경음

    [HideInInspector] public bool isPaused { get; private set; }

    public static Transform player
    {
        private set
        {
            _player = value;
        }

        get
        {
            if (_player == null)
                _player = GameObject.FindGameObjectWithTag("Player").transform;

            return _player;
        }
    }

    // Use this for initialization
    void Start () {
        playerState = player.GetComponent<PlayerState>();
        isPlaying = true;
	}

    void Awake()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
        gameManager = this;
    }

    // Update is called once per frame
    void Update () {
        if (!isPlaying)
            return;

        if (GameOverCheck())
        {
            GameOver();
        }
        else
        {
            score += scorePerSecond * Time.deltaTime;
            scoreText.text = "" + (int)score;
        }
    }

    void GameOver()
    {
        Time.timeScale = 0;
        uiManager.Alarm(GameOverString,GameOverAction);
        isPlaying = false;
    }

    public void GameOverAction()
    {
        SceneManager.LoadScene("_main_menu");
    }

    bool GameOverCheck()
    {
        if (playerState == null)
            playerState = GameManager.player.GetComponent<PlayerState>();

        if (!playerState.isLife)
        {
            //죽었을때
            return true;
        }
        else
        {
            //살았을떄
            return false;
        }
    }

    public void GetDrug()
    {
        score += scorePerDrug;
    }

    public void FEVER(bool afterDead)
    {
        scorePerSecond *= 10;

        //BGM audioclip을 미국브금으로 맞춘후, 새로 틈
        //TODO : Fever가 끝날 때 원래 브금(normal)으로 다시 맞추어 줘야 함
        BGM.clip = america;
        BGM.Play();
    }

    public void Pause(bool set)
    {
        if(set)
        {
            //멈추기
            Time.timeScale = 0f;
            isPaused = true;
        }
        else
        {
            //다시 시작
            Time.timeScale = 1f;
            isPaused = false;
        }

        uiManager.SetControlActive(!set);
    }


}
