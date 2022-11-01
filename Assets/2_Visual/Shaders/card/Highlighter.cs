using UnityEngine;

// 1
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Collider))]
public class Highlighter : MonoBehaviour
{
    // 2
    // reference to MeshRenderer component
    private MeshRenderer meshRenderer;

    [SerializeField]
    private Material originalMaterial;

    [SerializeField]
    private Material highlightedMaterial;

    void Start()
    {
        // 3
        // cache a reference to the MeshRenderer
        meshRenderer = GetComponent<MeshRenderer>();

        // 4
        // use non-highlighted material by default
        EnableHighlight(false);
    }

    // toggle betweeen the original and highlighted materials
    public void EnableHighlight(bool onOff)
    {
        // 5
        if (meshRenderer != null && originalMaterial != null &&
            highlightedMaterial != null)
        {
            // 6
            meshRenderer.material = onOff ? highlightedMaterial : originalMaterial;
        }
    }
    private void OnMouseOver()
    {
        EnableHighlight(true);
    }

    private void OnMouseExit()
    {
        EnableHighlight(false);
    }
}
