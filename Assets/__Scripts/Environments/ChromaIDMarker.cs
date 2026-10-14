using System;
using UnityEngine;

/// <summary>
/// Holds Chroma / Environment Enhancement ID / Collider info for objects in an environment
/// </summary>
public class ChromaIDMarker : MonoBehaviour
{
    private IntersectionCollider _collider;
    
    public string ChromaID;
#if UNITY_EDITOR
    [NonSerialized] public bool MarkUse;
    [NonSerialized] public bool MarkActivator;
#endif

    void Awake()
    {
        if (TryGetComponent<MeshFilter>(out var meshFilter))
        {
            _collider = gameObject.AddComponent<IntersectionCollider>();
            _collider.Mesh = meshFilter.sharedMesh;
            _collider.HardRefresh();
            _collider.enabled = false;
            _collider.enabled = true;
        }
    }
}
