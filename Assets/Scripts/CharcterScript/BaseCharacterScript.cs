using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
/// <summary>
/// キャラクター共通の動き
/// </summary>
public class BaseCharacterScript : BaseObjectScript
{
	[SerializeField]
	protected bool isDeath = default;
	protected ICharacterStateMachine _myStateMachine = default;
	protected Vector3 _beforePos = default;
	[SerializeField]
	protected CharcterStatus _myCharcterStatus = default;
	protected int _myJumpCount = default;
	protected Animator _myAnimator = default;
	protected int _isGroundHashValue = default;
	[SerializeField,Header("デバッグ用")]
	protected BaseWeaponScript _myWeapon = default;
	
	[SerializeField]
	protected bool isInputTowards = default;

	[SerializeField, Tooltip("デバッグ用")]
	protected bool isDebugInputPlayer = default;
	[SerializeField]
	protected bool canCollision = true;
	

	public BaseWeaponScript MyWeapon { get { return _myWeapon; } }
	public bool IsGravity { get { return isGravity; } set { isGravity = value; } }
	public Animator MyAnimator { get { return _myAnimator; } }
	public CharcterStatus MyCharcterStatus { get { return _myCharcterStatus; } }
	public bool IsDeath { get { return isDeath; } }
	public bool IsDebugInputPlayer { get { return isDebugInputPlayer; } }
	public bool IsInputTowards { get { return isInputTowards; } }
	public bool CanCollision { get { return canCollision; } set { canCollision = value; } }
	public ObjectManagerScript ObjectManagerScript { get { return _objectManagerScript; } }

	public override void Init()
	{
		base.Init();
		_isGroundHashValue = Animator.StringToHash("IsGround");
		_myWeapon = _objectManagerScript.GetMyWeapon(this);
		if (!TryGetComponent<Animator>(out _myAnimator))
		{
			ErrorManagerScript.MyInstance.NullCompornentError("Animator");
		}
		_beforePos = MyTransform.position;
		_myCharcterStatus.Init();
	}
	public override void ObjectUpdate()
	{
		base.ObjectUpdate();

		_myAnimator.SetBool(_isGroundHashValue, isGround);
		_myStateMachine.UpdateState().Execute();
		ClampPos();
		_beforePos = _myTransform.position;
		//if(_forwardCollisionAreaDataIndex >= 0 && _myCollisionObjects[_forwardCollisionAreaDataIndex].name == "Floor")
		//{
		//	Debug.LogError(_myCollisionObjects[_forwardCollisionAreaDataIndex]);
		//}
	}

	public void ClampPos()
	{
		SearchHitObjects();

		_myCollisionAreaData.MyTransform.position
			= GetClampVector(_myCollisionAreaData.MyTransform.position - _beforePos);
	}
	public override void ObjectMove(Vector3 vector)
	{
		_myTransform.position += vector;
	}
	protected override void Reset()
	{
		base.Reset();
		if (gameObject.GetComponent<Animator>() == null)
		{
			gameObject.AddComponent<Animator>();
		}
	}
	protected override void GravityFall()
	{
		base.GravityFall();
		if (_myCollisionObjects.Count <= 0) { return; }
		if (_myCollisionObjects[0] is StageFloorScript floorScriptTemp)
		{
			floorScriptTemp.OnTopCharcter(this);
		}
	}

	public virtual void HealHP(float healValue)
	{
		_myCharcterStatus.Hp.Value += healValue;
	}


	public virtual void ReceiveDamage(float damage, float staggerThreshold)
	{
		if (_myCharcterStatus.Hp.Value <= damage)
		{
			_myCharcterStatus.Hp.Value = 0;
			isDeath = true;
			return;
		}
		else
		{
			_myCharcterStatus.Hp.Value -= damage;
		}

		_myCharcterStatus.StaggerThreshold.Value -= staggerThreshold;
	}

