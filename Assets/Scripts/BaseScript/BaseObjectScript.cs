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
					|| !(_myCollisionObjects[_bottomCollisionIndex].ObjectData.MyCollisionAreaData.TopYPos
					< _myCollisionObjects[i].ObjectData.MyCollisionAreaData.TopYPos))
				{
					_bottomCollisionIndex = i;
				}
			}
			if (_myCollisionObjects[i].IsCollisionTop)
			{
				if (_topCollisionAreaDataIndex < 0
					|| _myCollisionObjects[_topCollisionAreaDataIndex].ObjectData.MyCollisionAreaData.BottomYPos
					> _myCollisionObjects[i].ObjectData.MyCollisionAreaData.BottomYPos)
				{
					_topCollisionAreaDataIndex = i;
				}
			}
			if (_myCollisionObjects[i].IsCollisionRight)
			{
				if (_rightCollisionAreaDataIndex < 0
					|| _myCollisionObjects[_rightCollisionAreaDataIndex].ObjectData.MyCollisionAreaData.LeftXPos
					> _myCollisionObjects[i].ObjectData.MyCollisionAreaData.LeftXPos)
				{
					_rightCollisionAreaDataIndex = i;
				}
			}
			if (_myCollisionObjects[i].IsCollisionLeft)
			{
				if (_leftCollisionAreaDataIndex < 0
					|| _myCollisionObjects[_leftCollisionAreaDataIndex].ObjectData.MyCollisionAreaData.RightXPos
					< _myCollisionObjects[i].ObjectData.MyCollisionAreaData.RightXPos)
				{
					_leftCollisionAreaDataIndex = i;
				}
			}
			//ものから見て前側にヒットしている
			if (_myCollisionObjects[i].IsCollisionForward)
			{
				if (_forwardCollisionAreaDataIndex < 0
					//
					|| _myCollisionObjects[_forwardCollisionAreaDataIndex].ObjectData.MyCollisionAreaData.BackZPos
					> _myCollisionObjects[i].ObjectData.MyCollisionAreaData.BackZPos)
				{
					_forwardCollisionAreaDataIndex = i;
				}
			}
			else if (_myCollisionObjects[i].IsCollisionBack)
			{
				if (_backCollisionAreaDataIndex < 0
					|| _myCollisionObjects[_backCollisionAreaDataIndex].ObjectData.MyCollisionAreaData.ForwardZPos
					< _myCollisionObjects[i].ObjectData.MyCollisionAreaData.ForwardZPos)
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
		Debug.Log("pos:" + _myCollisionAreaData.MyTransform.position + ":" + beforPos + "vector:" + vector);

		_myCollisionAreaData.MyTransform.position = GetClampPos(_myCollisionAreaData
			, (_myCollisionAreaData.MyTransform.position - beforPos).normalized);

	}
	protected virtual void SearchHitObjects()
	{
		CollisionIndexInit();
		_myCollisionObjects.Clear();
		_objectManagerScript.GetCollisionAllObject(_myCollisionAreaData, _myCollisionObjects);
		SelectColObjectResult();
	}
	[ContextMenu("test")]
	public void MsgOut()
	{
		if (_forwardCollisionAreaDataIndex >= 0)
		{
			Debug.Log
			(_myCollisionObjects[_forwardCollisionAreaDataIndex].ObjectData.MyCollisionAreaData.MyTransform.name);
		}
	}
	public Vector3 GetClampPos(CollisionAreaData myData, Vector3 moveDir)
	{
		Vector3 returnValue = Vector3.zero;
		if (moveDir.x != 0)
		{
			//動いた方向が右
			if (moveDir.x > 0)
			{
				if (_rightCollisionAreaDataIndex >= 0)
				{
					returnValue += Vector3.right * (_myCollisionObjects[_rightCollisionAreaDataIndex]
						.ObjectData.MyCollisionAreaData.LeftXPos - myData.RightXPos - myData.AreaWidth);
					_myCollisionObjects.RemoveAt(_rightCollisionAreaDataIndex);
					SearchHitObjects();
				}
			}
			else
			{
				if (_leftCollisionAreaDataIndex >= 0)
				{
					returnValue += Vector3.right * (_myCollisionObjects[_leftCollisionAreaDataIndex]
						.ObjectData.MyCollisionAreaData.RightXPos - myData.LeftXPos + myData.AreaWidth);
					_myCollisionObjects.RemoveAt(_leftCollisionAreaDataIndex);
					SearchHitObjects();
				}
			}
			
		}
		if (moveDir.z != 0)
		{
			if (moveDir.z > 0)
			{
				if (_forwardCollisionAreaDataIndex >= 0)
				{
					returnValue += Vector3.forward * (_myCollisionObjects[_forwardCollisionAreaDataIndex]
						.ObjectData.MyCollisionAreaData.BackZPos - myData.ForwardZPos - myData.AreaWidth);
					_myCollisionObjects.RemoveAt(_forwardCollisionAreaDataIndex);
					SearchHitObjects();
				}
			}
			else
			{
				if (_backCollisionAreaDataIndex >= 0)
				{
					returnValue += Vector3.forward * (_myCollisionObjects[_backCollisionAreaDataIndex]
						.ObjectData.MyCollisionAreaData.ForwardZPos - myData.BackZPos - myData.AreaWidth);
					_myCollisionObjects.RemoveAt(_backCollisionAreaDataIndex);
					SearchHitObjects();
				}
			}
		}
		if (moveDir.y != 0)
		{
			if (moveDir.y < 0)
			{
				if (_bottomCollisionIndex >= 0)
				{
					returnValue += Vector3.up * (_myCollisionObjects[_bottomCollisionIndex]
						.ObjectData.MyCollisionAreaData.TopYPos - myData.BottomYPos + myData.AreaWidth);
					_myCollisionObjects.RemoveAt(_bottomCollisionIndex);
					SearchHitObjects();
				}
			}
			else
			{
				if (_forwardCollisionAreaDataIndex >= 0)
				{
					returnValue += Vector3.up * (_myCollisionObjects[_topCollisionAreaDataIndex]
						.ObjectData.MyCollisionAreaData.BottomYPos - myData.TopYPos + myData.AreaWidth);
					_myCollisionObjects.RemoveAt(_topCollisionAreaDataIndex);
					SearchHitObjects();
				}
			}
		}
		returnValue += myData.MyTransform.position;

		Debug.Log("clamp" + returnValue);
		return returnValue;
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
