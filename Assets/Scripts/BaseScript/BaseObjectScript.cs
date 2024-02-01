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
		MoveClamp(_myCollisionAreaData.MyTransform.position - beforPos,beforPos);
	}
	protected virtual void SearchHitObjects()
	{
		CollisionIndexInit();
		_myCollisionObjects.Clear();
		_objectManagerScript.GetCollisionAllObject(_myCollisionAreaData, _myCollisionObjects);
		SelectColObjectResult();
	}
	private void MoveClamp(Vector3 moveDirection, Vector3 beforPos)
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
		//前
		if (_forwardCollisionAreaDataIndex >= 0)
		{
			_myCollisionAreaData.MyTransform.position = beforPos;
			//_myCollisionAreaData.MyTransform.position = test();
		}
	}
	public Vector3 test(CollisionAreaData me, CollisionAreaData target)
	{
		Vector3 c = me.MyTransform.position - target.MyTransform.position;
		float xAbs = Mathf.Abs(c.x);
		float yAbs = Mathf.Abs(c.y);
		float zAbs = Mathf.Abs(c.z);
		float myHitBoxForward = me.ForwardZPos + me.AreaWidth;
		if (xAbs >= yAbs)
		{
			if (xAbs >= zAbs)
			{
				if (Mathf.Sign(c.x) > 0)
				{
					Debug.Log("left");
					return -target.MyTransform.right * (target.LeftXPos - myHitBoxForward);
				}
				else
				{
					Debug.Log("right");
					return target.MyTransform.right * (target.RightXPos - me.ForwardZPos + me.AreaWidth);
				}
			}
			else
			{
				if (Mathf.Sign(c.z) > 0)
				{
					Debug.Log("forward");
					return target.MyTransform.forward * (target.ForwardZPos - me.ForwardZPos + me.AreaWidth);
				}
				else
				{
					Debug.Log("back");
					return -target.MyTransform.forward * (target.BackZPos - me.ForwardZPos + me.AreaWidth);
				}
			}
		}
		return Vector3.zero;
		//else if (yAbs >= zAbs)
		//{
		//	if (Mathf.Sign(c.y) > 0)
		//	{
		//		return TopYPos;
		//	}
		//	else
		//	{
		//		return BottomYPos;
		//	}
		//}
		//else
		//{
		//	if (Mathf.Sign(c.z) > 0)
		//	{
		//		Debug.Log("forward2");
		//		return ForwardZPos;
		//	}
		//	else
		//	{
		//		Debug.Log("back2");
		//		return BackZPos;
		//	}
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
