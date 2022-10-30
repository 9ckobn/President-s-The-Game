using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Collider))]
public class Highlighter : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    [SerializeField]
    private Material originalMaterial;

    [SerializeField]
    private Material highlightedMaterial;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        EnableHighlight(false);

    }

    public void EnableHighlight(bool onOff)
    {
        if (meshRenderer != null && originalMaterial != null &&
            highlightedMaterial != null)
        {
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