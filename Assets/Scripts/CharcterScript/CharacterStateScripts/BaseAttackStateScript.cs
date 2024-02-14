using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttackStateScript : BaseCharcterStateScript
{
	private BaseWeaponScript _myOwnerWeapon = default;
	private int _attackTriggerHash = default;
	private int _nowAnimationHash = default;
	protected AttackState _nowState = default;
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
			//Debug.LogWarning("w");
			//���݂̃A�j���[�V�����̃n�b�V���l�����Z�b�g
			_nowAnimationHash = default;
		}

		//���݂̃A�j���[�V��������ύX���ꂽ��
		if (_ownerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash != _nowAnimationHash)
		{
			//�ύX��̃A�j���[�V�����̃n�b�V���l�����݂̒l�Ƃ��ĕێ�
			_nowAnimationHash = _ownerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
			
			//���݂��҂���ԂȂ�
			if (_nowState == AttackState.Wait)
			{
				//Debug.LogWarning("a");
				//�U�����ɑJ��
				_nowState = AttackState.Attacking;
				//����̓����蔻����I���ɂ���
				_myOwnerWeapon.IsAttack = true;
			}
			//�U������������
			else if(_nowState == AttackState.Attacking)
			{
				//Debug.LogWarning("e");
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
				//Debug.LogWarning("a");
				//�U�����ɑJ�ڂ���
				_nowState = AttackState.Attacking;
				//����̓����蔻����I���ɂ���
				_myOwnerWeapon.IsAttack = true;
			}
		}
		//���̍U���ɔh���������������炩�U�����Ȃ�
		else if(_input.IsAttack && _nowState == AttackState.Attacking)
		{
			//���̍U���ɔh���҂���Ԃɂ���
			_nowState = AttackState.NextAttack;
			//Debug.LogWarning("na");
			//�U���g���K�[���I���ɂ���
			_ownerAnimator.SetTrigger(_attackTriggerHash);
		}
	}
	public override void Exit()
	{
		base.Exit();
		//�A�h���X��j������
		_myOwnerWeapon = null;
	}
}
