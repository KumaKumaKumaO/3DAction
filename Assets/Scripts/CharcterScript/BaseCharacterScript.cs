using UnityEngine;

[RequireComponent(typeof(Animator))]
/// <summary>
/// キャラクター共通の動き
/// </summary>
public class BaseCharacterScript : BaseObjectScript
{
	[SerializeField,Header("ステータス")]
	protected CharcterStatus _myCharcterStatus = default;
	[SerializeField, Header("デバッグ用")]
	protected BaseWeaponScript _myWeapon = default;
	protected ICharacterStateMachine _myStateMachine = default;
	protected Animator _myAnimator = default;
	protected BaseCharacterScript _lockTarget = default;

	[SerializeField,Tooltip("当たり判定があるか")]
	protected bool canCollision = true;
	[SerializeField,Tooltip("動作状態にあるか")]
	protected bool isActive = default;
	[SerializeField,Tooltip("ボスか")]
	protected bool isBoss = default;
	[SerializeField,Tooltip("向いている方向に対して入力が反応するか")]
	protected bool isInputTowards = default;
	[SerializeField,Tooltip("死んでいるか")]
	protected bool isDeath = default;
#if UNITY_EDITOR
	[SerializeField, Tooltip("プレイヤー入力で操作できるようにする（プレイヤーがいない場合）")]
	protected bool isDebugInputPlayer = default;
#endif
	protected float _lockDistance = default;
	protected int _isGroundHashValue = default;
	protected int _myJumpCount = default;
	protected Vector3 _beforePos = default;

	public BaseCharacterScript LockTarget { get { return _lockTarget; } }
	public BaseWeaponScript MyWeapon { get { return _myWeapon; } }
	public CharcterStatus MyCharcterStatus { get { return _myCharcterStatus; } }
	public bool IsDeath { get { return isDeath; } }
#if UNITY_EDITOR
	public bool IsDebugInputPlayer { get { return isDebugInputPlayer; } }
#endif
	public bool IsInputTowards { get { return isInputTowards; } }
	public bool CanCollision { get { return canCollision; } set { canCollision = value; } }
	public bool IsBoss { get { return isBoss; } }
	public bool IsActive { get { return isActive; } }

	public override void Init()
	{
		base.Init();
		_isGroundHashValue = Animator.StringToHash("IsGround");
		_myWeapon = _objectManagerScript.GetMyWeapon(this);
		_myAnimator = GetComponent<Animator>();

#if UNITY_EDITOR
		if (_myAnimator is null)
		{
			ErrorManagerScript.MyInstance.NullCompornentError("Animator");
		}
#endif
		_beforePos = MyTransform.position;
		_myCharcterStatus.Init();
	}

	public override void ObjectUpdate()
	{
		base.ObjectUpdate();
		SettingLockTarget();
		_myAnimator.SetBool(_isGroundHashValue, isGround);
		_myStateMachine.UpdateState().Execute();
		ClampPos();
		_beforePos = _myTransform.position;
	}

	/// <summary>
	/// ターゲットを設定する
	/// </summary>
	private void SettingLockTarget()
	{
		BaseCharacterScript lockTargetTemp = _objectManagerScript.GetNearCharcter(_myTransform);
		if(lockTargetTemp is null) { return; }
		if ((_myTransform.position - lockTargetTemp.MyTransform.position).magnitude <= _lockDistance)
		{
			_lockTarget = lockTargetTemp;
		}
	}

	/// <summary>
	/// めり込み制御
	/// </summary>
	public void ClampPos()
	{
		CollisionIndexInit();
		_myCollisionObjects.Clear();

		_myCollisionAreaData.MyTransform.position
			= GetClampVector(_myCollisionAreaData.MyTransform.position - _beforePos);
	}

	public override void ObjectMove(Vector3 vector)
	{
		_myTransform.position += vector;
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

	/// <summary>
	/// ダメージを受ける
	/// </summary>
	/// <param name="damage">与えられたダメージ</param>
	/// <param name="staggerThreshold">怯み値</param>
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
					//Debug.LogWarning("右：斜め" + _myCollisionObjects[_forwardCollisionAreaDataIndex]);
				}
				else if (_objectManagerScript.IsCollisionObject(
					_myCollisionObjects[_forwardCollisionAreaDataIndex].MyCollisionAreaData
					, this, MoveDirection.Left))
				{
					returnValue += Vector3.right * (_myCollisionObjects[_forwardCollisionAreaDataIndex]
					.MyCollisionAreaData.LeftXPos - _myCollisionAreaData.RightXPos
					+ _myCollisionAreaData.AreaWidth);
					//Debug.LogWarning("左：斜め");
				}
				else if (_objectManagerScript.IsCollisionObject(
					_myCollisionObjects[_forwardCollisionAreaDataIndex].MyCollisionAreaData
					, this, MoveDirection.Forward))
				{
					returnValue += Vector3.forward * (_myCollisionObjects[_forwardCollisionAreaDataIndex]
					.MyCollisionAreaData.ForwardZPos - _myCollisionAreaData.BackZPos
					- _myCollisionAreaData.AreaWidth);
					//Debug.LogWarning("前：斜め");
				}
				else if (_objectManagerScript.IsCollisionObject(
					_myCollisionObjects[_forwardCollisionAreaDataIndex].MyCollisionAreaData
					, this, MoveDirection.Back))
				{
					returnValue += Vector3.forward * (_myCollisionObjects[_forwardCollisionAreaDataIndex]
					.MyCollisionAreaData.BackZPos - _myCollisionAreaData.ForwardZPos
					- _myCollisionAreaData.AreaWidth);
					//Debug.LogWarning("後ろ：斜め");
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
				//Debug.LogWarning("左：" + _myCollisionObjects[_forwardCollisionAreaDataIndex]);
				returnValue += Vector3.right * (_myCollisionObjects[_forwardCollisionAreaDataIndex]
					.MyCollisionAreaData.RightXPos - _myCollisionAreaData.LeftXPos
					+ _myCollisionAreaData.AreaWidth);
			}
			//動いた方向が前
			else if (moveVector.z > 0)
			{
				//Debug.LogWarning("前");
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

#if UNITY_EDITOR
	protected override void OnDrawGizmos()
	{
		base.OnDrawGizmos();
		if (isDebugColliderVisible)
		{
			if (canCollision)
			{
				Gizmos.color = Color.white;
			}
			else
			{
				Gizmos.color = Color.green;
			}
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
#endif

}
