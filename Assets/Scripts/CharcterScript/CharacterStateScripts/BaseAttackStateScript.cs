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
	/// 攻撃の状態遷移
	/// </summary>
	protected enum AttackState
	{
		/// <summary>
		/// 待ち中
		/// </summary>
		Wait,
		/// <summary>
		/// 攻撃中
		/// </summary>
		Attacking,
		/// <summary>
		/// 次の攻撃への派生待機中
		/// </summary>
		NextAttack,
		/// <summary>
		/// 攻撃終了待ち中
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
		//アニメーションのハッシュ値を取得する
		_attackTriggerHash = Animator.StringToHash("AttackTrigger");
		//武器の当たり判定をオンにする
		_myOwnerWeapon.IsAttack = true;
		//現在のアニメーションのハッシュ値を取得する
		_nowAnimationHash = _ownerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
		//攻撃トリガーをオンにする
		_ownerAnimator.SetTrigger(_attackTriggerHash);
		//待ち状態に遷移する
		_nowState = AttackState.Wait;
	}

	public override void Execute()
	{
		base.Execute();
		//終了状態だったら
		if (_nowState == AttackState.End)
		{
			//待ち状態に遷移
			_nowState = AttackState.Wait;
			//Debug.LogWarning("w");
			//現在のアニメーションのハッシュ値をリセット
			_nowAnimationHash = default;
		}

		//現在のアニメーションから変更されたら
		if (_ownerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash != _nowAnimationHash)
		{
			//変更後のアニメーションのハッシュ値を現在の値として保持
			_nowAnimationHash = _ownerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
			
			//現在が待ち状態なら
			if (_nowState == AttackState.Wait)
			{
				//Debug.LogWarning("a");
				//攻撃中に遷移
				_nowState = AttackState.Attacking;
				//武器の当たり判定をオンにする
				_myOwnerWeapon.IsAttack = true;
			}
			//攻撃中だったら
			else if(_nowState == AttackState.Attacking)
			{
				//Debug.LogWarning("e");
				//終了状態に遷移
				_nowState = AttackState.End;
				//武器の当たり判定をオフにする
				_myOwnerWeapon.IsAttack = false;
				//動作の割り込みを許可する
				canInterruption = true;
			}
			//次の攻撃に派生しようとしていたら
			else if (_nowState == AttackState.NextAttack)
			{
				//Debug.LogWarning("a");
				//攻撃中に遷移する
				_nowState = AttackState.Attacking;
				//武器の当たり判定をオンにする
				_myOwnerWeapon.IsAttack = true;
			}
		}
		//次の攻撃に派生させたかったらかつ攻撃中なら
		else if(_input.IsAttack && _nowState == AttackState.Attacking)
		{
			//次の攻撃に派生待ち状態にする
			_nowState = AttackState.NextAttack;
			//Debug.LogWarning("na");
			//攻撃トリガーをオンにする
			_ownerAnimator.SetTrigger(_attackTriggerHash);
		}
	}
	public override void Exit()
	{
		base.Exit();
		//アドレスを破棄する
		_myOwnerWeapon = null;
	}
}
