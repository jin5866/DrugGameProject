using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/*
 * 카메라를 모션에 화면에 손가락으로 하는 모션에 따라 움직인다.
 * 
 * 2017-07-17 jin5866
 * 
 */ 
public class MotionPanel : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private bool isAvtive;

    private Vector2 beforePos;
    private CameraMove _camera;

    public void OnDrag(PointerEventData eventData)
    {
        if (!isAvtive)
            return;

        Vector2 movePos = eventData.position - beforePos;
        _camera.CameraRotate(movePos);
        beforePos = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isAvtive)
            return;

        beforePos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isAvtive)
            return;

        beforePos = Vector2.zero;
    }

    public bool active
    {
        set
        {
            isAvtive = value;
        }

        private get
        {
            return isAvtive;
        }
    }

    // Use this for initialization
    void Start () {
        beforePos = Vector2.zero;
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMove>();

        isAvtive = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
