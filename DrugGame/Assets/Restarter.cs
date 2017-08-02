using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour {
    public float buffer = 5f;
	// Use this for initialization
	void Start () {
        
	}
    private void Awake()
    {
        StartCoroutine(Restart());
    }
    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(buffer);
        SceneManager.LoadScene("_main_game");
    }
}
