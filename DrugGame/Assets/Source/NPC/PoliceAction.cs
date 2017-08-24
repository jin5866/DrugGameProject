using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Assets.Source.Interface;
using System;

public class PoliceAction : MonoBehaviour, INPCAction {


    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject mask;
    private PlayerState state;
    private Animator ani;
    private NavMeshAgent agent;

    private Vector3 destination;

    [SerializeField]
    private bool isReturning = false;
    [SerializeField]
    private bool isDetected = false;
    [SerializeField]
    private bool isMoving = true;
    [SerializeField]
    private float moveTimer = 3.0f, detectTimer = 1.0f, chaseTimer = 15.0f;

    public float detectDistance = 20.0f; //경찰의 플레이어 감지 거리

    [SerializeField]
    private Vector3 origin;

    private bool init = true;

    public void Attack()
    {
       ani.SetTrigger("ATTACK");
    }

    public void Die()
    {
        
    }

    public void Move(Vector3 f, Vector3 r)
    {
        if (!agent.isOnNavMesh)
        {
            return;
        }
        if (isDetected) // 플레이어를 따라가고 있음
        {
            agent.destination = player.transform.position;
            
            //running animation
        }
        else if (isReturning)
        {
            agent.isStopped = false;
            agent.destination = origin;
            if (Vector3.Distance(transform.position, origin) <= 0.1f)
            {
                isReturning = false;
                chaseTimer = 15.0f;
            }
        }
        else if (isMoving)
        {
            agent.isStopped = false;
            if (agent.velocity.magnitude < 0.01f)
            {
                destination = new Vector3(UnityEngine.Random.Range(-50, 50), 0, UnityEngine.Random.Range(-50, 50));
                agent.destination = origin + destination;
            }
            
            //transform.Translate(new Vector3(0, 0, 0.1f));
            //walking animation
        }
        else
        {
            agent.isStopped = true;
            //idle animation
        }
    }

    // Use this for initialization
    void Start () {
        player = GameManager.player.gameObject;
        state = player.GetComponent<PlayerState>();
        isMoving = false;
        agent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        origin = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (init)
        {
            transform.position = origin;
            init = false;
        }

        if (state.getPoisoned() >= 70)
            mask.SetActive(true);
        else
            mask.SetActive(false);

        if (!isReturning)
            moveTimer -= Time.deltaTime;
        //detectTimer -= Time.deltaTime;

        //플레이어의 약기운이 셀 수록 탐지 범위가 늘어남
        detectDistance = 50 + state.getPoisoned() / 2;

        if (!isReturning && moveTimer < 0)
        {
            moveTimer = UnityEngine.Random.Range(3f, 10f);
            isMoving = !isMoving;
            ani.SetBool("IDLE", !isMoving);
        }

        if (isDetected)
            chaseTimer -= Time.deltaTime;

        //탐지범위 벗어나면 쫓아가는 행동 해제
        //if (Vector3.Distance(player.transform.position, transform.position) > detectDistance)
        //{
        //    ani.SetBool("DETECT",false);
        //    isDetected = false;
        //    agent.speed = 5;
        //}

        //10초간 쫓아가다가 원래 자리로 돌아감
        if (chaseTimer < 0 && !isReturning)
        {
            ani.SetBool("DETECT", false);
            ani.SetBool("IDLE", false);
            isReturning = true;
            isDetected = false;
            agent.speed = 5;
        }

        ////1초마다 주변을 탐지하며, 탐지 범위 내에 플레이어가 있을 경우, 랜덤을 통해 탐지성공, 또는 탐지실패함
        //if (!isDetected && detectTimer < 0 && Vector3.Distance(player.transform.position, transform.position) <= detectDistance)
        //{
        //    //약기운 50 이상부터 탐지시작, 랜덤을 통해 확률 조절
        //    //ex)   약기운 50 일때 탐지확률 2/7
        //    //      약기운 65 일때 탐지확률 1/2
        //    //      약기운 80 일때 탐지확률 5/7
        //    isDetected = (state.getPoisoned() >= 0 && state.getPoisoned() >= UnityEngine.Random.Range(30f, 100f));
        //    detectTimer = 1.0f;
        //}

        //debugging
        if (Vector3.Distance(player.transform.position,transform.position) <= detectDistance && state.getPoisoned() >= 50 && !isDetected && !isReturning)
        {
            ani.SetBool("DETECT", true);
            isDetected = true;
            agent.speed = 20;
        }
    }
}
