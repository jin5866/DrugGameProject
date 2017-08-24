using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * 게임 매니저
 * 게임이 끝났는지 안끝났는지 관리
 * 점수 관리
 * 
 * 2017-07-17 jin5866
 */ 

public class GameManager : MonoBehaviour {
    public static GameManager gameManager { get; private set; }
    public static UIManager uiManager { get; private set; }
    public static MapManager mapManager { get; private set; }

    public int moneyPerCoin = 100;
    public int pointPerMoney = 100;


    public string GameOverString = "앙 쥬금";

    public Text scoreText;

    public int scorePerSecond = 10;

    public int scorePerDrug = 30;

    [HideInInspector] public bool isPlaying = true;

    private PlayerState playerState;


    private float score;

    private static Transform _player;

    public AudioSource BGM;
    public AudioClip normal; //일반 배경음
    public AudioClip america; //fever 배경음

    public int coin = 0;
    public float playTime = 0;

    [HideInInspector] public bool isPaused { get; private set; }

    private int pauseCounter;

    private bool isFever = false;

    public PoliceAction[] policeList;

    public static Transform player
    {
        private set
        {
            _player = value;
        }

        get
        {
            if (_player == null)
            {
                try
                {
                    _player = GameObject.FindGameObjectWithTag("Player").transform;
                }
                catch
                {
                    _player = null;
                }
            }
                

            return _player;
        }
    }

    // Use this for initialization
    void Start () {
        _player = null;

        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
        gameManager = this;

        playerState = player.GetComponent<PlayerState>();
        isPlaying = true;
	}

    void Awake()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
        gameManager = this;
    }

    // Update is called once per frame
    void Update () {
        playTime += Time.unscaledDeltaTime;

        if (!isPlaying)
            return;

        if (GameOverCheck())
        {
            GameOver();
        }
        else
        {
            score += scorePerSecond * Time.deltaTime;
            scoreText.text = "" + (int)score;
        }
        foreach(var p in policeList)
        {
            p.gameObject.SetActive(Vector3.Distance(p.transform.position, player.transform.position) <= 100);
        }
    }

    void GameOver()
    {
        Time.timeScale = 0;
        uiManager.Alarm(GameOverString,GameOverAction);
        isPlaying = false;
    }

    public void GameOverAction()
    {
        DataManager.inst.coin = coin;
        DataManager.inst.savedCoin += coin * moneyPerCoin;
        DataManager.inst.savedCoin += (int)score / pointPerMoney; 
        DataManager.inst.playTime = playTime;
        DataManager.inst.totalPlayTime += playTime;
        DataManager.inst.point = (int)score;
        DataManager.inst.highestPoint = Mathf.Max((int)score, DataManager.inst.highestPoint);
        DataManager.inst.Save();
        SceneManager.LoadScene("_game_over");
    }

    bool GameOverCheck()
    {
        if (playerState == null)
            playerState = GameManager.player.GetComponent<PlayerState>();

        if (!playerState.isLife)
        {
            //죽었을때
            return true;
        }
        else
        {
            //살았을떄
            return false;
        }
    }

    public void GetDrug()
    {
        score += scorePerDrug;
    }

    public void getCoin()
    {
        coin++;
    }


    public void FEVER(bool afterDead)
    {
        scorePerSecond *= 10;
        isFever = true;
        StartCoroutine(Rainbow());
        try
        {
            //BGM audioclip을 미국브금으로 맞춘후, 새로 틈
            //TODO : Fever가 끝날 때 원래 브금(normal)으로 다시 맞추어 줘야 함
            BGM.clip = america;
            BGM.Play();
        }
        catch
        {

        }
    }

    public void Pause(bool set)
    {
        if(set)
        {
            pauseCounter++;
            _Pause(true);
        }
        else
        {
            pauseCounter--;
            if(pauseCounter <= 0)
            {
                _Pause(false);
            }
        }
    }


    private void _Pause(bool set)
    {
        if(set)
        {
            //멈추기
            Time.timeScale = 0f;
            isPaused = true;
            
        }
        else
        {
            //다시 시작
            Time.timeScale = 1f;
            isPaused = false;
        }

        player.GetComponent<PlayerControl>().isPaused = set;

        uiManager.SetControlActive(!set);
    }

    IEnumerator Rainbow()
    {
        Renderer[] renderers = player.GetComponentsInChildren<Renderer>();
        List<Material> materials = new List<Material>();
        foreach (var ren in renderers)
        {
            materials.Add(ren.material);
        }
        List<Color> originalColor = new List<Color>();

        foreach (var mat in materials)
        {
            originalColor.Add(mat.color);
        }

        while (isFever)
        {
            foreach (var mat in materials)
            {
                mat.color = new Color(1, 0, 0);
            }
            for (int i = 0; i < 6; ++i)
            {
                foreach (var mat in materials)
                {
                    mat.color += (Color.yellow - Color.red) / 6;
                }
                yield return new WaitForSeconds(1 / 60f);
            }
            for (int i = 0; i < 6; ++i)
            {
                foreach (var mat in materials)
                {
                    mat.color += (Color.green - Color.yellow) / 6;
                }
                yield return new WaitForSeconds(1 / 60f);
            }
            for (int i = 0; i < 6; ++i)
            {
                foreach (var mat in materials)
                {
                    mat.color += (Color.blue - Color.green) / 6;
                }
                yield return new WaitForSeconds(1 / 60f);
            }
            for (int i = 0; i < 6; ++i)
            {
                foreach (var mat in materials)
                {
                    mat.color += (Color.magenta - Color.blue) / 6;
                }
                yield return new WaitForSeconds(1 / 60f);
            }
            for (int i = 0; i < 6; ++i)
            {
                foreach (var mat in materials)
                {
                    mat.color += (Color.red - Color.magenta) / 6;
                }
                yield return new WaitForSeconds(1 / 60f);
            }
            yield return new WaitForSeconds(1 / 60f);
        }

        for (int i = 0; i < materials.Count; ++i)
        {
            materials[i].color = originalColor[i];
        }
    }
}
