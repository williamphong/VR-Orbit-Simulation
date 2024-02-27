using UnityEngine;
using UnityEngine.EventSystems;

public class EnterMouseKeyBehavior : MouseKeyBehavior
{

    public override void OnPointerClick(PointerEventData eventData)
    {
        string currentValue = feedbackText.text;
        if (currentValue.Length > 0)
        {
            float finalValue = float.Parse(currentValue);
            Debug.Log("final parsed value: " + finalValue);
        }
        else
        {
            Debug.Log("nothing to parse");
        }
    }

}
