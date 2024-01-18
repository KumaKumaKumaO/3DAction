using UnityEngine;
[System.Serializable]
public struct CollisionData
{
	[SerializeField]
	private Vector3 _areaHalfSize;
	[SerializeField]
	private Vector3 _offset;
	private Transform _myTransform;
	public Vector3 AreaHalfSize { get { return _areaHalfSize; } }
	public Vector3 Offset { get { return _offset; } }
	public Transform MyTransform { get { return _myTransform; } }

	public void Init(Transform myTransform)
	{
		this._myTransform = myTransform;
	}
}