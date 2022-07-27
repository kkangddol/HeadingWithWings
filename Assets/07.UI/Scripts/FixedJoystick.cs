using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[System.Serializable]
public enum JoyStickDirection { Horizontal, Vertical, Both}
public class FixedJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform Background;
    public JoyStickDirection JoystickDirection = JoyStickDirection.Both;
    public RectTransform Handle;
    [Range(0,2f)] public float HandleLimit = 1f;
    private Vector2 input = Vector2.zero;
    //Output
    public float Vertical {get {return input.y;}}
    public float Horizontal {get {return input.x;}}
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 JoyDirection = eventData.position - RectTransformUtility.WorldToScreenPoint(new Camera(),Background.position);
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
        input = Vector2.zero;
        Handle.anchoredPosition = Vector2.zero;
    }

}
