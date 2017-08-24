using UnityEngine;
using System.Collections;
/*
 * 2017-07-18 jin5866
 * 
 * 아이템이 만들어지는 위치
 * 아이템을 만들고 관리
 * 
 * 
 */ 
public class GenPosition : MonoBehaviour {

    public Transform item;

    private bool isItem;

	// Use this for initialization
	void Start () {
        isItem = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool GenItem()
    {
        if(isItem)
        {
            return false;
        }

        isItem = true;
        GameObject a = Instantiate(item).gameObject;

        if (tag == "CoinGen")
            transform.position += new Vector3(0, 5, 0);

        a.transform.SetParent(transform);
        a.transform.localPosition = Vector3.zero;

        return true;
    }

    public void PlayerGetItem(int playerNum)
    {
        isItem = false;
    }
}
