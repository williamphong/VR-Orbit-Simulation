using UnityEngine;
using UnityEngine.EventSystems;

public class DotMouseKeyBehavior : MouseKeyBehavior
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        string currentValue = feedbackText.text;
        
        if (currentValue.Length > 0)
        {
            if(!currentValue.Contains("."))
            {
                feedbackText.text = feedbackText.text + ".";
            }
        }
        else
        {
            feedbackText.text = "0.";
        }
    }
}
