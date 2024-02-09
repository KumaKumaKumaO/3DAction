using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttackStateScript : BaseCharcterStateScript
{
	private BaseWeaponScript _myOwnerWeapon = default;
	private int _attackTriggerHash = default;
	private int _nowAnimationHash = default;
	protected AttackState _nowState = default;
	protected int _attackCount = default;

	protected AttackDistanceScriptableScript _attackDistanceData = default;
	private Vector3 _attackMoveVectorTemp = default;
	private Vector3 _attackMoveVectorClac = default;
	/// <summary>
	/// �U���̏�ԑJ��
	/// </summary>
	protected enum AttackState
	{
		/// <summary>
		/// �҂���
		/// </summary>
		Wait,
		/// <summary>
		/// �U����
		/// </summary>
		Attacking,
		/// <summary>
		/// ���̍U���ւ̔h���ҋ@��
		/// </summary>
		NextAttack,
		/// <summary>
		/// �U���I���҂���
		/// </summary>
		End
	}

	public BaseAttackStateScript(BaseCharacterScript myOwner, Animator ownerAnimator
		, IInputCharcterAction input) : base(myOwner, ownerAnimator, input)
	{
		_myOwnerWeapon = myOwner.MyWeapon;
		_attackDistanceData = Resources.Load<AttackDistanceScriptableScript>("Scriptable/BaseAttackMoveData");
	}
	public override void Enter()
	{
		base.Enter();
		//�A�j���[�V�����̃n�b�V���l���擾����
		_attackTriggerHash = Animator.StringToHash("AttackTrigger");
		//����̓����蔻����I���ɂ���
		_myOwnerWeapon.IsAttack = true;
		//���݂̃A�j���[�V�����̃n�b�V���l���擾����
		_nowAnimationHash = _ownerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
		//�U���g���K�[���I���ɂ���
		_ownerAnimator.SetTrigger(_attackTriggerHash);
		//�҂���ԂɑJ�ڂ���
		_nowState = AttackState.Wait;
	}

	public override void Execute()
	{
		base.Execute();
		//�I����Ԃ�������
		if (_nowState == AttackState.End)
		{
			//�҂���ԂɑJ��
			_nowState = AttackState.Wait;
			//���݂̃A�j���[�V�����̃n�b�V���l�����Z�b�g
			_nowAnimationHash = default;
			//�U���񐔂�����������
			_attackCount = 0;
		}

		//���݂̃A�j���[�V��������ύX���ꂽ��
		if (_ownerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash != _nowAnimationHash)
		{
			//�ύX��̃A�j���[�V�����̃n�b�V���l�����݂̒l�Ƃ��ĕێ�
			_nowAnimationHash = _ownerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
			
			//���݂��҂���ԂȂ�
			if (_nowState == AttackState.Wait)
			{
				//�U�����ɑJ��
				_nowState = AttackState.Attacking;
				//����̓����蔻����I���ɂ���
				_myOwnerWeapon.IsAttack = true;
				//�U���񐔂����Z����
				AttackCountUp();
			}
			//�U������������
			else if(_nowState == AttackState.Attacking)
			{
				//�I����ԂɑJ��
				_nowState = AttackState.End;
				//����̓����蔻����I�t�ɂ���
				_myOwnerWeapon.IsAttack = false;
				//����̊��荞�݂�������
				canInterruption = true;
			}
			//���̍U���ɔh�����悤�Ƃ��Ă�����
			else if (_nowState == AttackState.NextAttack)
			{
				//�U�����ɑJ�ڂ���
				_nowState = AttackState.Attacking;
				//����̓����蔻����I���ɂ���
				_myOwnerWeapon.IsAttack = true;
			}
		}
		//���̍U���ɔh���������������炩�U�����Ȃ�
		else if(_input.IsAttack() && _nowState == AttackState.Attacking && _attackCount < 2)
		{
			//���̍U���ɔh���҂���Ԃɂ���
			_nowState = AttackState.NextAttack;
			//�U���񐔂����Z����
			AttackCountUp();
			//�U���g���K�[���I���ɂ���
			_ownerAnimator.SetTrigger(_attackTriggerHash);
		}
		AttackMove();
	}
	protected virtual void AttackCountUp()
	{
		_attackMoveVectorTemp = _attackDistanceData[_attackCount];
		_attackCount += 1;
	}
	/// <summary>
	/// �U�����̈ړ�
	/// </summary>
	public virtual void AttackMove()
	{
		switch (_nowState)
		{
			case AttackState.Attacking:
				{
					_attackMoveVectorClac = (_myOwner.MyTransform.forward * _attackMoveVectorTemp.z
						+ _myOwner.MyTransform.right * _attackMoveVectorTemp.x
						+ _myOwner.MyTransform.up * _attackMoveVectorTemp.y)
						* Time.deltaTime;
					_myOwner.ObjectMove(_attackMoveVectorClac);
					break;
				}
		}
	}
	public override void Exit()
	{
		base.Exit();
		//�A�h���X��j������
		_myOwnerWeapon = null;
		_attackDistanceData = null;
	}
}
