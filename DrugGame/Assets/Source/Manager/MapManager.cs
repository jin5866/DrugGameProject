using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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

    public GameManager gameManager;

    public float mapPosY = 0.0f;
    public Transform[] mapBlock;
    public Transform[] feverMapBlock;

    public float feverMapHight = 50f;

    public Transform[] npc;

    public float drugGenTime = 10.0f;
    public float potionGenTime = 10.0f;

    

    public int mapSIze = 10;
    public int initDrug = 100;
    public int initPotion = 20;

    public float blockSize = 30;

    private List<GenPosition> drugGenList;
    private List<GameObject> NPCList;
    private List<GenPosition> potionGenList;

    private Transform player;

    [HideInInspector] public List<GameObject> blockList;
    [HideInInspector] public List<GameObject> feverBlockList;
    [HideInInspector] private bool isPlaying;

    public bool isFever;

	// Use this for initialization
	void Start () {
        isPlaying = true;

        blockList = new List<GameObject>();
        feverBlockList = new List<GameObject>();
        drugGenList = new List<GenPosition>();
        NPCList = new List<GameObject>();
        potionGenList = new List<GenPosition>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        CreateInitMap();
	}
	
	// Update is called once per frame
	void Update () {
        isPlaying = gameManager.isPlaying;
	}

    public void CreateInitMap()
    {
        for (int i = -mapSIze; i <= mapSIze; i++) 
        {
            for (int j = -mapSIze; j <= mapSIze; j++)
            {
                GameObject newBlock = CreateOneBlock();
                newBlock.transform.position = new Vector3(i * blockSize, mapPosY, j * blockSize);
                GameObject newFeverBlock = CreateOneFeverBlock();
                newFeverBlock.transform.position = new Vector3(i * blockSize, feverMapHight, j * blockSize);
            }
        }

        for (int i=0;i<initDrug;i++)
        {
            GenNewDrug();
        }

        for (int i = 0; i < initPotion; i++)
        {
            GenNewPotion();
        }

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

    private GameObject CreateOneBlock()
    {
        Transform a = mapBlock[Random.Range(0, mapBlock.Length)];
        GameObject b = Instantiate(a).gameObject;

        blockList.Add(b);
        int childNum = b.transform.GetChildCount();

        for (int i = 0; i < childNum ; i++) 
        {
            Transform tmp = b.transform.GetChild(i);
            if (tmp.tag == "DrugGen")
            {
                drugGenList.Add(tmp.gameObject.GetComponent<GenPosition>());
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

    

    public void FEVERMap(bool afterDie)
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
