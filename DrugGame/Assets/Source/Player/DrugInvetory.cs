using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * 
 * 약 갯수 저장
 * 사용과 획득도 관리
 * 
 * jin5866
 * 
 * 
 */ 

[RequireComponent(typeof(PlayerState))]
public class DrugInvetory : MonoBehaviour {
    public GameManager gameManager;

    public GameObject uiPanel;

    public int invenSize = 3;
    public int maxInvenNum = 99;

    public int DrugMinNum = 1;
    public int DrugMaxNum = 3;
    public int initDrugNum = 1;

    public Transform drugButton;

    public int drugButtonDis = 100;

    public Transform drugButtonPos;

    public Button[] button;

    private int lastDrug = Enum.GetValues(typeof(ItemType)).Length;
    private Inven[] inven;
    private Text[] itemCounter;

    private PlayerState playerState;
    
    // Use this for initialization
    void Start () {
        inven = new Inven[invenSize];
        //button = new Button[invenSize];
        itemCounter = new Text[invenSize];
        //인벤 초기화.
        for(int i=0;i < invenSize;i++)
        {
            inven[i].type = (ItemType)(i);
            inven[i].num = initDrugNum;

            //Transform tmp = Instantiate(drugButton,uiPanel.transform);
            //tmp.SetParent(uiPanel.transform);
            /*
            button[i].transform.position = drugButtonPos.position + (
                new Vector3(
                    -Mathf.Cos((float)i / (invenSize - 1) * 90) * drugButtonDis
                    , Mathf.Sin((float)i / (invenSize - 1) * 90) * drugButtonDis
                    )
                );
                */
            //텍스트 가져오기

            itemCounter[i] = button[i].transform.GetChild(0).GetComponent<Text>();
            itemCounter[i].text = "" + initDrugNum;

            /*
            button[i] = tmp.GetComponent<Button>();
            button[i].onClick.AddListener(
                () => { UseDrug((ItemType)i); }
            );
            //button[i].onClick.
            */
        }


        playerState = GetComponent<PlayerState>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public bool GetDrug()
    {
        gameManager.GetDrug();
        return GetDrug(UnityEngine.Random.Range(DrugMinNum, DrugMaxNum + 1));
    }

    public bool GetDrug(int num)
    {
        return GetDrug((ItemType)UnityEngine.Random.Range(0, lastDrug), num);
    }

    //아이템 여러개 획득
    public bool GetDrug(ItemType type,int num)
    {
        /*
        int part = 0;
        for(part = 0; part<invenSize; part++)
        {
            if(inven[part].type == type)
            {
                if(inven[part].num + num >= maxInvenNum)
                {
                    int tmp = maxInvenNum - inven[part].num;
                    inven[part].num = maxInvenNum;
                    num -= tmp;
                    continue;
                }
                else
                {
                    inven[part].num += num;
                    return true;
                }
            }
            else if(inven[part].type == ItemType.NULL)
            {
                inven[part].type = type;
                inven[part].num = num;
                return true;
            }
        }
        
        //인벤토리 칸이 다 찬 경우
        return false;
        */


        try
        {
            inven[(int)type].num = Mathf.Min(maxInvenNum, inven[(int)type].num + num);
        }
        catch(Exception e)
        {
            return false;
        }

        SetItemCounter();

        return true;
    }



    public void UseDrug(int type)
    {
        if(inven[type].num > 0)
        {
            inven[type].num--;
            playerState.UseDrug((ItemType)type);
        }
        else
        {
            return;
        }

        SetItemCounter();
    }

    private void SetItemCounter()
    {
        for (int i = 0; i < invenSize; i++)
        {
            itemCounter[i].text = "" + inven[i].num;
        }
    }


    //인벤토리에서 한칸 한칸
    private struct Inven
    {
        public ItemType type;
        public int num;
    }
}
