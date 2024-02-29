using UnityEngine;

public abstract class BaseCharcterStateScript : BaseStateScript
{
	protected BaseCharacterScript _myOwner = default;
	protected Animator _myOwnerAnimator = default;
	protected IInputCharcterActionGetable _input = default;
	protected bool canInterruption = false;
	public bool CanInterruption { get { return canInterruption; } }

	public BaseCharcterStateScript(BaseCharacterScript myOwner, Animator ownerAnimator, IInputCharcterActionGetable input)
	{
		this._input = input;
		this._myOwnerAnimator = ownerAnimator;
		this._myOwner = myOwner;
	}
	public override void Enter()
	{
		Debug.Log(_myOwner.name + "が" + this + "に遷移");
	}
	public override void Exit()
	{
		base.Exit();
		_myOwner = null;
		_myOwnerAnimator = null;
		_input = null;
	}
}
