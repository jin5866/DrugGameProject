using Assets.Source.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelecter : MonoBehaviour {

    public Text selected;

    public int[] prices; 

    public string[] names;

    public GameObject alarm;
    public Text alarmText;
    public Button OK;
    public Button cancel;

    public Text coin;

    public string buyString = "{0}코인 인데 {1}을(를) 구매하시겠습니까?";
    public string cannotBuy = "코인이 부족합니다.";
    // Use this for initialization
    void Start() {
        selected.text = "Selected : " + names[PlaySetting.playerCha];

        DataManager.inst.Load();

        DataManager.inst.characterUnlock[0] = true;


        coin.text = "" + DataManager.inst.savedCoin + "$";

        alarm.active = false;
    }

    // Update is called once per frame
    void Update() {

    }

    public void CharacterSelect(int a)
    {
        if(DataManager.inst.characterUnlock[a])
        {
            PlaySetting.playerCha = a;
            selected.text = "Selected : " + names[a];
        }
        else
        {
            UnlockCharacter(a);
        }
    }

    public void Finish()
    {
        SceneManager.LoadScene("_main_menu");
    }

    public void DefaultAction()
    {
        alarm.SetActive(false);
    }

    public void Buy(int a)
    {
        if(prices[a] <= DataManager.inst.savedCoin)
        {
            DataManager.inst.characterUnlock[a] = true;
            DataManager.inst.savedCoin -= prices[a];
            DataManager.inst.Save();
        }
        else
        {
            Alarm(cannotBuy);
        }
    }

    private void UnlockCharacter(int a)
    {
        Alarm(string.Format(buyString, prices[a], names[a]), delegate { Buy(a); }, delegate { });
    }


    private void Alarm(string mess)
    {
        Alarm(mess, delegate { }, delegate { });
    }

    private void Alarm(string mess,UnityAction ok,UnityAction cancel)
    {
        alarm.SetActive(true);
        OK.onClick.AddListener(DefaultAction);
        OK.onClick.AddListener(ok);
        this.cancel.onClick.AddListener(DefaultAction);
        this.cancel.onClick.AddListener(cancel);
        alarmText.text = mess;
    }

    private void AlarmReset()
    {
        selected.text = "";
        OK.onClick.RemoveAllListeners();
        cancel.onClick.RemoveAllListeners();
    }
}
