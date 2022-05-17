using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class FloatingJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform Background;
    public JoyStickDirection JoystickDirection = JoyStickDirection.Both;
    public RectTransform Handle;
    [Range(0,2f)] public float HandleLimit = 1f;
    private Vector2 input = Vector2.zero;
    //Output
    public float Vertical {get {return input.y;}}
    public float Horizontal {get {return input.x;}}

    Vector2 JoyPosition = Vector2.zero;

    public void OnPointerDown(PointerEventData eventData)
    {
        Background.gameObject.SetActive(true);
        OnDrag(eventData);
        JoyPosition = eventData.position;
        Background.position = eventData.position;
        Handle.anchoredPosition = Vector2.zero;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 JoyDirection = eventData.position - JoyPosition;
        input = (JoyDirection.magnitude > Background.sizeDelta.x / 2f) ? JoyDirection.normalized :
        JoyDirection / (Background.sizeDelta.x / 2f);
        if(JoystickDirection == JoyStickDirection.Horizontal)
            input = new Vector2(input.x, 0f);
        if(JoystickDirection == JoyStickDirection.Vertical)
            input = new Vector2(0f, input.y);
        Handle.anchoredPosition = (input * Background.sizeDelta.x / 2f) * HandleLimit;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Background.gameObject.SetActive(false);
        input = Vector2.zero;
        Handle.anchoredPosition = Vector2.zero;
    }

}
