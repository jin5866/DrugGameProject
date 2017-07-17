using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public int textOffset = 0;
    public int OKButtonOffset = 1;
    public Transform alarm;

    public UnityAction defaultAction;

    private Text text;
    private Button OKButton;



    void Start()
    {
        text = alarm.GetChild(textOffset).GetComponent<Text>();
        OKButton = alarm.GetChild(OKButtonOffset).GetComponent<Button>();

        AlarmReset();

        alarm.gameObject.SetActive(false);

        defaultAction = DefaultAction;

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void DefaultAction()
    {
        alarm.gameObject.SetActive(false);
    }

    public void Alarm(string message)
    {
        Alarm(message, delegate { });
    }

    public void Alarm(string message,UnityAction a)
    {
        AlarmReset();
        alarm.gameObject.SetActive(true);
        OKButton.onClick.AddListener(defaultAction);
        OKButton.onClick.AddListener(a);
        text.text = message;
    }


    private void AlarmReset()
    {
        text.text = "";
        OKButton.onClick.RemoveAllListeners();
    }
    
}
