using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoBehaviour {
    
    public float mapPosZ = 17.0f;
    public Transform[] mapBlock;

    public static List<GameObject> blockList;
    public int mapSIze = 10;

    public float blockSize = 30;

	// Use this for initialization
	void Start () {
        blockList = new List<GameObject>();
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
    }

    private GameObject CreateOneBlock()
    {
        Transform a = mapBlock[Random.Range(0, mapBlock.Length)];
        GameObject b = Instantiate(a).gameObject;
        blockList.Add(b);
        return b;
    }
}
