using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour,IDragHandler,IPointerUpHandler,IPointerDownHandler {

    public Image joyStickContainer;
    public Image joyStickKnod;
    public Vector2 inputDir;
    public float limitMoveKnod=2f;
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(joyStickContainer.rectTransform,
                                                                eventData.position,
                                                                eventData.pressEventCamera,out pos);

        float x = pos.x / joyStickContainer.rectTransform.sizeDelta.x;
        x = x * 2 - 1;

        float y = pos.y / joyStickContainer.rectTransform.sizeDelta.y;
        y = y * 2 - 1;
        inputDir = new Vector2(x, y);
        inputDir = inputDir.magnitude > 1 ? inputDir.normalized : inputDir;

        joyStickKnod.rectTransform.anchoredPosition = 
            new Vector2(inputDir.x * limitMoveKnod * joyStickContainer.rectTransform.sizeDelta.x,
                        inputDir.y * limitMoveKnod * joyStickContainer.rectTransform.sizeDelta.y);

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputDir = Vector2.zero;
        joyStickKnod.rectTransform.anchoredPosition = Vector2.zero;
    }


}
