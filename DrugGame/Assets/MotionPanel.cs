using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MotionPanel : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    Vector2 beforePos;
    private CameraMove _camera;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 movePos = eventData.position - beforePos;
        _camera.CameraRotate(movePos);
        beforePos = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        beforePos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        beforePos = Vector2.zero;
    }

    // Use this for initialization
    void Start () {
        beforePos = Vector2.zero;
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMove>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
