using Assets.Source.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*
 * 유니티짱의 움직임 관리
 * 
 * jin5866
 */ 
public class UnityChanMover : MonoBehaviour,ICharacterMover {

    public float animSpeed = 1.5f;              // アニメーション再生速度設定
    public float lookSmoother = 3.0f;           // a smoothing setting for camera motion
    public bool useCurves = true;               // Mecanimでカーブ調整を使うか設定する
                                                // このスイッチが入っていないとカーブは使われない
    public float useCurvesHeight = 0.5f;

    private float forwardSpeed =  10.0f;
    private float backwardSpeed = 3.0f;
    public float rotateSpeed = 2.0f;

    // ジャンプ威力
    public float jumpPower = 3.0f;

    private CapsuleCollider col;
    private Rigidbody rb;
    // キャラクターコントローラ（カプセルコライダ）の移動量
    private Vector3 velocity;
    // CapsuleColliderで設定されているコライダのHeiht、Centerの初期値を収める変数
    private float orgColHight;
    private Vector3 orgVectColCenter;

    private Animator anim;                          // キャラにアタッチされるアニメーターへの参照
    private AnimatorStateInfo currentBaseState;         // base layerで使われる、アニメーターの現在の状態の参照

    private GameObject cameraObject;    // メインカメラへの参照
    private CameraMove camMove;
    private Vector3 lookDir;

    // アニメーター各ステートへの参照
    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int locoState = Animator.StringToHash("Base Layer.Locomotion");
    static int jumpState = Animator.StringToHash("Base Layer.Jump");
    static int restState = Animator.StringToHash("Base Layer.Rest");

    public float maxSpeed
    {
        set
        {
            forwardSpeed = value;
            backwardSpeed = value * 0.3f;
        }
    }

    public void Move(float h, float v)
    {
        float speed = Mathf.Pow(h * h + v * v, 0.5f);

        if(speed > 0.1f)
        {
            anim.SetFloat("Speed", speed);                                             
            anim.speed = animSpeed;                             
            currentBaseState = anim.GetCurrentAnimatorStateInfo(0); 

            //보는방향으로 돌며 움직이기.
            lookDir = camMove.playerForward.normalized * v + camMove.playerRight.normalized * h;
            transform.rotation = Quaternion.LookRotation(lookDir);
            transform.Translate(Vector3.forward * forwardSpeed * speed * Time.deltaTime);

            //y값을 0으로 고정
            //transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }

        

        if (currentBaseState.nameHash == locoState)
        {
            //カーブでコライダ調整をしている時は、念のためにリセットする
            if (useCurves)
            {
                resetCollider();
            }
        }
        
        else if (currentBaseState.nameHash == idleState)
        {
            //カーブでコライダ調整をしている時は、念のためにリセットする
            if (useCurves)
            {
                resetCollider();
            }
            /*
            // スペースキーを入力したらRest状態になる
            if (Input.GetButtonDown("Jump"))
            {
                anim.SetBool("Rest", true);
            }
            */
        }
        else if (currentBaseState.nameHash == restState)
        {
            //cameraObject.SendMessage("setCameraPositionFrontView");		// カメラを正面に切り替える
            // ステートが遷移中でない場合、Rest bool値をリセットする（ループしないようにする）
            if (!anim.IsInTransition(0))
            {
                anim.SetBool("Rest", false);
            }
        }

    }

    void resetCollider()
    {
        // コンポーネントのHeight、Centerの初期値を戻す
        col.height = orgColHight;
        col.center = orgVectColCenter;
    }

    // Use this for initialization
    void Start () {
        // Animatorコンポーネントを取得する
        anim = GetComponent<Animator>();
        // CapsuleColliderコンポーネントを取得する（カプセル型コリジョン）
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        //メインカメラを取得する
        cameraObject = GameObject.FindWithTag("MainCamera");
        // CapsuleColliderコンポーネントのHeight、Centerの初期値を保存する
        orgColHight = col.height;
        orgVectColCenter = col.center;

        camMove = cameraObject.GetComponent<CameraMove>();
        //움직임 부자연스러운거 수정
        //camMove.moveBuffer = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
