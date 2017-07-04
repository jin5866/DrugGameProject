using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    
    public float mapPosZ = 17.0f;
    public Transform[] mapBlock;

    public List<GameObject> blockList;
    public int mapSIze = 10;
    public int initDrug = 100;


    public float blockSize = 30;

    private List<DrugGen> drugGenList;

	// Use this for initialization
	void Start () {

        blockList = new List<GameObject>();
        drugGenList = new List<DrugGen>();

        CreateInitMap();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CreateInitMap()
    {
        for (int i = -mapSIze; i <= mapSIze; i++) 
        {
            for (int j = -mapSIze; j <= mapSIze; j++)
            {
                GameObject newBlock = CreateOneBlock();
                newBlock.transform.position = new Vector3(i * blockSize, j * blockSize, mapPosZ);
            }
        }

        for (int i=0;i<initDrug;i++)
        {
            GenNewDrug();
        }


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
}
