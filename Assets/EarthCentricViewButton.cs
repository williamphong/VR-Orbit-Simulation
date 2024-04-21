using UnityEngine;

public class EarthCentricViewButton : MonoBehaviour
{
    public RenderTexture earthCentricTexture; // Drag your Earth-centric camera's Render Texture here
    public GameObject tvScreen; // Drag your TV screen object here

    private Material tvMaterial; // To store the TV screen material

    void Start()
    {
        // Make sure the TV Screen object has a renderer and get its material
        Renderer tvRenderer = tvScreen.GetComponent<Renderer>();
        if (tvRenderer != null)
        {
            tvMaterial = tvRenderer.material;
        }
        else
        {
            Debug.LogError("Renderer component not found on TV screen object");
            this.enabled = false; // Disable this script if no renderer is found
        }

        // Optionally start with the TV off
        tvScreen.SetActive(false);
    }

    private void OnMouseUpAsButton()
    {
        if (tvMaterial != null && earthCentricTexture != null)
        {
            if (tvScreen.activeSelf)
            {
                tvScreen.SetActive(false); // Turn off the TV
            }
            else
            {
                tvMaterial.mainTexture = earthCentricTexture; // Assign the Earth-centric view
                tvScreen.SetActive(true); // Turn on the TV
            }
        }
        else
        {
            if (tvMaterial == null)
                Debug.LogError("TV Material not assigned");
            if (earthCentricTexture == null)
                Debug.LogError("Earth Centric Texture not assigned");
        }
    }
}
