using UnityEngine;
public struct CollisionData
{
    private Vector3 _areaSize;
    private Vector3 _offset;
    private ColliderType _myColliderType;
    public Vector3 AreaSize { get { return _areaSize; } }
    public Vector3 Offset { get { return _offset; } }
    public ColliderType MyColliderType { get { return _myColliderType; } }
}