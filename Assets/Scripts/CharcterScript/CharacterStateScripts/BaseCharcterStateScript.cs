using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharcterStateScript : BaseStateScript
{
	protected BaseCharacterScript _myOwner = default;
	protected Animator _ownerAnimator = default;
	protected IInputCharcterAction _input = default;
	protected bool canInterruption = false;
	public bool CanInterruption { get { return canInterruption; } }

	public BaseCharcterStateScript(BaseCharacterScript myOwner, Animator ownerAnimator, IInputCharcterAction input)
	{
		this._input = input;
		this._ownerAnimator = ownerAnimator;
		this._myOwner = myOwner;
	}
	public override void Enter()
	{
		Debug.Log(_myOwner.name + "‚ª" + this + "‚É‘JˆÚ");
	}
	public override void Exit()
	{
		base.Exit();
		_myOwner = null;
		_ownerAnimator = null;
		_input = null;
	}
}
