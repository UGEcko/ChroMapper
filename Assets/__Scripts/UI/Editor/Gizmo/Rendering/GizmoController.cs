using System.Collections.Generic;
using UnityEngine;

public static class GizmoController
{
    public static TransformHandleType HandleType { get; private set; } = TransformHandleType.Position;

    public static GizmoRenderer Renderer;
    
    public static List<TransformHandle> activeHandles = new();
    
    public static void SetHandleTypeForAll(TransformHandleType handleType)
    {
        HandleType = handleType;
    }
    
    public static void SetHandleType(TransformHandle handle, TransformHandleType type) => handle.SetHandleType(type);
    public static void SetHandleType(TransformHandleType type, params TransformHandle[] handles)
    {
        foreach (var handle in handles)
            SetHandleType(handle, type);
    }
}

public enum TransformHandleType
{
    Position,
    Rotation,
    Scale,
    Origin
}
