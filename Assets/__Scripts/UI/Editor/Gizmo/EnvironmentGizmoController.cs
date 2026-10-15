using TransformHandles;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentGizmoController : MonoBehaviour
{
    [SerializeField] private EnvironmentObjectColliderInput environmentObjectColliderInput;
    
    [SerializeField] private Toggle positionToggle;
    [SerializeField] private Toggle rotationToggle;
    [SerializeField] private Toggle scaleToggle;
    
    [SerializeField] private Toggle worldSpaceToggle;
    [SerializeField] private Toggle localSpaceToggle;

    
    // TODO: Apply these changes to new handles as well, not just existing ones.

    public void PositionToggled()
    {
        if (positionToggle.isOn)
        {
            rotationToggle.isOn = false;
            scaleToggle.isOn = false;
            SetHandleType(HandleType.Position);
        }
    }
    
    public void RotationToggled()
    {
        if (rotationToggle.isOn)
        {
            positionToggle.isOn = false;
            scaleToggle.isOn = false;
            SetHandleType(HandleType.Rotation);
        }
    }
    
    public void ScaleToggled()
    {
        if (scaleToggle.isOn)
        {
            positionToggle.isOn = false;
            rotationToggle.isOn = false;
            SetHandleType(HandleType.Scale);
        }
    }
    
    public void WorldSpaceToggled()
    {
        if (worldSpaceToggle.isOn)
        {
            localSpaceToggle.isOn = false;
            SetHandleSpace(Space.World);
        }
    }
    
    public void LocalSpaceToggled()
    {
        if (localSpaceToggle.isOn)
        {
            worldSpaceToggle.isOn = false;
            SetHandleSpace(Space.Self);
        }
    }


    public void SetHandleType(HandleType handleType)
    {
        foreach (var handle in environmentObjectColliderInput.activeHandles)
            TransformHandleManager.ChangeHandleType(handle, handleType);
    }
    
    public void SetHandleSpace(Space space)
    {
        foreach (var handle in environmentObjectColliderInput.activeHandles)
            TransformHandleManager.Instance.ChangeHandleSpace(handle, space);
    }
}
