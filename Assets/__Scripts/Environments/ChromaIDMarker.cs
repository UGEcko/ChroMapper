using System;
using UnityEngine;

/// <summary>
/// Holds Chroma / Environment Enhancement ID / Collider info for objects in an environment
/// </summary>
public class ChromaIDMarker : GizmoCollider
{
    public string ChromaID;
#if UNITY_EDITOR
    [NonSerialized] public bool MarkUse;
    [NonSerialized] public bool MarkActivator;
#endif
}
