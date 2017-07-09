using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
/*
* 2017-07-03 jin5866
* 
* 가상 조이스틱
* 
* http://tenlie10.tistory.com/115 를 참조하여 만듬.
*/
public class JoyStick : MonoBehaviour,IDragHandler,IPointerUpHandler,IPointerDownHandler {

    private Image bgImg;
    private RawImage joystick;
    private Vector3 inputVector;

    private Vector2 origPos;

    // Use this for initialization
    void Start () {
        bgImg = GetComponent<Image>();
        joystick = transform.GetChild(0).GetComponent<RawImage>();
        origPos = joystick.rectTransform.anchoredPosition;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //인터페이스 구현
    public void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

            inputVector = new Vector3(pos.x * 2 , pos.y * 2 , 0);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;


            // Move Jooystick Img
            joystick.rectTransform.anchoredPosition = (Vector3)origPos + new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3),
                                                                    inputVector.y * (bgImg.rectTransform.sizeDelta.y / 3));
        }
    }

    public void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector3.zero;
        joystick.rectTransform.anchoredPosition = origPos;
    }


    public float GetHorizontalValue()
    {
        return inputVector.x;
    }

    public float GetVerticalValue()
    {
        return inputVector.y;
    }    
    

}
