using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Source.Interface;
using System;

public class PoliceAction : MonoBehaviour, INPCAction {

    private GameObject player;
    private PlayerState state;

    public bool isDetected = false;

    private bool isMoving = true, clockWise = true;

    private float moveTimer = 3.0f, dirTimer = 5.0f, detectTimer = 1.0f;

    public float detectDistance = 20.0f; //경찰의 플레이어 감지 거리

    public void Attack()
    {
        
    }

    public void Die()
    {
        
    }

    public void Move(Vector3 f, Vector3 r)
    {
        if (isDetected) // 플레이어를 따라가고 있음
        {
            transform.Rotate(r);
            transform.Translate(f);
        }
        else if (isMoving)
        {
            transform.Translate(new Vector3(0, 0, 0.1f));
        }
        else
        {
            if (clockWise)
                transform.Rotate(new Vector3(0, 0.5f, 0));
            else
                transform.Rotate(new Vector3(0, -0.5f, 0));
        }
    }

    // Use this for initialization
    void Start () {
        player = GameManager.player.gameObject;
        state = player.GetComponent<PlayerState>();
        isMoving = true;
        
	}
	
	// Update is called once per frame
	void Update () {
        moveTimer -= Time.deltaTime;
        dirTimer -= Time.deltaTime;
        detectTimer -= Time.deltaTime;

        //약기운이 셀 수록 탐지 범위가 늘어남
        detectDistance = 100 + state.getPoisoned();

        if (moveTimer < 0)
        {
            moveTimer = UnityEngine.Random.Range(1f, 5f);
            isMoving = !isMoving;
        }
        if (dirTimer < 0)
        {
            dirTimer = UnityEngine.Random.Range(1f, 5f);
            clockWise = !clockWise;
        }

        //탐지범위 벗어나면 쫓아가는 행동 해제
        if (Vector3.Distance(player.transform.position, transform.position) > detectDistance)
            isDetected = false;

        //1초마다 주변을 탐지하며, 탐지 범위 내에 플레이어가 있을 경우, 랜덤을 통해 탐지성공, 또는 탐지실패함
        if (detectTimer < 0 && Vector3.Distance(player.transform.position, transform.position) <= detectDistance)
        {
            //약기운 50 이상부터 탐지시작, 랜덤을 통해 확률 조절
            //ex)   약기운 50 일때 탐지확률 2/7
            //      약기운 65 일때 탐지확률 1/2
            //      약기운 80 일때 탐지확률 5/7
            isDetected =  (state.getPoisoned() >= 50 && state.getPoisoned() >= UnityEngine.Random.Range(30f, 100f));
            detectTimer = 1.0f;
        }
    }
}
