using UnityEngine;
using System.Collections;

/*
 * 
 * 카메라가 캐릭터를 따라 움직입니다.
 * 
 * 2017-07-03 jin5866
 * 
 */
public class CameraMove : MonoBehaviour {
    public float cameraPosZ = -10.0f;
    public float moveRate = 0.1f;

    private Transform player;
    private float _cameraPosZ;

    private Vector3 target;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _cameraPosZ = cameraPosZ;

	}
	
	// Update is called once per frame
	void Update () {
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector3(player.position.x, player.position.y, _cameraPosZ);
        transform.position = Vector3.Lerp(transform.position, target, moveRate);
	}
}
