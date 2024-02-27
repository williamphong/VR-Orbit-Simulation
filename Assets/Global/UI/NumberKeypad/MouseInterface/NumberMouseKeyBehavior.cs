using UnityEngine;
using UnityEngine.EventSystems;

public class NumberMouseKeyBehavior : MouseKeyBehavior
{
    public string keyValue;

    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(keyValue);
        Debug.Log(feedbackText.text);
        feedbackText.text = feedbackText.text + keyValue;
    }

}
