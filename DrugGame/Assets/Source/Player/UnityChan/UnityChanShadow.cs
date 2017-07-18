using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanShadow : MonoBehaviour {
    /*
     * 유니티짱의 잔상 관리
     * 미완성
     * 2017-07-18 jin5866
     */ 

    public SpriteRenderer spriteSrc;
    public bool afterImageEnabled;

    private PlayerState playerState;
    private bool isLife;
	// Use this for initialization
	void Start () {
        afterImageEnabled = true;
        playerState = gameObject.GetComponent<PlayerState>();

        StartCoroutine("AfterImage");
	}
	
	// Update is called once per frame
	void Update () {
        isLife = playerState.isLife;
        //StartCoroutine("AfterImage");
    }

    IEnumerator AfterImage()
    {
        while(isLife)
        {
            while(afterImageEnabled)
            {
                //SpriteRenderer spriteCopy = Instantiate(spriteSrc) as SpriteRenderer;
                //spriteCopy.transform.position = spriteSrc.transform.position;
                //spriteCopy.transform.localScale = spriteSrc.transform.parent.transform.localScale;
                //spriteCopy.color = new Color(1.0f, 0f, 0f, 0.5f);
                //spriteCopy.sortingLayerName = "Char";
                //spriteCopy.sortingOrder = 1;
                yield return new WaitForSeconds(1);
            }
            yield return new WaitForSeconds(1);
        }
    }
}
