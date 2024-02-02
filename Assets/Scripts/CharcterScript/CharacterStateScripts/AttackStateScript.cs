using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateScript : BaseCharcterStateScript
{
	private BaseWeaponScript _myOwnerWeapon = default;
	private int _attackCount = default;
	private int _attackCountHashValue = default;
	private int _nowAnimationHash = default;
	private AttackState _nowState = default;

	private enum AttackState
	{
		Wait,
		Attacking,
		NextAttack,
		End
	}

	public AttackStateScript(BaseCharacterScript myOwner, Animator ownerAnimator
		, IInputCharcterAction input) : base(myOwner, ownerAnimator, input)
	{
		_myOwnerWeapon = myOwner.MyWeapon;
	}
	public override void Enter()
	{
		base.Enter();
		_attackCountHashValue = Animator.StringToHash("AttackCount");
		ResetAttackCount();
		IncrementAttackCount();
		_myOwnerWeapon.IsAttack = true;
		_nowAnimationHash = _ownerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
		_nowState = AttackState.Wait;
		Debug.Log("w");
	}

	public override void Execute()
	{
		base.Execute();

		bool isChangeAnimation = _ownerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash != _nowAnimationHash;
		//アニメーションが変更されたら
		if (isChangeAnimation)
		{
			_nowAnimationHash = _ownerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
			if (_nowState == AttackState.Wait)
			{
				_nowState = AttackState.Attacking;
				Debug.Log("a");
				_myOwnerWeapon.IsAttack = true;
			}
			else if(_nowState == AttackState.Attacking)
			{
				_nowState = AttackState.End;
				Debug.Log("e");
				_myOwnerWeapon.IsAttack = false;
				canInterruption = true;
			}
			else if (_nowState == AttackState.NextAttack)
			{
				Debug.Log("A");
				_nowState = AttackState.Attacking;
				_myOwnerWeapon.IsAttack = true;
			}
		}
		//攻撃継続をさせたかったら
		else if(_input.IsAttack() && _nowState == AttackState.Attacking && _attackCount <= 2)
		{
			IncrementAttackCount();
			_nowState = AttackState.NextAttack;
			Debug.Log("n");
		}



	}
	private void ResetAttackCount()
	{
		_attackCount = 0;
		_ownerAnimator.SetInteger(_attackCountHashValue, _attackCount);
	}
	private void IncrementAttackCount()
	{
		_attackCount += 1;
		_ownerAnimator.SetInteger(_attackCountHashValue, _attackCount);
	}

	public override void Exit()
	{
		ResetAttackCount();

		base.Exit();

		_myOwnerWeapon = null;
	}
}
