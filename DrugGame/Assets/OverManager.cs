using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverManager : MonoBehaviour {
    public Text score;
    public Text getCoin;
    public Text getMoney;
	// Use this for initialization
	void Start () {
        try
        {
            DataManager.inst.Load();
            score.text = "" + DataManager.inst.point;
            getCoin.text = "" + DataManager.inst.coin;
            getMoney.text = "" + (DataManager.inst.coin * 100 + DataManager.inst.point / 100);
        }
        catch
        {

        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OK()
    {
        SceneManager.LoadScene("_main_menu");
    }
}
