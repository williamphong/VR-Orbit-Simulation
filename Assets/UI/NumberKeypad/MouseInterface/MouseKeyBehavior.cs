using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public abstract class MouseKeyBehavior : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler 
{
    public GameObject keyTextObject;

    protected TMP_Text feedbackText;
    private KeypadKeyUI keypadKeyUI;
    public void Awake()
    {
        keypadKeyUI = new KeypadKeyUI(keyTextObject);

        feedbackText = GameObject.Find("feedback_text").GetComponent<TMP_Text>();
        feedbackText.text = "";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        keypadKeyUI.pressKey();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        keypadKeyUI.releaseKey();
    }

    public abstract void OnPointerClick(PointerEventData eventData);

    public void OnPointerEnter(PointerEventData eventData)
    {
        keypadKeyUI.hoverKey();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        keypadKeyUI.stopHoveringKey();
    }
}
