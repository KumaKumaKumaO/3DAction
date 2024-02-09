using UnityEngine;
/// <summary>
/// キャラクター用のステートマシンのベース
/// </summary>
public class BaseCharacterStateMachineScript : ICharacterStateMachine
{
	protected IInputCharcterAction _input = default;
	protected BaseCharcterStateScript _nowState = default;
	protected BaseCharacterScript _myOwner = default;
	protected Animator _myOwnerAnimator = default;

	public BaseCharacterStateMachineScript(IInputCharcterAction input
		, BaseCharacterScript myOwner, Animator myOwnerAnimator)
	{
		this._input = input;
		this._myOwner = myOwner;
		_myOwnerAnimator = myOwnerAnimator;
		_nowState = new WalkStateScript(myOwner,myOwnerAnimator,input);
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
		if (_input.IsAttack())
		{
			if (!(_nowState is BaseAttackStateScript))
			{
				ChangeState(new BaseAttackStateScript(_myOwner, _myOwnerAnimator, _input));
			}
		}
		else if (_input.IsEvasion())
		{

		}
		else if (_input.ChangeWeapon() != 0)
		{

		}
		else if (_input.UseItem() != 0)
		{

		}
		else if (_input.IsJump())
		{
			//ChangeState(new JumpStateScript(_myOwner, _myOwnerAnimator, _input));
		}
		else if (_input.IsRun())
		{

		}
		//歩く
		else
		{
			if (!(_nowState is WalkStateScript))
			{
				ChangeState(new WalkStateScript(_myOwner, _myOwnerAnimator, _input));
			}
		}
		return _nowState;
	}
	protected void ChangeState(BaseCharcterStateScript nextState)
	{
		_nowState.Exit();
		_nowState = nextState;
		_nowState.Enter();
	}
}
