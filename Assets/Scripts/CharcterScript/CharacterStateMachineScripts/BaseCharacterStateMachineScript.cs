using UnityEngine;

/// <summary>
/// キャラクター用のステートマシンのベース
/// </summary>
public class BaseCharacterStateMachineScript : ICharacterStateMachine
{

	protected IInputCharcterActionGetable _input = default;
	protected BaseCharcterStateScript _nowState = default;
	protected BaseCharcterStateScript _beforeState = default;
	protected BaseCharacterScript _myOwner = default;
	protected Animator _myOwnerAnimator = default;
	protected Transform _cameraTransform = default;

	public BaseCharacterStateMachineScript(IInputCharcterActionGetable input
		, BaseCharacterScript myOwner, Animator myOwnerAnimator, Transform cameraTransform)
	{
		this._cameraTransform = cameraTransform;
		this._input = input;
		this._myOwner = myOwner;
		_myOwnerAnimator = myOwnerAnimator;
		_nowState = new WalkStateScript(myOwner,myOwnerAnimator,input,cameraTransform);
		_nowState.Enter();
	}

	public virtual BaseCharcterStateScript UpdateState()
	{
		if (_nowState is DeathStateScript)
		{
			return _nowState;
		}
		else if (_myOwner.IsDeath )
		{
			ChangeState(new DeathStateScript(_myOwner, _myOwnerAnimator, _input));
		}
		if (!_nowState.CanInterruption)
		{
			return _nowState;
		}
		if (_input.IsAttack)
		{
			if (!(_nowState is BaseAttackStateScript))
			{
				ChangeState(new BaseAttackStateScript(_myOwner, _myOwnerAnimator, _input));
			}
		}
		else if (_input.IsEvasion)
		{
			if(!(_nowState is EvasionStateScript))
			{
				ChangeState(new EvasionStateScript(_myOwner, _myOwnerAnimator, _input));
			}
		}
		else if (_input.ChangeWeapon() != 0)
		{

		}
		else if (_input.UseItem() != 0)
		{

		}
		else if (_input.IsJump)
		{
			//ChangeState(new JumpStateScript(_myOwner, _myOwnerAnimator, _input));
		}
		else if (_input.IsRun)
		{

		}
		//歩く
		else
		{
			if (!(_nowState is WalkStateScript))
			{
				ChangeState(new WalkStateScript(_myOwner, _myOwnerAnimator, _input,_cameraTransform));
			}
		}
		return _nowState;
	}

	/// <summary>
	/// ステートを変更する
	/// </summary>
	/// <param name="nextState">変更後のステート</param>
	protected void ChangeState(BaseCharcterStateScript nextState)
	{
		_nowState.Exit();
		_nowState = nextState;
		_nowState.Enter();
	}

	/// <summary>
	/// 削除処理
	/// </summary>
	public virtual void Delete()
	{
		_input = null;
		_myOwner = null;
		_myOwnerAnimator = null;
		_nowState.Exit();
		_nowState = null;
	}
}
