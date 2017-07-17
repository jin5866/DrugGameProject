using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Source;
using Assets.Source.Interface;
using System;
using UnityEngine.UI;

public class UnityChanBuff : MonoBehaviour,ICharacterBuff {


    public MapManager mapManager;
    public Image uiPanel;

    public float maxFade = 0.33f;

    public int ghostMaxNum = 5;

    private int nowGhost = 0;

    private PlayerControl control;
    private float origSpeed;

    private Color origColor;

    public void GetBuff(float degree)
    {
        control.maxSpeed = 1.3f * origSpeed;
    }

    public void GetDebuff(float degree)
    {
        if(nowGhost < (int)(degree*ghostMaxNum))
        {
            mapManager.CreateOneNPC(0);
            nowGhost++;
        }
    }
    public void ResetBuff()
    {
        mapManager.ResetNPC();
        nowGhost = 0;

        control.maxSpeed = origSpeed;

        uiPanel.color = origColor;
    }

    // Use this for initialization
    void Start () {
        control = GetComponent<PlayerControl>();
        origSpeed = control.maxSpeed;
        origColor = uiPanel.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
