using UnityEngine;
[System.Serializable]
public struct CollisionData
{
	[SerializeField]
	private Vector3 _halfAreaSize;
	[SerializeField]
	private Vector3 _offset;
	private Transform _myTransform;
	public Vector3 HalfAreaSize { get { return _halfAreaSize; } }
	public Vector3 Offset { get { return _offset; } }
	public float TopYPos { get { return _myTransform.position.y + _offset.y + _halfAreaSize.y; } }
	public float BottomYPos { get { return _myTransform.position.y + _offset.y - _halfAreaSize.y; } }
	public float RightXPos { get { return _myTransform.position.x + _offset.x + _halfAreaSize.x; } }
	public float LeftXPos { get { return _myTransform.position.x + Offset.x - _halfAreaSize.x; } }
	public float ForwardZPos {  get { return _myTransform.position.z + Offset.z + _halfAreaSize.z; } }
	public float BackZPos { get { return _myTransform.position.z + _offset.z - _halfAreaSize.z; } }
	public Transform MyTransform { get { return _myTransform; } }

	public void Init(Transform myTransform)
	{
		this._myTransform = myTransform;
	}
}