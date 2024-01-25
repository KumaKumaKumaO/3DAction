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
	protected ObjectManagerScript _objectManagerScript = default;
	protected List<CollisionResultData> _myCollisionObjects = new List<CollisionResultData>();
	protected int _bottomCollisionIndex = -1;
	protected int _topCollisionAreaDataIndex = -1;
	protected int _rightCollisionAreaDataIndex = -1;
	protected int _leftCollisionAreaDataIndex = -1;
	protected int _forwardCollisionAreaDataIndex = -1;
	protected int _backCollisionAreaDataIndex = -1;
	[Header("デバッグ用")]
	[SerializeField]
	private bool isDebugColliderVisible = false;
	[SerializeField]
	protected bool isGround = false;

	[SerializeField]
	public CollisionResultData test;
	[SerializeField]
	float hitvalue;
	public CollisionAreaData MyCollisionAreaData { get { return _myCollisionAreaData; } }

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
		if (!isGravity)
		{
			isGround = true;
		}
	}
	private void CollisionIndexInit()
	{
		_bottomCollisionIndex = -1;
		_topCollisionAreaDataIndex = -1;
		_rightCollisionAreaDataIndex = -1;
		_leftCollisionAreaDataIndex = -1;
	}
	private void Reset()
	{
		gameObject.tag = "Object";
	}

	public virtual void ObjectUpdate()
	{
		

		if (_myCollisionAreaData.IsCollision)
		{
			if (isGravity)
			{
				_objectManagerScript.GetCollisionObject(_myCollisionAreaData, _myCollisionObjects);
				GravityFall();
			}
			MoveClampCollision();
		}
		
		//側面が衝突しているか
		//上面が衝突しているか

	}

	protected virtual void MoveClampCollision()
	{
		if(_myCollisionObjects.Count <= 0) { return; }
		CollisionIndexInit();
		SelectColObjectResult();
		if (!isGround)
		{
			if (_bottomCollisionIndex > 0)
			{
				_myCollisionAreaData.MyTransform.position += Vector3.up *
					(_myCollisionObjects[_bottomCollisionIndex].CollisionObjectData.MyCollisionAreaData.TopYPos
					- _myCollisionAreaData.BottomYPos);
				isGround = true;
			}
		}
		if(_rightCollisionAreaDataIndex > 0)
		{
			_myCollisionAreaData.MyTransform.position += Vector3.right *
				(_myCollisionObjects[_bottomCollisionIndex].CollisionObjectData.MyCollisionAreaData.LeftXPos - _myCollisionAreaData.RightXPos);
		}
		
		
	}
	protected virtual void GravityFall()
	{
		hitvalue = _myCollisionObjects.Count;
		if (_myCollisionObjects.Count <= 0)
		{
			_myCollisionAreaData.MyTransform.position -= Vector3.up * (_objectManagerScript.GravityPower * Time.deltaTime);
			isGround = false;
			return;
		}
	}

	private void SelectColObjectResult()
	{
		for (int i = 0; i < _myCollisionObjects.Count; i++)
		{
			if (_myCollisionObjects[i].IsCollisionBottom)
			{
				_bottomCollisionIndex = i;
				if (i == 0) { continue; }
				if (_myCollisionObjects[_bottomCollisionIndex].CollisionObjectData.MyCollisionAreaData.TopYPos
					< _myCollisionObjects[i].CollisionObjectData.MyCollisionAreaData.TopYPos)
				{
					_bottomCollisionIndex = i;
				}
			}
			if (_myCollisionObjects[i].IsCollisionTop)
			{
				_topCollisionAreaDataIndex = i;
				if(i == 0) { continue; }
				if(_myCollisionObjects[_bottomCollisionIndex].CollisionObjectData.MyCollisionAreaData.BottomYPos
					> _myCollisionObjects[i].CollisionObjectData.MyCollisionAreaData.BottomYPos)
				{
					_topCollisionAreaDataIndex = i;
				}
			}
			if (_myCollisionObjects[i].IsCollisionRight)
			{
				_rightCollisionAreaDataIndex = i;
				if (i == 0) { continue; }
				if (_myCollisionObjects[_rightCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData.LeftXPos
					> _myCollisionObjects[i].CollisionObjectData.MyCollisionAreaData.LeftXPos)
				{
					_rightCollisionAreaDataIndex = i;
				}
			}
			if (_myCollisionObjects[i].IsCollisionLeft)
			{
				_leftCollisionAreaDataIndex = i;
				if (i == 0) { continue; }
				if (_myCollisionObjects[_leftCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData.RightXPos
					< _myCollisionObjects[i].CollisionObjectData.MyCollisionAreaData.RightXPos)
				{
					_leftCollisionAreaDataIndex = i;
				}
			}
			if (_myCollisionObjects[i].IsCollisionForward)
			{
				_forwardCollisionAreaDataIndex = i;
				if (i == 0) { continue; }
				if (_myCollisionObjects[_forwardCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData.BackZPos
					> _myCollisionObjects[i].CollisionObjectData.MyCollisionAreaData.BackZPos)
				{
					_forwardCollisionAreaDataIndex = i;
				}
			}
			if (_myCollisionObjects[i].IsCollisionBack)
			{
				_backCollisionAreaDataIndex = i;
				if (i == 0) { continue; }
				if (_myCollisionObjects[_backCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData.ForwardZPos
					< _myCollisionObjects[i].CollisionObjectData.MyCollisionAreaData.ForwardZPos)
				{
					_backCollisionAreaDataIndex = i;
				}
			}
		}
	}

	private void OnDrawGizmos()
	{
		if (isDebugColliderVisible)
		{
			Gizmos.DrawWireCube(transform.position + _myCollisionAreaData.Offset, _myCollisionAreaData.HalfAreaSize * 2);
		}
	}
}
