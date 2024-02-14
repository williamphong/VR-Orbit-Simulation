using UnityEngine;
using UnityEngine.EventSystems;

public class DeleteMouseKeyBahavior : MouseKeyBehavior
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        string currentValue = feedbackText.text;
        if (currentValue.Length > 0)
        {
            string newValue = currentValue.Remove(currentValue.Length - 1, 1);
            feedbackText.text = newValue;
        }
    }

}
