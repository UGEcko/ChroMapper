using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class GizmoRenderer : MonoBehaviour
{
    [SerializeField] private GameObject TransformArrowPrefab;
    [SerializeField] private GameObject RotationRingPrefab;
    [SerializeField] private GameObject ScaleCubePrefab;
    [SerializeField] private GameObject OriginSpherePrefab;
    
    
    public float GizmoScale = 2f;
    
    
    public void RenderHandleOnTransform(Transform target)
    {
        if (GizmoController.HandleType == TransformHandleType.Position)
        {
            var x = InstantiateHandle(TransformArrowPrefab, target.position, Quaternion.Euler(0, 90, 0), transform, new Color(1,0,0,0));
            var y = InstantiateHandle(TransformArrowPrefab, target.position, Quaternion.Euler(-90, 0, 0), transform, new Color(0,1,0,0));
            var z = InstantiateHandle(TransformArrowPrefab, target.position, Quaternion.Euler(0, 0, 0), transform, new Color(0,0,1,0));
            
            var origin = InstantiateHandle(OriginSpherePrefab, target.position, Quaternion.identity, transform, new Color(1,1,1,0));
        }
    }
    
    private TransformHandle InstantiateHandle(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent, Color color)
    {
        var handleObject = Instantiate(prefab, position, rotation, parent);
        handleObject.transform.localScale = Vector3.one * GizmoScale;
        
        var handle = handleObject.GetComponent<TransformHandle>();
        if (handle == null)
        {
            Debug.LogError($"TransformHandle component not found on {handleObject.name}");
            return null;
        }
        handle.SetColor(color);
        return handle;
    }
    

    void Start()
    {
        GizmoController.Renderer = this;
    }
}
