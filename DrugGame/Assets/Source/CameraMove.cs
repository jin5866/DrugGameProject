using UnityEngine;
using System.Collections;

/*
 * 
 * 카메라가 캐릭터를 따라 움직입니다.
 * 화면 회전 기능도 추가.
 * 화면 이동에 따라 움직입니다.
 * 
 * 2017-07-08 jin5866
 * 
 */
public class CameraMove : MonoBehaviour {
    public float cameraDistance = Mathf.Pow(700, 0.5f);

    public float moveRate = 0.1f;

    public int xRotatePerSize = 1000;
    public int YRotatePerSize = 1000;

    public float defaultXRotate = 0;
    public float defaultYRotate = 0;

    public float YRotateMin = 10.0f;
    public float YRotateMax = 85.0f;

    public bool moveBuffer = true;

    public Vector3 cameraPos;

    private Transform player;
    

    private Vector3 target;

    private float xRotateDegree;
    private float yRotateDegree;

    private float cameraPosX;
    private float cameraPosY;
    private float cameraPosZ;

    //private Camera _camera;
    // Use this for initialization
    void Start () {
        //플레이어 찾기
        player = GameObject.FindGameObjectWithTag("Player").transform;


        xRotateDegree = defaultXRotate;
        yRotateDegree = defaultYRotate;

        SetCameraPos();

	}
	
	// Update is called once per frame
	void Update () {
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        //update마다 계산
        SetCameraPos();

        //캐릭터를 약간 느리게 따라다님
        target = player.position + cameraPos;
        if(moveBuffer)
        {
            transform.position = Vector3.Lerp(transform.position, target, moveRate);
        }
        else
        {
            transform.position = target;
        }
        transform.LookAt(player);

	}

    public void CameraRotate(Vector2 move)
    {
        xRotateDegree += (-move.x / xRotatePerSize * 360.0f);
        xRotateDegree %= 360.0f;

        yRotateDegree += (move.y / YRotatePerSize * 360.0f);
        yRotateDegree %= 360.0f;
        yRotateDegree = Mathf.Min(yRotateDegree, YRotateMax);
        yRotateDegree = Mathf.Max(yRotateDegree, YRotateMin);

    }

    //카메라 위치 계산
    private void SetCameraPos()
    {
        float horizonDis = Mathf.Cos(yRotateDegree) * cameraDistance;

        cameraPosY = Mathf.Sin(yRotateDegree) * cameraDistance;
        cameraPosY = Mathf.Max(Mathf.Sin(YRotateMin), cameraPosY);

        cameraPosX = -Mathf.Sin(xRotateDegree) * horizonDis;
        cameraPosZ = Mathf.Cos(xRotateDegree) * horizonDis;

        cameraPos = new Vector3(cameraPosX, cameraPosY, cameraPosZ);
    }
}
