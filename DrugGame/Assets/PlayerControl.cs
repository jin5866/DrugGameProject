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
    private JoyStick joyStick;

	// Use this for initialization
	void Start () {
        joyStick = GameObject.FindGameObjectWithTag("JoyStick").GetComponent<JoyStick>();
	}
	
	// Update is called once per frame
    // 키보드 입력은 테스트용.
	void Update () {
        transform.Translate(Vector3.up * maxSpeed * Time.deltaTime * joyStick.GetVerticalValue());
        transform.Translate(-Vector3.right * maxSpeed * Time.deltaTime * joyStick.GetHorizontalValue());
    }
}
