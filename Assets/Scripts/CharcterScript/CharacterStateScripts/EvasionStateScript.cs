using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasionStateScript : BaseCharcterStateScript
{
	private int _evationAnimationHash = default;
	private int _nowAnimationHash = default;
	private bool isMove = default;
	public EvasionStateScript(BaseCharacterScript myOwner, Animator ownerAnimator
		, IInputCharcterAction input) : base(myOwner, ownerAnimator, input)
	{
	}

	public override void Enter()
	{
		base.Enter();
		_evationAnimationHash = Animator.StringToHash("EvationTrigger");
		_nowAnimationHash = _myOwnerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
		_myOwnerAnimator.SetTrigger(_evationAnimationHash);
	}

	public override void Execute()
	{
		base.Execute();
		if (canInterruption && isMove)
		{
			canInterruption = false;
			isMove = false;
			_nowAnimationHash = _myOwnerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
		}
		else if (!_nowAnimationHash.Equals(_myOwnerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash))
		{
			_nowAnimationHash = _myOwnerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
			if (isMove)
			{
				canInterruption = true;
				_myOwner.CanCollision = true;
			}
			else
			{
				isMove = true;
				_myOwner.CanCollision = false;
			}
		}
	}

	public override void Exit()
	{
		_myOwner.CanCollision = true;
		base.Exit();
	}
}
