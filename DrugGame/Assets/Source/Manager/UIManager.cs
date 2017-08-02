using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
/*
 * 
 * UI 관리
 * 
 * 알람창등을 띠움
 * 
 * 2017-07-17 jin5866
 */ 
public class UIManager : MonoBehaviour {
    public static GameManager gameManager { get; private set; }
    public static UIManager uiManager { get; private set; }
    public static MapManager mapManager { get; private set; }

    public int textOffset = 0;
    public int OKButtonOffset = 1;
    public Transform alarm;

    public UnityAction defaultAction;

    public Text rightUpText;

    public Slider poisonedBar;
    public Slider toleranceBar;
    public Slider healthBar;

    public Image uiPanel;

    public Button[] drugButton;
    public JoyStick joyStick;
    public MotionPanel motionPanel;

    [HideInInspector] public bool isFever;

    private Text text;
    private Button OKButton;



    void Start()
    {
        mapManager = GameManager.mapManager;
        uiManager = this;
        gameManager = GameManager.gameManager;

        text = alarm.GetChild(textOffset).GetComponent<Text>();
        OKButton = alarm.GetChild(OKButtonOffset).GetComponent<Button>();

        AlarmReset();

        alarm.gameObject.SetActive(false);

        defaultAction = DefaultAction;

        rightUpText.text = "";
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
        OKButton.onClick.AddListener(DefaultAction);
        OKButton.onClick.AddListener(a);
        text.text = message;
    }

    public void FEVER(bool set)
    {
        isFever = true;
        rightUpText.color = Color.red;
        StartCoroutine(FeverAlarm());
    }

    public void SetControlActive(bool set)
    {
        //컨트롤 버튼 활성화
        //조이스틱
        joyStick.isActive = set;
        //약 버튼
        foreach(var i in drugButton)
        {
            i.interactable = set;
        }
        //모션 패널
        motionPanel.active = set;
    }
    private void AlarmReset()
    {
        text.text = "";
        OKButton.onClick.RemoveAllListeners();
    }
    

    IEnumerator FeverAlarm()
    {
        while(isFever)
        {
            rightUpText.text = "FEVER";
            yield return new WaitForSeconds(0.5f);
            rightUpText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
