using UnityEngine;

/**
 This class is used to trigger the UI animation for
 the key objects for the NumberKeypad UI element
 **/ 
public class KeypadKeyUI
{

    private Color normalStateColor = new Color(255, 255, 255);
    private Color overStateColor = new Color(0, 204, 102);
    private Color pressStateColor = new Color(0, 255, 0);

    private Renderer renderer;
    public KeypadKeyUI(GameObject keyTextObject)
    {
        //get renderer from GameObject to change color later
        renderer = keyTextObject.GetComponent<Renderer>();

        //Set the initial state for key
        renderer.material.color = normalStateColor;
    }

    // sets the animation state for when hovering the key
    public void hoverKey()
    {
        renderer.material.color = overStateColor;
    }

    // sets the animation state for when stopping hovering the key
    public void stopHoveringKey()
    {
        renderer.material.color = normalStateColor;
    }

    // sets the animation state for pressing the key
    public void pressKey()
    {
        renderer.material.color = pressStateColor;
    }

    // sets the animation state for releasing the key
    public void releaseKey()
    {
        renderer.material.color = overStateColor;
    }

}
