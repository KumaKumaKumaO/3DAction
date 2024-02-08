using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �L�����N�^�[���ʂ̓���
/// </summary>
public class BaseCharacterScript : BaseObjectScript
{

	protected ICharacterStateMachine _myStateMachine = default;
	protected IInputCharcterAction _myInput = default;
	[SerializeField]
	protected CharcterStatus _myCharcterStatus = default;
	protected int _myJumpCount = default;
	protected Animator _myAnimator = default;
	protected int _isGroundHashValue = default;
	protected BaseWeaponScript _myWeapon = default;
	[SerializeField]
	protected float _staggerRecastTime = default;
	protected float _staggerRecastTimeTemp = default;

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
		if(_staggerRecastTimeTemp > 0)
		{
			_staggerRecastTimeTemp -= Time.deltaTime;
		}
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
		if (_myCollisionObjects[0].ObjectData is StageFloorScript floorScriptTemp)
		{
			floorScriptTemp.OnTopCharcter(this);
		}
	}

	public virtual void HealHP(float healValue)
	{
		_myCharcterStatus.Hp += healValue;
	}

	public virtual void ReceiveDamage(float damage,float staggerThreshold)
	{
		if(_myCharcterStatus.Hp < damage)
		{
			_myCharcterStatus.Hp = 0;
			return;
		}
		else
		{
			_myCharcterStatus.Hp -= damage;
		}

		if(_myCharcterStatus.StaggerThreshold < staggerThreshold)
		{
			_myCharcterStatus.StaggerThreshold = 0;
		}
		else
		{
			_myCharcterStatus.StaggerThreshold -= staggerThreshold;
		}
		_staggerRecastTimeTemp = _staggerRecastTime;
	}
	public override Vector3 GetClampPos(Vector3 moveVector)
	{
		Vector3 returnValue = base.GetClampPos(moveVector);
		//���ʂɉ����������Ă��邩
		if (_forwardCollisionAreaDataIndex < 0)
		{
			return returnValue;
		}
		//�΂ߓ���
		if (moveVector.x != 0 && moveVector.z != 0)
		{
			if (_objectManagerScript.IsCollisionCharcter(
				_myCollisionObjects[_forwardCollisionAreaDataIndex].ObjectData as BaseCharacterScript))
			{
				return returnValue - moveVector;
			}

			CollisionResultData resultData = _objectManagerScript.CollisionObject(
			   _myCollisionObjects[_forwardCollisionAreaDataIndex].ObjectData.MyCollisionAreaData, this);
			if (resultData.IsCollisionRight)
			{
				returnValue += Vector3.right * (_myCollisionObjects[_forwardCollisionAreaDataIndex]
				.ObjectData.MyCollisionAreaData.RightXPos - _myCollisionAreaData.LeftXPos
				+ _myCollisionAreaData.AreaWidth);
			}
			else if (resultData.IsCollisionLeft)
			{
				returnValue += Vector3.right * (_myCollisionObjects[_forwardCollisionAreaDataIndex]
				.ObjectData.MyCollisionAreaData.LeftXPos - _myCollisionAreaData.RightXPos
				+ _myCollisionAreaData.AreaWidth);
			}
			else if (resultData.IsCollisionForward)
			{
				returnValue += Vector3.forward * (_myCollisionObjects[_forwardCollisionAreaDataIndex]
				.ObjectData.MyCollisionAreaData.ForwardZPos - _myCollisionAreaData.BackZPos
				- _myCollisionAreaData.AreaWidth);
			}
			else
			{
				returnValue += Vector3.forward * (_myCollisionObjects[_forwardCollisionAreaDataIndex]
				.ObjectData.MyCollisionAreaData.BackZPos - _myCollisionAreaData.ForwardZPos
				- _myCollisionAreaData.AreaWidth);
			}
		}
		else if (moveVector.x != 0)
		{
			//�������������E
			if (moveVector.x > 0)
			{
				//Debug.LogWarning("�E");
				returnValue += Vector3.right * (_myCollisionObjects[_forwardCollisionAreaDataIndex]
					.ObjectData.MyCollisionAreaData.LeftXPos - _myCollisionAreaData.RightXPos
					+ _myCollisionAreaData.AreaWidth);
			}
			else
			{
				//Debug.LogWarning("��");
				returnValue += Vector3.right * (_myCollisionObjects[_forwardCollisionAreaDataIndex]
					.ObjectData.MyCollisionAreaData.RightXPos - _myCollisionAreaData.LeftXPos
					+ _myCollisionAreaData.AreaWidth);
			}
		}

		else if (moveVector.z > 0)
		{
			//Debug.LogWarning("�O");
			returnValue += Vector3.forward * (_myCollisionObjects[_forwardCollisionAreaDataIndex]
				.ObjectData.MyCollisionAreaData.BackZPos - _myCollisionAreaData.ForwardZPos
				- _myCollisionAreaData.AreaWidth);
		}
		else if (moveVector.z < 0)
		{
			//Debug.LogWarning("���");
			returnValue += Vector3.forward * (_myCollisionObjects[_forwardCollisionAreaDataIndex]
				.ObjectData.MyCollisionAreaData.ForwardZPos - _myCollisionAreaData.BackZPos
				- _myCollisionAreaData.AreaWidth);
		}
		return returnValue;
	}
	protected override void OnDrawGizmos()
	{
		base.OnDrawGizmos();
		if (isDebugColliderVisible)
		{
			//�����̓����蔻��
			//��
			Gizmos.DrawWireCube(_myCollisionAreaData.Offset
				+ Vector3.up * (_myCollisionAreaData.HalfAreaSize.y + _myCollisionAreaData.HalfAreaWidth)
				, Vector3.right * _myCollisionAreaData.HalfAreaSize.x * 2
				+ Vector3.up * _myCollisionAreaData.AreaWidth
				+ Vector3.forward * _myCollisionAreaData.HalfAreaSize.z * 2);
			//��
			Gizmos.DrawWireCube(_myCollisionAreaData.Offset
				+ Vector3.down * (_myCollisionAreaData.HalfAreaSize.y + _myCollisionAreaData.HalfAreaWidth)
				, Vector3.right * _myCollisionAreaData.HalfAreaSize.x * 2
				+ Vector3.up * _myCollisionAreaData.AreaWidth
				+ Vector3.forward * _myCollisionAreaData.HalfAreaSize.z * 2);
			//�E
			Gizmos.DrawWireCube(_myCollisionAreaData.Offset
				+ Vector3.right * (_myCollisionAreaData.HalfAreaSize.x + _myCollisionAreaData.HalfAreaWidth)
				, Vector3.right * _myCollisionAreaData.AreaWidth
				+ Vector3.up * _myCollisionAreaData.HalfAreaSize.y * 2
				+ Vector3.forward * _myCollisionAreaData.HalfAreaSize.z * 2);
			//��
			Gizmos.DrawWireCube(_myCollisionAreaData.Offset
				 + Vector3.left * (_myCollisionAreaData.HalfAreaSize.x + _myCollisionAreaData.HalfAreaWidth)
				, Vector3.right * _myCollisionAreaData.AreaWidth
				+ Vector3.up * _myCollisionAreaData.HalfAreaSize.y * 2
				+ Vector3.forward * _myCollisionAreaData.HalfAreaSize.z * 2);
			//�O
			Gizmos.DrawWireCube(_myCollisionAreaData.Offset
				+ Vector3.forward * (_myCollisionAreaData.HalfAreaSize.z + _myCollisionAreaData.HalfAreaWidth)
				, Vector3.right * _myCollisionAreaData.HalfAreaSize.x * 2
				+ Vector3.up * _myCollisionAreaData.HalfAreaSize.y * 2
				+ Vector3.forward * _myCollisionAreaData.AreaWidth);
			//��
			Gizmos.DrawWireCube(_myCollisionAreaData.Offset
				+ Vector3.back * (_myCollisionAreaData.HalfAreaSize.z + _myCollisionAreaData.HalfAreaWidth)
				, Vector3.right * _myCollisionAreaData.HalfAreaSize.x * 2
				+ Vector3.up * _myCollisionAreaData.HalfAreaSize.y * 2
				+ Vector3.forward * _myCollisionAreaData.AreaWidth);
			Gizmos.matrix = _matrixTemp;
		}
	}


}