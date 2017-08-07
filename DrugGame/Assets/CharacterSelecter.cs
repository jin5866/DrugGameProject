using Assets.Source.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelecter : MonoBehaviour {

    public Text text;

    public string[] names;

    // Use this for initialization
    void Start() {
        text.text = "Selected : " + names[PlaySetting.playerCha];

    }

    // Update is called once per frame
    void Update() {

    }

    public void CharacterSelect(int a)
    {
        PlaySetting.playerCha = a;
        text.text = "Selected : " + names[a];
    }

    public void Finish()
    {
        SceneManager.LoadScene("_main_menu");
    }
}
