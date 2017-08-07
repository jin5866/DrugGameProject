using Assets.Source.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeathKnightMover : MonoBehaviour,ICharacterMover {
    private float _maxSpeed = 15f;

    private Animator anim;

    private GameObject cameraObject;
    private Vector3 lookDir;
    private CameraMove camMove;

    public float maxSpeed {
        set { _maxSpeed = value; }
    }

    public void Move(float h, float v)
    {
        float speed = Mathf.Pow(h * h + v * v, 0.5f);


        if(speed > 0.6f)
        {
            if(!anim.GetBool("Run"))
            {
                anim.SetBool("Run", true);
            }
        }
        else if (speed > 0.1f)
        {
            if(!anim.GetBool("Walk"))
            {
                anim.SetBool("Walk", true);
            }
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }

        //보는방향으로 돌며 움직이기.
        lookDir = camMove.playerForward.normalized * v + camMove.playerRight.normalized * h;
        transform.rotation = Quaternion.LookRotation(lookDir);
        transform.Translate(Vector3.forward * _maxSpeed * speed * Time.deltaTime);
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

        cameraObject = GameObject.FindWithTag("MainCamera");
        camMove = cameraObject.GetComponent<CameraMove>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
