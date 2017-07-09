using Assets.Source.Interface;
using UnityEngine;
using System.Collections;
/*
 * Player의 움직임 관리
 * 
 * 2017-07-03 jin5866
 * 
 */
public class PlayerControl : MonoBehaviour {
    public int playerNum = 1;
    public float maxSpeed = 10.0f;
    public float backSpeed = 3.0f;

    public float rotSpeed = 10.0f;

    public MonoBehaviour chaMover;

    private JoyStick joyStick;
    private ICharacterMover mover;

	// Use this for initialization
	void Start () {
        joyStick = GameObject.FindGameObjectWithTag("JoyStick").GetComponent<JoyStick>();
        mover = (ICharacterMover)chaMover;
	}
	
	// Update is called once per frame
	void Update () {
        //조이스틱 입력값.
        float h = joyStick.GetHorizontalValue();             
        float v = joyStick.GetVerticalValue();

        //캐릭터를 움직인다.
        mover.Move(h, v);
    }
}
