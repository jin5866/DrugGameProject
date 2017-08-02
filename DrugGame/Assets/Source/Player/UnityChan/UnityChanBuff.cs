using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Source;
using Assets.Source.Interface;
using System;
using UnityEngine.UI;
/*
 * 2017-07-17 jin5866
 * 
 * 유니티짱의 버프 디버프 관리
 * 
 * 
 */ 
public class UnityChanBuff : MonoBehaviour,ICharacterBuff {

    private Image uiPanel;

    public float maxFade = 0.5f;
    public float fadeBreath = 0.15f;

    public int ghostMaxNum = 5;

    private MapManager mapManager;
    private GameManager gameManager;

    private int nowGhost = 0;

    private PlayerControl control;
    private float origSpeed;

    private Color origColor;

    private bool isBuffed;
    private bool isDebuffed;
    
    public void GetBuff(float degree)
    {
        if (gameManager.isPaused)
            return;

        //디버프 없애기
        if(isDebuffed)
        {
            mapManager.ResetNPC();
            nowGhost = 0;
            uiPanel.color = origColor;
            isDebuffed = false;
        }

        //버프
        control.maxSpeed = origSpeed * (1f + 0.5f * degree);
        Time.timeScale = 1 / (1f + 0.8f * degree);
        isBuffed = true;
    }

    public void GetDebuff(float degree)
    {
        if (gameManager.isPaused)
            return;

        //버프 업애기
        if (isBuffed)
        {
            Time.timeScale = 1;
            control.maxSpeed = origSpeed;
            isBuffed = false;
        }

        

        //디버프
        if (nowGhost < (int)(degree*ghostMaxNum))
        {
            mapManager.CreateOneNPC(0);
            nowGhost++;
        }

        //움직이는 숨쉬는 속도가 처음엔 pi()/4초마다 한번 나중엔 그 두배
        uiPanel.color = new Color(origColor.r, origColor.g, origColor.b, maxFade * (degree + fadeBreath * Mathf.Sin(Time.time * 8 * Mathf.Max(degree, 0.5f))));
        isDebuffed = true;
    }
    public void ResetBuff()
    {
        if (gameManager.isPaused)
            return;

        //디버프 없애기
        if (isDebuffed)
        {
            mapManager.ResetNPC();
            nowGhost = 0;
            uiPanel.color = origColor;
            isDebuffed = false;
        }


        //버프 없애기
        if(isBuffed)
        {
            Time.timeScale = 1;
            control.maxSpeed = origSpeed;
            isBuffed = false;
        }
    }



    // Use this for initialization
    void Start () {
        mapManager = GameManager.mapManager;
        uiPanel = GameManager.uiManager.uiPanel;
        gameManager = GameManager.gameManager;

        control = GetComponent<PlayerControl>();
        origSpeed = control.maxSpeed;
        origColor = uiPanel.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
