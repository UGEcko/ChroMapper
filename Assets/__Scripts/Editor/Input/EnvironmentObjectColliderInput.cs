using System.Collections.Generic;
using TransformHandles;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;


// TODO: Fix: Some objects (like spectrogram in Origins or Gaga Aurora) have inaccurate or just non-functional colliders.
public class EnvironmentObjectColliderInput : MonoBehaviour
{
    [SerializeField] private CameraManager _cameraManager;
    [SerializeField] private EnvironmentGizmoController _gizmoController;
    
    [SerializeField] private TransformHandleSettings transformHandleSettings;
    private TransformHandleManager transformHandleManager;
    
    [SerializeField] private LayerMask whitelistLayers;
    
    public List<Handle> activeHandles = new();
    
    void Start()
    {
        transformHandleManager = TransformHandleManager.Instance;
        transformHandleManager.Settings = transformHandleSettings;
    }
    
    private bool mouseDown = false;
    
    // TODO: If the user clicks on 2+ objects, render the origin indicator on the objects where the gizmo *would* be.
    public void Update()
    {
        var mousePosition = Mouse.current.position.ReadValue();
        
        if (Input.GetMouseButton((int)MouseButton.Left) && Application.isFocused && !mouseDown)
        {
            mouseDown = true;
            
            if (activeHandles.Count > 0 && activeHandles.Exists(isHoveringOnHandle))
                return;
            
            var ray = _cameraManager.SelectedCameraController.Camera.ScreenPointToRay(mousePosition);
            var hasHit = Intersections.Raycast(ray, -1, out var hit);
            
            if (hasHit && hit.GameObject.TryGetComponent<ChromaIDMarker>(out var marker))
            {
                Debug.Log("Selected " + marker.ChromaID);
                
                // Check if user is holding shift, if they are, then we dont destroy existing handles.
                if (Input.GetKey(KeyCode.LeftShift) && activeHandles.Count > 0)
                    transformHandleManager.AddTarget(hit.GameObject.transform, activeHandles[0]);
                else
                {
                    ResetHandles();
                    activeHandles.Add(CreateHandleForObject(hit.GameObject.transform));
                    
                    if (!_gizmoController.isActiveAndEnabled)
                        _gizmoController.gameObject.SetActive(true);
                }
            }
            /*
            else
            {
                ResetHandles();
                _gizmoController.gameObject.SetActive(false);
            }
            */
        }
        
        if (Input.GetMouseButtonUp((int)MouseButton.Left))
            mouseDown = false;
    }
    
    
    Handle CreateHandleForObject(Transform t)
    {
        Handle handle = transformHandleManager.CreateHandle(t);
        handle.OnInteractionStartEvent += _ => Debug.Log("Started interacting with " + t.name);
        handle.OnInteractionEndEvent += _ => Debug.Log("Stopped interacting with " + t.name);
        
        return handle;
    }

    void ResetHandles()
    {
        transformHandleManager.DestroyAllHandles();
        activeHandles.Clear();
    }
    
    bool isHoveringOnHandle (Handle handle)
    {
        // Holy fuck is this really the only way lmao
        return handle.gameObject.GetComponentInChildren<MeshRenderer>().material.color ==
               transformHandleManager.Settings.HighlightColor;
    }
}
