using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
 * 
 * 플레이어 따라다니는 인공지능
 * 
 * 개선필요
 * 
 * 
 * 2017-07-17 jin5866
 * 
 * 
 */ 
public class PlayerFollower : MonoBehaviour {
    public float rotateSpeed = 180f;
    public float speed = 7f;
    public float acceleration = 8.0f;

    public float veryEasyWeight = 0.7f;
    public float easySpeedWeight = 0.9f;

    public float stopDistance = 1.0f;


    private GameObject m_Player;
    private Transform playersPosition;

    private float velocity;

    private Vector3 target;

    // Use this for initialization
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        velocity = 0;
        target = new Vector3(m_Player.transform.position.x, 0, m_Player.transform.position.z);
        //transform.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {
        target = new Vector3(m_Player.transform.position.x, 0, m_Player.transform.position.z);
        StartCoroutine("SetTarget");
        TurnToTarget();
        GoForward();
    }

    void GoForward()
    {
        if (Vector3.Distance(target, transform.position) < stopDistance)
        {
            //가까우면 0까지 줄이기
            velocity -= acceleration * Time.deltaTime;
            velocity = Mathf.Max(0, velocity);
        }
        else
        {
            //멀면 가속
            velocity += acceleration * Time.deltaTime;
            velocity = Mathf.Min(speed, velocity);
        }

        transform.Translate(-transform.forward * Time.deltaTime * velocity);
    }

    void TurnToTarget()
    {
        
        Vector3 trapos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 ta2th = (target - trapos).normalized;
        float a = ta2th.x * transform.right.x + ta2th.y * transform.right.y + ta2th.z * transform.right.z;
        float rightDe = Mathf.Acos(a) * Mathf.Rad2Deg;
        if (rightDe < 20 && rightDe > 160) 
        {

        }
        else if (rightDe < 90) 
        {
            //타겟이 오른쪽일떄
            transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
        }
        else
        {
            //타겟이 왼쪽일떄
            transform.Rotate(-Vector3.up * Time.deltaTime * rotateSpeed);
        }
        
        //transform.LookAt(target);
    }

    IEnumerable SetTarget()
    {
        while(true)
        {
            target = new Vector3(m_Player.transform.position.x, 0, m_Player.transform.position.z);
            yield return new WaitForSeconds(0.1f);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Block")
        {

        }
    }
}
