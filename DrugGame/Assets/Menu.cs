using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public GameObject menuList;

    private Button button;

    private bool isActive;

    //의미 없는 버튼 누른 횟수
    private int nmCounter;

	// Use this for initialization
	void Start () {
        button = GetComponent<Button>();

        menuList.active = false;

        nmCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMenuClick()
    {
        
        SetMenu(true);
    }

    public void OnResumeClick()
    {
        Resume();
    }

    public void OnMainMenuClick()
    {
        SceneManager.LoadScene("_main_menu");
    }

    public void OnRestartClick()
    {
        //바로 재시작시 발생하는 오류떄문에 버퍼를 만듬.
        //만들었는데도 안됨.
        SceneManager.LoadScene("_main_game");
    }

    public void OnNoMeanClick()
    {
        nmCounter++;

        if(nmCounter==10)
        {
            UIManager.uiManager.Alarm("그만좀 눌러");
        }
        else if(nmCounter == 20)
        {
            UIManager.uiManager.Alarm("그만좀 누르라고");
        }
        else if(nmCounter == 30)
        {
            Application.Quit();
        }
        
    }
    //메뉴버튼 활성화
    private void SetMenuButtonActive(bool active)
    {
        button.interactable = active;
    }
    //메뉴들 껏다 켰다.
    private void SetMenu(bool set)
    {
        //켯다 껐다
        SetMenuButtonActive(!set);
        GameManager.gameManager.Pause(set);
        menuList.active = set;
    }
    //다시시작.
    private void Resume()
    {
        GameManager.gameManager.Pause(false);
        SetMenu(false);
    }
}
