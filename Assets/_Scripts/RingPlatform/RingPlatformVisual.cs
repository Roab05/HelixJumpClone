using UnityEngine;

public class RingPlatformVisual : MonoBehaviour
{
    [SerializeField] private Material ringPlatformMaterial;
    [SerializeField] private Material ringPlatformMaterialTransparent;
    private MeshRenderer ringPlatformMeshRenderer;
    private MeshCollider ringPlatformMeshCollider;

    private void Awake()
    {
        ringPlatformMeshRenderer = GetComponent<MeshRenderer>();
        ringPlatformMeshCollider = GetComponent<MeshCollider>();
    }

    public void SetTransparent()
    {
        ringPlatformMeshRenderer.material = ringPlatformMaterialTransparent;
        ringPlatformMeshCollider.enabled = false;
        foreach (Transform childTransform in transform)
        {
            if (childTransform.TryGetComponent<RingPlatformVisual>(out var childRingPlatformVisual))
            {
                childRingPlatformVisual.SetTransparent();
            }
        }
    }

}
