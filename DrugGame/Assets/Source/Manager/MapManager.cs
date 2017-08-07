using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Source.Manager;

/*
 * 2017-07-04 jin5866
 * 
 * 맵 매니저
 * 
 * 
 * 
 * 
 */

public class MapManager : MonoBehaviour {

    public static GameManager gameManager { get; private set; }
    public static UIManager uiManager { get; private set; }
    public static MapManager mapManager { get; private set; }

    public float mapPosY = 0.0f;
    public Transform[] startBlock;
    public Transform[] mapBlock;
    public Transform[] feverMapBlock;

    public float feverMapHight = 50f;

    public Transform[] npc;
    public Transform[] playerPreFab;
    
    public float drugGenTime = 10.0f;
    public float potionGenTime = 10.0f;

    public int[] specialBlockNum;
    public int specialBlockRatio = 10;

    public int mapSize = 10;
    public int initDrug = 100;
    public int initPotion = 20;

    public float blockSizeX = 200;
    public float blockSizeZ = 100;

    private List<GenPosition> drugGenList;
    private List<GameObject> NPCList;
    private List<GenPosition> potionGenList;

    [HideInInspector] public Transform player { get; private set; }

    [HideInInspector] public List<GameObject> blockList;
    [HideInInspector] public List<GameObject> feverBlockList;
    [HideInInspector] private bool isPlaying;

    public bool isFever;

	// Use this for initialization
	void Start () {
        mapManager = this;
        uiManager = GameManager.uiManager;
        gameManager = GameManager.gameManager;

        isPlaying = true;

        blockList = new List<GameObject>();
        feverBlockList = new List<GameObject>();
        drugGenList = new List<GenPosition>();
        NPCList = new List<GameObject>();
        potionGenList = new List<GenPosition>();

        CreateInitMap();
	}
	
	// Update is called once per frame
	void Update () {
        isPlaying = gameManager.isPlaying;
	}

    public void CreateInitMap()
    {
        for (int i = -mapSize; i <= mapSize; i++) 
        {
            for (int j = -mapSize; j <= mapSize * (blockSizeX / blockSizeZ); j++) 
            {
                GameObject newBlock = CreateOneBlock(i == 0 && j == 0);
                newBlock.transform.position = new Vector3(i * blockSizeX, mapPosY, j * blockSizeZ);
                GameObject newFeverBlock = CreateOneFeverBlock();
                newFeverBlock.transform.position = new Vector3(i * blockSizeX, feverMapHight, j * blockSizeZ);
            }
        }

        //플레이어 소환.
        GenPlayer();

        //적당한 갯수만큼 약 소환.
        for (int i=0;i<initDrug;i++)
        {
            GenNewDrug();
        }

        for (int i = 0; i < initPotion; i++)
        {
            GenNewPotion();
        }

        //약과 포션 젠
        StartCoroutine(GenDrugRoutine());
        StartCoroutine(GenPotionRoutine());

        isFever = false;
    }
    public GameObject CreateOneNPC(int a)
    {
        GameObject tmp = Instantiate(npc[a]).gameObject;
        tmp.transform.position = new Vector3(player.position.x + Random.Range(-20f, 20f), 1.5f, player.position.z + Random.Range(-20f, 20f));
        NPCList.Add(tmp);
        return tmp;
    }

    public void ResetNPC()
    {
        foreach (var i in NPCList)
        {
            i.GetComponent<NPCControl>().controler.Die();
        }
        NPCList.Clear();
    }

    private GameObject CreateOneBlock(bool isStartPoint)
    {
        Transform a;

        if (!isStartPoint)
        {
            int tmp= 0;
            bool passable = false;

            while(!passable)
            {
                tmp = Random.Range(0, mapBlock.Length);
                passable = true;
                foreach (var i in specialBlockNum)
                {
                    if (i == tmp)
                    {
                        if(Random.Range(0,100) < specialBlockRatio)
                        {
                            //스페셜 블럭 생성
                            passable = true;
                        }
                        else
                        {
                            //생성 취소.
                            passable = false;
                        }
                        break;
                    }
                }
            }
            
            a = mapBlock[tmp];
        }
        else
        {
            a = startBlock[Random.Range(0, startBlock.Length)];
        }

        GameObject b = Instantiate(a).gameObject;

        blockList.Add(b);
        int childNum = b.transform.GetChildCount();

        for (int i = 0; i < childNum ; i++) 
        {
            Transform tmp = b.transform.GetChild(i);
            if (tmp.tag == "DrugGen")
            {
                GenPosition gp = tmp.gameObject.GetComponent<GenPosition>();
                drugGenList.Add(gp);

                if(isStartPoint)
                {
                    gp.GenItem();
                }
            }
            else if(tmp.tag == "PotionGen")
            {
                potionGenList.Add(tmp.gameObject.GetComponent<GenPosition>());
            }
        }

        return b;
    }

    private GameObject CreateOneFeverBlock()
    {
        Transform a = feverMapBlock[Random.Range(0, feverMapBlock.Length)];
        GameObject b = Instantiate(a).gameObject;
        feverBlockList.Add(b);

        /* 마약과 포션 젠 여부는 결정되지 않음.
        int childNum = b.transform.GetChildCount();

        for (int i = 0; i < childNum; i++)
        {
            Transform tmp = b.transform.GetChild(i);
            if (tmp.tag == "DrugGen")
            {
                drugGenList.Add(tmp.gameObject.GetComponent<GenPosition>());
            }
            else if (tmp.tag == "PotionGen")
            {
                potionGenList.Add(tmp.gameObject.GetComponent<GenPosition>());
            }
        }
        */

        return b;
    }

    private void GenPlayer()
    {
        
        Transform genPos = GameObject.FindGameObjectWithTag("PlayerGen").transform;

        //세팅에 맞게 소환
        player = Instantiate(playerPreFab[PlaySetting.playerCha]);
        player.position = genPos.position;
    }

    public void FEVER(bool afterDie)
    {
        if(!isFever)
        {
            player.position += Vector3.up * feverMapHight;
            isFever = true;
        }
    }

    public void GenNewDrug()
    {
        GenPosition a = drugGenList[Random.Range(0, drugGenList.Count)];
        int count = 0;

        //과하게 안만들어질때 대비용
        while (!a.GenItem())
        {
            a = drugGenList[Random.Range(0, drugGenList.Count)];

            count++;
            if(count > 100)
            {
                return;
            }
        }
    }

    public void GenNewPotion()
    {
        
        GenPosition a = potionGenList[Random.Range(0, potionGenList.Count)];
        int count = 0;

        //과하게 안만들어질때 대비용
        while (!a.GenItem())
        {
            a = potionGenList[Random.Range(0, potionGenList.Count)];
            count++;
            if (count > 100)
            {
                return;
            }
        }
        
    }

    public void NewMap()
    {
        foreach(var i in blockList)
        {
            Destroy(i);
        }
        foreach(var i in feverBlockList)
        {
            Destroy(i);
        }
        foreach(var i in NPCList)
        {
            Destroy(i);
        }
        Destroy(player);
        player = null;

        blockList.Clear();
        drugGenList.Clear();
        NPCList.Clear();
        CreateInitMap();
    }

    IEnumerator GenDrugRoutine()
    {
        while(isPlaying)
        {
            GenNewDrug();
            yield return new WaitForSeconds(drugGenTime);
        }
    }

    IEnumerator GenPotionRoutine()
    {
        while(isPlaying)
        {
            GenNewPotion();
            yield return new WaitForSeconds(potionGenTime);
        }
    }
}