	public override Vector3 GetClampVector(Vector3 moveVector)
	{
		Vector3 returnValue = base.GetClampVector(moveVector);
		if (isInputTowards)
		{
			GetColObjects(MoveDirection.Forward);
			//正面に何か当たっていない場合
			if (_forwardCollisionAreaDataIndex < 0)
			{
				//Debug.Log(returnValue);
				return returnValue;
			}
			//斜め入力
			else if (moveVector.x != 0 && moveVector.z != 0)
			{
				//Debug.LogError("斜め");
				//キャラクターにぶつかっている場合は移動できなくする
				if (_objectManagerScript.IsCollisionCharcter(
					_myCollisionObjects[_forwardCollisionAreaDataIndex] as BaseCharacterScript))
				{
					return returnValue - moveVector;
				}

				//当たったオブジェクトのどこが当たっているかを確認する
				if (_objectManagerScript.IsCollisionObject(
					_myCollisionObjects[_forwardCollisionAreaDataIndex].MyCollisionAreaData
					, this, MoveDirection.Right))
				{
					returnValue += Vector3.right * (_myCollisionObjects[_forwardCollisionAreaDataIndex]
					.MyCollisionAreaData.RightXPos - _myCollisionAreaData.LeftXPos
					+ _myCollisionAreaData.AreaWidth);
					Debug.LogWarning("右：斜め" + _myCollisionObjects[_forwardCollisionAreaDataIndex]);
				}
				else if (_objectManagerScript.IsCollisionObject(
					_myCollisionObjects[_forwardCollisionAreaDataIndex].MyCollisionAreaData
					, this, MoveDirection.Left))
				{
					returnValue += Vector3.right * (_myCollisionObjects[_forwardCollisionAreaDataIndex]
					.MyCollisionAreaData.LeftXPos - _myCollisionAreaData.RightXPos
					+ _myCollisionAreaData.AreaWidth);
					Debug.LogWarning("左：斜め");
				}
				else if (_objectManagerScript.IsCollisionObject(
					_myCollisionObjects[_forwardCollisionAreaDataIndex].MyCollisionAreaData
					, this, MoveDirection.Forward))
				{
					returnValue += Vector3.forward * (_myCollisionObjects[_forwardCollisionAreaDataIndex]
					.MyCollisionAreaData.ForwardZPos - _myCollisionAreaData.BackZPos
					- _myCollisionAreaData.AreaWidth);
					Debug.LogWarning("前：斜め");
				}
				else if (_objectManagerScript.IsCollisionObject(
					_myCollisionObjects[_forwardCollisionAreaDataIndex].MyCollisionAreaData
					, this, MoveDirection.Back))
				{
					returnValue += Vector3.forward * (_myCollisionObjects[_forwardCollisionAreaDataIndex]
					.MyCollisionAreaData.BackZPos - _myCollisionAreaData.ForwardZPos
					- _myCollisionAreaData.AreaWidth);
					Debug.LogWarning("後ろ：斜め");
				}
			}
			//動いた方向が右
			else if (moveVector.x > 0)
			{
				//Debug.LogWarning("右");
				returnValue += Vector3.right * (_myCollisionObjects[_forwardCollisionAreaDataIndex]
					.MyCollisionAreaData.LeftXPos - _myCollisionAreaData.RightXPos
					+ _myCollisionAreaData.AreaWidth);
			}
			//動いた方向が左
			else if (moveVector.x < 0)
			{
				Debug.LogWarning("左：" + _myCollisionObjects[_forwardCollisionAreaDataIndex]);
				returnValue += Vector3.right * (_myCollisionObjects[_forwardCollisionAreaDataIndex]
					.MyCollisionAreaData.RightXPos - _myCollisionAreaData.LeftXPos
					+ _myCollisionAreaData.AreaWidth);
			}
			//動いた方向が前
			else if (moveVector.z > 0)
			{
				Debug.LogWarning("前");
				returnValue += Vector3.forward * (_myCollisionObjects[_forwardCollisionAreaDataIndex]
					.MyCollisionAreaData.BackZPos - _myCollisionAreaData.ForwardZPos
					- _myCollisionAreaData.AreaWidth);
			}
			//動いた方向が後ろ
			else if (moveVector.z < 0)
			{
				//Debug.LogWarning("後ろ");
				returnValue += Vector3.forward * (_myCollisionObjects[_forwardCollisionAreaDataIndex]
					.MyCollisionAreaData.ForwardZPos - _myCollisionAreaData.BackZPos
					- _myCollisionAreaData.AreaWidth);
			}
		}
		//Debug.Log(returnValue);
		if((returnValue - _beforePos).magnitude > 3)
		{
			returnValue = _beforePos;
			Debug.LogError("補正");
		}
		return returnValue;

	}

