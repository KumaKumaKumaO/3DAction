using UnityEngine;
/// <summary>
/// キャラクター用のステートマシンのベース
/// </summary>
public abstract class BaseCharcterStateMachineScript:ICharacterStateMachine
{
	protected IInputCharcterAction _input = default;
	protected BaseCharcterStateScript _nowState = default;
	protected BaseCharcterScript _myOwner = default;
	protected Animator _myOwnerAnimator = default;

	public BaseCharcterStateMachineScript(IInputCharcterAction input,BaseCharcterScript myOwner)
	{
		this._input = input;
		this._myOwner = myOwner;
		_myOwnerAnimator = myOwner.GetComponent<Animator>();
	}
	public virtual BaseCharcterStateScript UpdateState()
    {
		if (_input.IsAttack())
		{

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

		}
		else if (_input.IsRun())
		{

		}
		//歩く
		else
		{
			if (!(_nowState is WalkStateScript))
			{
				ChangeState(new WalkStateScript(_myOwner, _myOwnerAnimator,_input));
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
