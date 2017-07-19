using Assets.Source.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * 귀신의 움직임 조절
 * 
 * 2017-07-19 jin5866
 */ 
 [RequireComponent(typeof(Animator))]

public class GhostAction : MonoBehaviour,INPCAction {

    

    private bool isDead;
    private bool isAttackAble;

    private Animator ani;

    public void Attack()
    {
        if(UnityEngine.Random.Range(0,2) ==0)
        {
            ani.SetTrigger("Surround Attack");
        }
        else
        {
            ani.SetTrigger("Bite Attack");
        }
    }

    public void Die()
    {
        isDead = true;
        StartCoroutine(DieCounter());

    }

    public void Move(Vector3 f, Vector3 r)
    {
        if (isDead)
            return;
        transform.Rotate(r);
        transform.Translate(f);
    }

    // Use this for initialization
    void Start () {
        isDead = false;
        isAttackAble = true;

        ani = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator DieCounter()
    {
        ani.SetTrigger("Die");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
