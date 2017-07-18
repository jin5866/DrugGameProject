using UnityEngine;
using System.Collections;
/*
 * 
 * 맵 위에 있는 약
 * 
 * 
 * 
 * 
 */ 
public class DrugObj : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            transform.parent.GetComponent<GenPosition>().PlayerGetItem(other.GetComponent<PlayerControl>().playerNum);
            other.GetComponent<DrugInvetory>().GetDrug();
            Destroy(gameObject);
        }
    }
}
