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
    
    public float mapPosY = 0.0f;
    public Transform[] mapBlock;

    public Transform[] npc;

    public float genTime = 10.0f;

    [HideInInspector] public List<GameObject> blockList;
    public int mapSIze = 10;
    public int initDrug = 100;


    public float blockSize = 30;

    private List<DrugGen> drugGenList;
    private List<GameObject> NPCList;

    private Transform player;

	// Use this for initialization
	void Start () {

        blockList = new List<GameObject>();
        drugGenList = new List<DrugGen>();
        NPCList = new List<GameObject>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        CreateInitMap();
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine("GenDrugRoutine");
	}

    public void CreateInitMap()
    {
        for (int i = -mapSIze; i <= mapSIze; i++) 
        {
            for (int j = -mapSIze; j <= mapSIze; j++)
            {
                GameObject newBlock = CreateOneBlock();
                newBlock.transform.position = new Vector3(i * blockSize, mapPosY, j * blockSize);
            }
        }

        for (int i=0;i<initDrug;i++)
        {
            GenNewDrug();
        }


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
            Destroy(i);
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
                drugGenList.Add(tmp.gameObject.GetComponent<DrugGen>());
            }
        }

        return b;
    }

    

    public void GenNewDrug()
    {
        DrugGen a = drugGenList[Random.Range(0, drugGenList.Count)];
        int count = 0;

        //과하게 안만들어질때 대비용
        while (!a.GenDrug())
        {
            
            count++;
            if(count > 100)
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
        foreach(var i in NPCList)
        {
            Destroy(i);
        }
        blockList.Clear();
        drugGenList.Clear();
        NPCList.Clear();
        CreateInitMap();
    }

    IEnumerable GenDrugRoutine()
    {
        while(true)
        {
            GenNewDrug();
            yield return new WaitForSeconds(genTime);
        }
    }
}
