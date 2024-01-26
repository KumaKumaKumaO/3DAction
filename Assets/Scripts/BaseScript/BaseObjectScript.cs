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
	protected Vector3 _beforePos = default;

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
		_beforePos = _myCollisionAreaData.MyTransform.position;
	}
	private void CollisionIndexInit()
	{
		_bottomCollisionIndex = -1;
		_topCollisionAreaDataIndex = -1;
		_rightCollisionAreaDataIndex = -1;
		_leftCollisionAreaDataIndex = -1;
		_forwardCollisionAreaDataIndex = -1;
		_backCollisionAreaDataIndex = -1;
	}
	private void Reset()
	{
		gameObject.tag = "Object";
	}

	public virtual void ObjectUpdate()
	{


		if (_myCollisionAreaData.IsCollision)
		{
			_objectManagerScript.GetCollisionObject(_myCollisionAreaData, _myCollisionObjects);
			CollisionIndexInit();
			SelectColObjectResult();
			if (isGravity)
			{
				GravityFall();
			}
			MoveClampCollision();
		}

		//側面が衝突しているか
		//上面が衝突しているか

	}

	protected virtual void MoveClampCollision()
	{
		
		
		if (!isGround)
		{
			if (_bottomCollisionIndex >= 0)
			{
				_myCollisionAreaData.MyTransform.position += Vector3.up *
					(_myCollisionObjects[_bottomCollisionIndex].CollisionObjectData.MyCollisionAreaData.TopYPos
					- _myCollisionAreaData.BottomYPos);
				isGround = true;
			}
		}
		if (_rightCollisionAreaDataIndex >= 0)
		{
			_myCollisionAreaData.MyTransform.position += Vector3.right *
				(_myCollisionObjects[_rightCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData.LeftXPos - _myCollisionAreaData.RightXPos);
		}


	}
	protected virtual void GravityFall()
	{
		hitvalue = _myCollisionObjects.Count;
		if (_bottomCollisionIndex < 0)
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
					|| _myCollisionObjects[_bottomCollisionIndex].CollisionObjectData.MyCollisionAreaData.BottomYPos
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

	private void OnDrawGizmos()
	{
		if (isDebugColliderVisible)
		{
			Gizmos.DrawWireCube(transform.position + _myCollisionAreaData.Offset, _myCollisionAreaData.HalfAreaSize * 2);
		}
	}
}
