using UnityEngine;
using System.Collections;

public class DrugGen : MonoBehaviour {

    public Transform drug;

    private bool isDrug;

	// Use this for initialization
	void Start () {
        isDrug = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool GenDrug()
    {
        if(isDrug)
        {
            return false;
        }

        isDrug = true;
        GameObject a = Instantiate(drug).gameObject;

        a.transform.SetParent(transform);
        a.transform.localPosition = Vector3.up;

        return true;
    }

    public void PlayerGetDrug(int playerNum)
    {
        isDrug = false;
    }
}
