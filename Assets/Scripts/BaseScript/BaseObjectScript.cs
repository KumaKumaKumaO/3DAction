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
	protected List<BaseObjectScript> _myCollisionObjects = new List<BaseObjectScript>();
	protected int _bottomCollisionIndex = -1;
	protected int _topCollisionAreaDataIndex = -1;
	protected int _rightCollisionAreaDataIndex = -1;
	protected int _leftCollisionAreaDataIndex = -1;
	protected int _forwardCollisionAreaDataIndex = -1;
	protected int _backCollisionAreaDataIndex = -1;
	protected ObjectManagerScript _objectManagerScript = default;
	protected Matrix4x4 _matrixTemp = default;
	[SerializeField]
	protected bool isGround = false;
	protected Transform _myTransform = default;
	protected int _forwardCollisionCount = 0;
	private bool isDestroyObject = false;


	[SerializeField,Tooltip("デバッグ用")]
	protected bool isDebugColliderVisible = false;
	public CollisionAreaData MyCollisionAreaData { get { return _myCollisionAreaData; } }
	public Transform MyTransform { get { return _myTransform; } }
	public bool IsGround { get { return isGround; } }
	public bool IsDestroyObject { get { return isDestroyObject; } }
	public virtual void Init()
	{
		_myTransform = this.transform;
		GameObject objectManagerObject = GameObject.FindWithTag("ObjectManager");
#if UNITY_EDITOR
		if (objectManagerObject == null)
		{
			ErrorManagerScript.MyInstance.NullGameObjectError("ObjectManagerのタグがついたオブジェクト");
		}
#endif
		_objectManagerScript = objectManagerObject.GetComponent<ObjectManagerScript>();
#if UNITY_EDITOR
		if (!objectManagerObject.TryGetComponent<ObjectManagerScript>(out _objectManagerScript))
		{
			ErrorManagerScript.MyInstance.NullScriptError("ObjectManagerScript");
		}
#endif
		_myCollisionAreaData.Init(transform);
	}
	public virtual void ObjectUpdate()
	{
		SearchHitObjects();
		if (isGravity)
		{
			GravityFall();
		}

	}

	//今の向きに対して
	protected void GetColObjects(MoveDirection moveDirection)
	{
		_objectManagerScript.GetCollisionAllObject(_myCollisionAreaData,_myCollisionObjects,moveDirection);
		for (int i = 0; i < _myCollisionObjects.Count; i++)
		{
			if(moveDirection == MoveDirection.Down)
			{
				if (_bottomCollisionIndex < 0
					|| !(_myCollisionObjects[_bottomCollisionIndex].MyCollisionAreaData.TopYPos
					< _myCollisionObjects[i].MyCollisionAreaData.TopYPos))
				{
					_bottomCollisionIndex = i;
				}
			}
			else if(moveDirection == MoveDirection.Up)
			{
				if (_topCollisionAreaDataIndex < 0
					|| _myCollisionObjects[_topCollisionAreaDataIndex].MyCollisionAreaData.BottomYPos
					> _myCollisionObjects[i].MyCollisionAreaData.BottomYPos)
				{
					_topCollisionAreaDataIndex = i;
				}
			}
			else if(moveDirection == MoveDirection.Right)
			{
				if (_rightCollisionAreaDataIndex < 0
					|| _myCollisionObjects[_rightCollisionAreaDataIndex].MyCollisionAreaData.LeftXPos
					> _myCollisionObjects[i].MyCollisionAreaData.LeftXPos)
				{
					_rightCollisionAreaDataIndex = i;
				}
			}
			else if(moveDirection == MoveDirection.Left)
			{
				if (_leftCollisionAreaDataIndex < 0
					|| _myCollisionObjects[_leftCollisionAreaDataIndex].MyCollisionAreaData.RightXPos
					< _myCollisionObjects[i].MyCollisionAreaData.RightXPos)
				{
					_leftCollisionAreaDataIndex = i;
				}
			}
			else if(moveDirection == MoveDirection.Forward)
			{
				if (_forwardCollisionAreaDataIndex < 0
					|| _myCollisionObjects[_forwardCollisionAreaDataIndex].MyCollisionAreaData.BackZPos
					> _myCollisionObjects[i].MyCollisionAreaData.BackZPos)
				{
					_forwardCollisionAreaDataIndex = i;
				}
			}
			else if(moveDirection == MoveDirection.Back)
			{
				if (_backCollisionAreaDataIndex < 0
					|| _myCollisionObjects[_backCollisionAreaDataIndex].MyCollisionAreaData.ForwardZPos
					< _myCollisionObjects[i].MyCollisionAreaData.ForwardZPos)
				{
					_backCollisionAreaDataIndex = i;
				}
			}
		}
	}

	/// <summary>
	/// 倒す演出などが終わったあとの処理
	/// </summary>
	public virtual void Delete()
	{
		if(_myCollisionObjects is not null)
		{
			_myCollisionObjects.Clear();
			_myCollisionObjects = null;
		}
		if(_objectManagerScript is not null)
		{
			_objectManagerScript.SubtractObject(this);
			_objectManagerScript = null;
		}
		if(_myTransform is not null)
		{
			_myTransform = null;
			_myCollisionAreaData.Delete();
		}

		if(isDestroyObject) { return; }
		Destroy(gameObject);
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
		_forwardCollisionCount = 0;
	}
	/// <summary>
	/// オブジェクトを移動する
	/// </summary>
	/// <param name="vector">世界軸の移動ベクトル</param>
	public virtual void ObjectMove(Vector3 vector)
	{
		Vector3 beforPos = _myCollisionAreaData.MyTransform.position;
		_myCollisionAreaData.MyTransform.position += vector;
		SearchHitObjects();

		_myCollisionAreaData.MyTransform.position
			= GetClampVector(_myCollisionAreaData.MyTransform.position - beforPos);
	}
	protected virtual void SearchHitObjects()
	{
		CollisionIndexInit();
		_myCollisionObjects.Clear();
	}

	/// <summary>
	/// すり抜けが修正する
	/// </summary>
	/// <param name="moveVector">移動したベクトル</param>
	/// <returns>修正後のポジション</returns>
	public virtual Vector3 GetClampVector(Vector3 moveVector)
	{
		//移動後の位置を代入
		Vector3 returnValue = _myTransform.position;

		//下に移動している場合
		if (moveVector.y < 0)
		{
			GetColObjects(MoveDirection.Down);

			if (_bottomCollisionIndex >= 0)
			{
				returnValue += Vector3.up * (_myCollisionObjects[_bottomCollisionIndex]
					.MyCollisionAreaData.TopYPos - _myCollisionAreaData.BottomYPos
					+ _myCollisionAreaData.AreaWidth);
			}
		}
		//上に移動している場合
		else if (moveVector.y > 0)
		{
			GetColObjects(MoveDirection.Up);
			if (_forwardCollisionAreaDataIndex >= 0)
			{
				returnValue += Vector3.up * (_myCollisionObjects[_topCollisionAreaDataIndex]
					.MyCollisionAreaData.BottomYPos - _myCollisionAreaData.TopYPos
					+ _myCollisionAreaData.AreaWidth);
			}
		}
		return returnValue;
	}

	protected virtual void GravityFall()
	{
		GetColObjects(MoveDirection.Down);
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
#if UNITY_EDITOR
	protected virtual void OnDrawGizmos()
	{
		if (isDebugColliderVisible)
		{
			_matrixTemp = Gizmos.matrix;
			Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
		}
	}
#endif

	private void OnDestroy()
	{
		isDestroyObject = true;
		//Debug.LogWarning("Destroy:" + gameObject.name);
	}
}
