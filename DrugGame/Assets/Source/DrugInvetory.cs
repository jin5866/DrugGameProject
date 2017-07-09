using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugInvetory : MonoBehaviour {

    public int invenSize = 16;
    public int maxInvenNum = 99;

    private int lastDrug = Enum.GetValues(typeof(ItemType)).Length;
    private Inven[] inven;

    // Use this for initialization
    void Start () {
        inven = new Inven[invenSize];
        //인벤 초기화.
        for(int i=0;i < invenSize;i++)
        {
            inven[i].type = ItemType.NULL;
            inven[i].num = 0;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool GetUnknownDrug()
    {
        return GetItem(ItemType.unknownDrug,1);
    }

    public bool GetDrug(int num)
    {
        return GetItem((ItemType)UnityEngine.Random.Range((int)ItemType.unknownDrug + 1, lastDrug), num);
    }

    //아이템 여러개 획득
    public bool GetItem(ItemType type,int num)
    {
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
    }

    //인벤토리에서 한칸 한칸
    private struct Inven
    {
        public ItemType type;
        public int num;
    }
}
