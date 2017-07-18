using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 * 
 * 매인매뉴를 관리
 * 
 * jin5866 2017-07-17
 * 
 */ 
public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("_main_game");
    }
}
