using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.parent.GetComponent<GenPosition>().PlayerGetItem(other.GetComponent<PlayerControl>().playerNum);
            other.GetComponent<PlayerState>().GetHealPotion();
            Destroy(gameObject);
        }
    }
}
