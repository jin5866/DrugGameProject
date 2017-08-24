using Assets.Source.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AliceMover : MonoBehaviour,ICharacterMover{
    private float _maxSpeed = 20f;

    private Animation anim;

    private GameObject cameraObject;
    private Vector3 lookDir;
    private CameraMove camMove;

    public float maxSpeed { set { _maxSpeed = value; } }

    state moveSt;

    public void Move(float h, float v)
    {
        float speed = Mathf.Pow(h * h + v * v, 0.5f);

        if (speed > 0.6f)
        {
            if(moveSt != state.run)
            {
                anim.wrapMode = WrapMode.Loop;
                anim.CrossFade("Run");
            }
        }
        else if (speed > 0.1f)
        {
            if (moveSt != state.walk)
            {
                anim.wrapMode = WrapMode.Loop;
                anim.CrossFade("Walk");
            }
        }
        else
        {
            if (moveSt != state.idle)
            {
                anim.wrapMode = WrapMode.Loop;
                anim.CrossFade("Idle");
            }
        }

        //보는방향으로 돌며 움직이기.
        lookDir = camMove.playerForward.normalized * v + camMove.playerRight.normalized * h;
        transform.rotation = Quaternion.LookRotation(lookDir);
        transform.Translate(Vector3.forward * _maxSpeed * speed * Time.deltaTime);
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animation>();

        moveSt = state.idle;

        cameraObject = GameObject.FindWithTag("MainCamera");
        camMove = cameraObject.GetComponent<CameraMove>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    enum state
    {
        idle,
        walk,
        run
    }
    
}


