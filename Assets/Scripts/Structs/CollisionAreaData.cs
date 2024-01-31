using UnityEngine;
[System.Serializable]
public struct CollisionAreaData
{
    [SerializeField]
    private Vector3 _halfAreaSize;
    [SerializeField]
    private Vector3 _offset;
    private Transform _myTransform;
    [SerializeField]
    private float _areaWidth;
    public Vector3 HalfAreaSize { get { return _halfAreaSize; } }
    public Vector3 Offset { get { return _offset; } }
    public float TopYPos { get { return _myTransform.position.y + _offset.y + _halfAreaSize.y; } }
    public float BottomYPos { get { return _myTransform.position.y + _offset.y - _halfAreaSize.y; } }
    public float RightXPos { get { return _myTransform.position.x + _offset.x + _halfAreaSize.x; } }
    public float LeftXPos { get { return _myTransform.position.x + _offset.x - _halfAreaSize.x; } }
    public float ForwardZPos { get { return _myTransform.position.z + _offset.z + _halfAreaSize.z; } }
    public float BackZPos { get { return _myTransform.position.z + _offset.z - _halfAreaSize.z; } }
    public float HalfAreaWidth { get { return _areaWidth / 2; } }
    public float AreaWidth { get { return _areaWidth; } }
    public Transform MyTransform { get { return _myTransform; } }

    public float GetCollisionLinePosValue(Transform colTransform)
    {
        Vector3 worldDirection = colTransform.TransformDirection(colTransform.forward);
        float xAbs = Mathf.Abs(worldDirection.x);
        float yAbs = Mathf.Abs(worldDirection.y);
        float zAbs = Mathf.Abs(worldDirection.z);
        if (xAbs >= yAbs)
        {
            if (xAbs >= zAbs)
            {
                if (Mathf.Sign(worldDirection.x) > 0)
                {
                    return LeftXPos;
                }
                else
                {
                    return RightXPos;
                }
            }
            else
            {
                if(Mathf.Sign(worldDirection.z) > 0)
                {
                    return BackZPos;
                }
                else
                {
                    return ForwardZPos;
                }
            }
        }
        else if (yAbs >= zAbs)
        {
            if(Mathf.Sign(worldDirection.y) > 0)
            {
                return BottomYPos;
            }
            else
            {
                return TopYPos;
            }
        }
        else
        {
            if (Mathf.Sign(worldDirection.z) > 0)
            {
                return BackZPos;
            }
            else
            {
                return ForwardZPos;
            }
        }
    }

    public void Init(Transform myTransform)
    {
        this._myTransform = myTransform;
    }
}