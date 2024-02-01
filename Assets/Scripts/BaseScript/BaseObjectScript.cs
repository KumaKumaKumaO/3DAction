using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 全てのオブジェクトの元のクラス
/// </summary>
public abstract class BaseObjectScript : MonoBehaviour
{
	[SerializeField]
	protected bool isGravity = false;
	[SerializeField]
	protected CollisionAreaData _myCollisionAreaData = default;
	protected List<CollisionResultData> _myCollisionObjects = new List<CollisionResultData>();
	[SerializeField]
	protected int _bottomCollisionIndex = -1;
	[SerializeField]
	protected int _topCollisionAreaDataIndex = -1;
	[SerializeField]
	protected int _rightCollisionAreaDataIndex = -1;
	[SerializeField]
	protected int _leftCollisionAreaDataIndex = -1;
	[SerializeField]
	protected int _forwardCollisionAreaDataIndex = -1;
	[SerializeField]
	protected int _backCollisionAreaDataIndex = -1;
	protected ObjectManagerScript _objectManagerScript = default;
	protected Matrix4x4 _matrixTemp = default;
	[SerializeField]
	protected bool isGround = false;


	[Header("デバッグ用")]


	[SerializeField]
	protected bool isDebugColliderVisible = false;
	public CollisionAreaData MyCollisionAreaData { get { return _myCollisionAreaData; } }
	public bool IsGround { get { return isGround; } }
	public virtual void Init()
	{
		GameObject objectManagerObject = GameObject.FindWithTag("ObjectManager");
		if (objectManagerObject == null)
		{
			ErrorManagerScript.MyInstance.NullGameObjectError("ObjectManagerのタグがついたオブジェクト");
		}
		else if (!objectManagerObject.TryGetComponent<ObjectManagerScript>(out _objectManagerScript))
		{
			ErrorManagerScript.MyInstance.NullScriptError("ObjectManagerScript");
		}
		_myCollisionAreaData.Init(transform);
	}
	public virtual void ObjectUpdate()
	{
		if (isGravity)
		{
			GravityFall();
		}

	}
	protected void SelectColObjectResult()
	{
		for (int i = 0; i < _myCollisionObjects.Count; i++)
		{
			if (_myCollisionObjects[i].IsCollisionBottom)
			{
				if (_bottomCollisionIndex < 0
					|| !(_myCollisionObjects[_bottomCollisionIndex].CollisionObjectData.MyCollisionAreaData.TopYPos
					< _myCollisionObjects[i].CollisionObjectData.MyCollisionAreaData.TopYPos))
				{
					_bottomCollisionIndex = i;

				}
			}
			if (_myCollisionObjects[i].IsCollisionTop)
			{
				if (_topCollisionAreaDataIndex < 0
					|| _myCollisionObjects[_topCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData.BottomYPos
					> _myCollisionObjects[i].CollisionObjectData.MyCollisionAreaData.BottomYPos)
				{
					_topCollisionAreaDataIndex = i;
				}
			}
			if (_myCollisionObjects[i].IsCollisionRight)
			{
				if (_rightCollisionAreaDataIndex < 0
					|| _myCollisionObjects[_rightCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData.LeftXPos
					> _myCollisionObjects[i].CollisionObjectData.MyCollisionAreaData.LeftXPos)
				{
					_rightCollisionAreaDataIndex = i;
				}
			}
			if (_myCollisionObjects[i].IsCollisionLeft)
			{
				if (_leftCollisionAreaDataIndex < 0
					|| _myCollisionObjects[_leftCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData.RightXPos
					< _myCollisionObjects[i].CollisionObjectData.MyCollisionAreaData.RightXPos)
				{
					_leftCollisionAreaDataIndex = i;
				}
			}
			if (_myCollisionObjects[i].IsCollisionForward)
			{
				if (_forwardCollisionAreaDataIndex < 0
					|| _myCollisionObjects[_forwardCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData.BackZPos
					> _myCollisionObjects[i].CollisionObjectData.MyCollisionAreaData.BackZPos)
				{
					_forwardCollisionAreaDataIndex = i;
				}
			}
			if (_myCollisionObjects[i].IsCollisionBack)
			{
				if (_backCollisionAreaDataIndex < 0
					|| _myCollisionObjects[_backCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData.ForwardZPos
					< _myCollisionObjects[i].CollisionObjectData.MyCollisionAreaData.ForwardZPos)
				{
					_backCollisionAreaDataIndex = i;
				}
			}
		}
	}
	protected virtual void Reset()
	{
		gameObject.tag = "Object";
	}
	protected void CollisionIndexInit()
	{
		_bottomCollisionIndex = -1;
		_topCollisionAreaDataIndex = -1;
		_rightCollisionAreaDataIndex = -1;
		_leftCollisionAreaDataIndex = -1;
		_forwardCollisionAreaDataIndex = -1;
		_backCollisionAreaDataIndex = -1;
	}
	public virtual void ObjectMove(Vector3 vector)
	{
		Vector3 beforPos = _myCollisionAreaData.MyTransform.position;
		_myCollisionAreaData.MyTransform.position += vector;
		SearchHitObjects();
		//Debug.LogWarning("Dot:" + Vector3.Dot(Vector3.forward, vector - Vector3.up * vector.y));
		MoveClamp(_myCollisionAreaData.MyTransform.position - beforPos);
	}
	protected virtual void SearchHitObjects()
	{
		CollisionIndexInit();
		_myCollisionObjects.Clear();
		_objectManagerScript.GetCollisionAllObject(_myCollisionAreaData, _myCollisionObjects);
		SelectColObjectResult();
	}
	private void MoveClamp(Vector3 moveDirection)
	{
		//下
		//動いている方向は世界軸　自分の下のオブジェクトのインデックス
		if (moveDirection.y < 0 && _bottomCollisionIndex >= 0)
		{
			_myCollisionAreaData.MyTransform.position += Vector3.up *
				(_myCollisionObjects[_bottomCollisionIndex].CollisionObjectData.MyCollisionAreaData.TopYPos
				- _myCollisionAreaData.BottomYPos + _myCollisionAreaData.AreaWidth);
			//Debug.Log("した");
			SearchHitObjects();
		}
		//上
		else if (moveDirection.y > 0 && _topCollisionAreaDataIndex >= 0)
		{
			_myCollisionAreaData.MyTransform.position += Vector3.down *
				(_myCollisionObjects[_topCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData.TopYPos
				- _myCollisionAreaData.BottomYPos + _myCollisionAreaData.AreaWidth);
			SearchHitObjects();
		}
		////右
		////世界軸で右　自分から見て右にあたる
		//if (moveDirection.x > 0 && _rightCollisionAreaDataIndex >= 0)
		//{
		//	Debug.LogError("みぎ");
		//	//左 * 当たった左端　+ 自身の右
		//	_myCollisionAreaData.MyTransform.position += Vector3.left *
		//		(-_myCollisionObjects[_rightCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData.LeftXPos
		//		+ _myCollisionAreaData.RightXPos - _myCollisionAreaData.AreaWidth);
		//	SearchHitObjects();
		//}
		//////左
		//else if (moveDirection.x < 0 && _leftCollisionAreaDataIndex >= 0)
		//{
		//	Debug.Log("左");
		//	_myCollisionAreaData.MyTransform.position += Vector3.right *
		//		(_myCollisionObjects[_leftCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData.RightXPos
		//		- _myCollisionAreaData.LeftXPos - _myCollisionAreaData.AreaWidth);
		//	SearchHitObjects();
		//}
		int index = SelectDirectionIndex(moveDirection);
		//前
		if (_forwardCollisionAreaDataIndex >= 0)
		{
			Debug.Log(_forwardCollisionAreaDataIndex);
			Debug.Log("前:" +
				_myCollisionObjects[_forwardCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData
				.GetCollisionLinePosValue(moveDirection)
				+ ":" + _myCollisionAreaData.ForwardZPos + ":" + _myCollisionAreaData.AreaWidth);

			_myCollisionAreaData.MyTransform.position += transform.forward *
				(_myCollisionObjects[_forwardCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData
				.GetCollisionLinePosValue(moveDirection)
				- _myCollisionAreaData.ForwardZPos + _myCollisionAreaData.AreaWidth);

		}
		////後
		//else if (moveDirection.z < 0 && _backCollisionAreaDataIndex >= 0)
		//{
		//	_myCollisionAreaData.MyTransform.position += Vector3.forward *
		//		(_myCollisionObjects[_backCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData.ForwardZPos
		//		- _myCollisionAreaData.BackZPos - _myCollisionAreaData.AreaWidth);
		//	Debug.LogError("後ろ");
		//}
	}

	private int SelectDirectionIndex(Vector3 moveVector)
	{
		float xAbs = Mathf.Abs(moveVector.x);
		float yAbs = Mathf.Abs(moveVector.y);
		float zAbs = Mathf.Abs(moveVector.z);
		if (xAbs >= yAbs)
		{
			if (xAbs >= zAbs)
			{
				if (Mathf.Sign(moveVector.x) > 0)
				{
					return _leftCollisionAreaDataIndex;
				}
				else
				{
					return _rightCollisionAreaDataIndex;
				}
			}
			else
			{
				if (Mathf.Sign(moveVector.z) > 0)
				{
					return _backCollisionAreaDataIndex;
				}
				else
				{
					return _forwardCollisionAreaDataIndex;
				}
			}
		}
		else if (yAbs >= zAbs)
		{
			if (Mathf.Sign(moveVector.y) > 0)
			{
				return _bottomCollisionIndex;
			}
			else
			{
				return _topCollisionAreaDataIndex;
			}
		}
		else
		{
			if (Mathf.Sign(moveVector.z) > 0)
			{
				return _backCollisionAreaDataIndex;
			}
			else
			{
				return _forwardCollisionAreaDataIndex;
			}
		}
	}

	protected virtual void GravityFall()
	{
		SearchHitObjects();
		if (_bottomCollisionIndex < 0)
		{
			ObjectMove(Vector3.down * (_objectManagerScript.GravityPower * Time.deltaTime));
			isGround = false;
		}
		else
		{
			isGround = true;
		}
	}


	protected virtual void OnDrawGizmos()
	{
		if (isDebugColliderVisible)
		{
			_matrixTemp = Gizmos.matrix;
			Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
		}
	}
}
