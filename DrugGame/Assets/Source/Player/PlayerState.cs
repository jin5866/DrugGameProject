using Assets;
using Assets.Source.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour {

    public float maxPoint = 100f;

    public float initPoisoned = 50;
    public float initTolerance = 0;
    public float initHealth = 100;

    public float stateValueMax = 100.0f;

    public float healthPerSecond = 1.0f;
    public float poisenedPerSedond = 5.0f;
    public float tolerancePerPill = 0.3f;

    public float upDrugPoisened = 90f;
    public float fitDrugPoisened = 25f;
    public float downDrugPoisened = 3f;

    public int stateBarX = 450;
    public int stateBarY = -50;

    public Transform stateBar;
    //public Transform uiPanel;

    private Slider poisonedBar;
    private Slider toleranceBar;
    private Slider healthBar;

    public float debuffPoint = 30f;
    public float buffPoint = 70f;


    public MonoBehaviour characterBuff;


    public float HealPerPotion = 30.0f;

    private MapManager mapManager;
    private UIManager uiMnanger;

    [HideInInspector] public ItemType fitType = ItemType.powder;
    [HideInInspector] public ItemType downType = ItemType.smoke;
    [HideInInspector] public ItemType upType = ItemType.syringe;

    [HideInInspector] public bool isLife = true;

    private float poisoned; //약기운
    private float tolerance; //내성
    private float health; //체력 

    private ICharacterBuff buff;

    private bool isFever;
    // Use this for initialization
    void Start () {
        mapManager = GameManager.mapManager;
        uiMnanger = GameManager.uiManager;

        poisonedBar = uiMnanger.poisonedBar;
        toleranceBar = uiMnanger.toleranceBar;
        healthBar = uiMnanger.healthBar;

        Transform ui = uiMnanger.uiPanel.transform;

        /*
        poisonedBar = Instantiate(stateBar, ui).GetComponent<Slider>();
        toleranceBar = Instantiate(stateBar, ui).GetComponent<Slider>();
        healthBar = Instantiate(stateBar, ui).GetComponent<Slider>();

        poisonedBar.transform.localPosition = new Vector3(-(Screen.width ) + stateBarX, -(Screen.height) + stateBarY * 5);
        toleranceBar.transform.localPosition = new Vector3(-(Screen.width / 2) + stateBarX, -(Screen.height / 2) + stateBarY * 3);
        healthBar.transform.localPosition = new Vector3(-(Screen.width / 2) + stateBarX, -(Screen.height / 2) + stateBarY * 1);
       
        healthBar.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(255 / 255, 0, 0);
        */

        healthBar.transform.GetChild(2).GetComponent<Text>().text = "체력";
        toleranceBar.transform.GetChild(2).GetComponent<Text>().text = "내성";
        poisonedBar.transform.GetChild(2).GetComponent<Text>().text = "약기운";

        buff = (ICharacterBuff)characterBuff;

        InitValue();

    }
	
	// Update is called once per frame
	void Update () {
        poisoned -= Time.deltaTime * poisenedPerSedond;
        health -= Time.deltaTime * healthPerSecond;

        // 나중에 바꿀 계획
        //if (poisoned < 0.1f)
        //    health -= Time.deltaTime * healthPerSecond * 2;

        CheckLife();


        if(poisoned >= 97f && !isFever)
        {
            FEVER(true);
        }

        //Fever시 약기운 Max
        if (isFever)
            poisoned = maxPoint;

        poisoned = Mathf.Max(0, poisoned);
        health = Mathf.Max(0, health);

        CheckBuff();

        

	}
    void FixedUpdate()
    {
        poisonedBar.value = poisoned / stateValueMax;
        toleranceBar.value = tolerance / stateValueMax;

        //Fever시 health 값과 달리 UI상 표기는 Max로 고정
        if (isFever)
            healthBar.value = 1;
        else
            healthBar.value = health / stateValueMax;
    }

    void InitValue()
    {
        health = initHealth;
        tolerance = initTolerance;
        poisoned = initPoisoned;

        isLife = true;
        isFever = false;
    }

    void CheckLife()
    {
        if(health <= 0.0f)
        {
            //죽음
            isLife = false;

            //조정 불가.
            GameManager.uiManager.SetControlActive(false);
        }
            
    }

    void CheckBuff()
    {
        if(poisoned < debuffPoint)
        {
            buff.GetDebuff((debuffPoint - poisoned) / debuffPoint);
        }
        else if(poisoned < buffPoint)
        {
            buff.ResetBuff();
        }
        else
        {
            buff.GetBuff((buffPoint - poisoned) / (maxPoint - buffPoint));
        }

    }
    
    public void FEVER(bool isDead)
    {
        isFever = true;
        healthPerSecond *= 3;

        mapManager.FEVER(isDead);
        GameManager.gameManager.FEVER(isDead);
        uiMnanger.FEVER(true);
    }

    public void UseDrug(ItemType type)
    {
        //차차 조정해갑시다.
        if(type == fitType)
        {
            tolerance += tolerancePerPill;
            poisoned += (fitDrugPoisened * ((maxPoint - tolerance) / maxPoint));
        }
        else if( type == upType)
        {
            tolerance += tolerancePerPill * 2;
            poisoned += 90.0f;
        }
        else if(type == downType)
        {
            tolerance += tolerancePerPill;
            poisoned += (downDrugPoisened * ((maxPoint - tolerance) / maxPoint));
        }
        else
        {

        }

        poisoned = Mathf.Min(poisoned, stateValueMax);
        
    }

    public void GetHealPotion()
    {
        GetHealPotion(1);
    }
    /// <summary>
    /// 가중치가 있는 포션
    /// </summary>
    /// <param name="w"></param>
    public void GetHealPotion(int w)
    {
        health += HealPerPotion * w;
        health = Mathf.Min(health, maxPoint);
    }

    public void GetDamage(float d)
    {
        health -= d;
    }

    public float getPoisoned()
    {
        return poisoned;
    }
}
