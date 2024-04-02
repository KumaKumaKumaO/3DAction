using UnityEngine;

/// <summary>
/// �����蔻��̍\����
/// </summary>
[System.Serializable]
public struct CollisionAreaData
{
	[SerializeField]
	private Vector3 _halfAreaSize;
	[SerializeField]
	private Vector3 _offset;
	private UnityEngine.Transform _myTransform;
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
	public UnityEngine.Transform MyTransform { get { return _myTransform; } }

	/// <summary>
	/// ������
	/// </summary>
	/// <param name="myTransform">������Transform</param>
	public void Init(UnityEngine.Transform myTransform)
	{
		this._myTransform = myTransform;
	}

	/// <summary>
	/// �폜����
	/// </summary>
	public void Delete()
	{
		_myTransform = null;
	}
}