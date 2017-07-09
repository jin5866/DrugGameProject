using UnityEngine;
using System.Collections;

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
            transform.parent.GetComponent<DrugGen>().PlayerGetDrug(other.GetComponent<PlayerControl>().playerNum);
            other.GetComponent<DrugInvetory>().GetUnknownDrug();
            Destroy(gameObject);
        }
    }
}
