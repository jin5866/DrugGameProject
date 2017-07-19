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
    public string GameOverString = "앙 쥬금";

    public Text scoreText;

    public int scorePerSecond = 10;

    public int scorePerDrug = 30;

    [HideInInspector] public bool isPlaying = true;

    private PlayerState playerState;
    private UIManager uiManager;

    private float score;

	// Use this for initialization
	void Start () {
        playerState = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        isPlaying = true;
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
        if(playerState == null)
            playerState = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();

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
    }

}
