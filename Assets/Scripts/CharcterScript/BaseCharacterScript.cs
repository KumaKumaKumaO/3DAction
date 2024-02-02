using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラクターの操作や実際の動き
/// </summary>
public abstract class BaseCharacterScript : BaseObjectScript
{

	protected ICharacterStateMachine _myStateMachine = default;
	protected IInputCharcterAction _myInput = default;
	[SerializeField]
	protected CharcterStatus _myCharcterStatus = default;
	protected int _myJumpCount = default;
	protected Animator _myAnimator = default;
	protected int _isGroundHashValue = default;
	protected BaseWeaponScript _myWeapon = default;

	public BaseWeaponScript MyWeapon { get { return _myWeapon; } }
	public bool IsGravity { get { return isGravity; } set { isGravity = value; } }
	public Animator MyAnimator { get { return _myAnimator; } }
	public CharcterStatus MyCharcterStatus { get { return _myCharcterStatus; } }

	public override void Init()
	{
		base.Init();
		_isGroundHashValue = Animator.StringToHash("IsGround");
		_myWeapon = _objectManagerScript.GetMyWeapon(this);
	}
	public override void ObjectUpdate()
	{
		base.ObjectUpdate();

		_myAnimator.SetBool(_isGroundHashValue, isGround);
		_myStateMachine.UpdateState().Execute();
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
		if (_myCollisionObjects[0].CollisionObjectData is StageFloorScript floorScriptTemp)
		{
			floorScriptTemp.OnTopCharcter(this);
		}
	}

	public virtual void HealHP(float healValue)
	{
		_myCharcterStatus.Hp += healValue;
	}

	public virtual void ReceiveDamage(float damage)
	{
		_myCharcterStatus.Hp -= damage;
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
