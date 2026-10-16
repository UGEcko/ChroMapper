using UnityEngine;

// Objects that derive this class will 
public class GizmoCollider : MonoBehaviour
{
    protected IntersectionCollider collider;
    
    void Awake()
    {
        if (TryGetComponent<MeshFilter>(out var meshFilter))
        {
            collider = gameObject.AddComponent<IntersectionCollider>();
            collider.Mesh = meshFilter.sharedMesh;
            collider.HardRefresh();
            collider.enabled = false;
            collider.enabled = true;
            collider.enabled = true;
        }
        
        if (GizmoController.Renderer != null)
            GizmoController.Renderer.RenderHandleOnTransform(transform);
    }
}
