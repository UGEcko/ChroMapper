using UnityEngine;

public class TransformHandle : MonoBehaviour
{
    [SerializeField] private TransformHandleType _handleType = TransformHandleType.Position;
    public TransformHandleType HandleType => _handleType;
    
    
    private MaterialPropertyBlock _materialPropertyBlock;
    private Renderer renderer;
    public void SetHandleType(TransformHandleType handleType)
    {
        _handleType = handleType;
    }
    
    public void Awake()
    {
        renderer = GetComponent<Renderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
    }
    
    public void SetColor(Color color)
    {
        if (renderer == null) return;
        renderer.GetPropertyBlock(_materialPropertyBlock);
        _materialPropertyBlock.SetColor("_Color", color);
        renderer.SetPropertyBlock(_materialPropertyBlock);
    }
}