	public override void Delete()
	{
		base.Delete();
		if (_myStateMachine is not null)
		{
			_myStateMachine.Delete();
			_myStateMachine = null;
		}
		_myWeapon = null;
		_myAnimator = null;
	}
	protected override void OnDrawGizmos()
	{
		base.OnDrawGizmos();
		if (isDebugColliderVisible)
		{
			//自分の当たり判定
			//上
			Gizmos.DrawWireCube(_myCollisionAreaData.Offset
				+ Vector3.up * (_myCollisionAreaData.HalfAreaSize.y + _myCollisionAreaData.HalfAreaWidth)
				, Vector3.right * _myCollisionAreaData.HalfAreaSize.x * 2
				+ Vector3.up * _myCollisionAreaData.AreaWidth
				+ Vector3.forward * _myCollisionAreaData.HalfAreaSize.z * 2);
			//下
			Gizmos.DrawWireCube(_myCollisionAreaData.Offset
				+ Vector3.down * (_myCollisionAreaData.HalfAreaSize.y + _myCollisionAreaData.HalfAreaWidth)
				, Vector3.right * _myCollisionAreaData.HalfAreaSize.x * 2
				+ Vector3.up * _myCollisionAreaData.AreaWidth
				+ Vector3.forward * _myCollisionAreaData.HalfAreaSize.z * 2);
			//右
			Gizmos.DrawWireCube(_myCollisionAreaData.Offset
				+ Vector3.right * (_myCollisionAreaData.HalfAreaSize.x + _myCollisionAreaData.HalfAreaWidth)
				, Vector3.right * _myCollisionAreaData.AreaWidth
				+ Vector3.up * _myCollisionAreaData.HalfAreaSize.y * 2
				+ Vector3.forward * _myCollisionAreaData.HalfAreaSize.z * 2);
			//左
			Gizmos.DrawWireCube(_myCollisionAreaData.Offset
				 + Vector3.left * (_myCollisionAreaData.HalfAreaSize.x + _myCollisionAreaData.HalfAreaWidth)
				, Vector3.right * _myCollisionAreaData.AreaWidth
				+ Vector3.up * _myCollisionAreaData.HalfAreaSize.y * 2
				+ Vector3.forward * _myCollisionAreaData.HalfAreaSize.z * 2);
			//前
			Gizmos.DrawWireCube(_myCollisionAreaData.Offset
				+ Vector3.forward * (_myCollisionAreaData.HalfAreaSize.z + _myCollisionAreaData.HalfAreaWidth)
				, Vector3.right * _myCollisionAreaData.HalfAreaSize.x * 2
				+ Vector3.up * _myCollisionAreaData.HalfAreaSize.y * 2
				+ Vector3.forward * _myCollisionAreaData.AreaWidth);
			//後
			Gizmos.DrawWireCube(_myCollisionAreaData.Offset
				+ Vector3.back * (_myCollisionAreaData.HalfAreaSize.z + _myCollisionAreaData.HalfAreaWidth)
				, Vector3.right * _myCollisionAreaData.HalfAreaSize.x * 2
				+ Vector3.up * _myCollisionAreaData.HalfAreaSize.y * 2
				+ Vector3.forward * _myCollisionAreaData.AreaWidth);
			Gizmos.matrix = _matrixTemp;
		}
	}


}
