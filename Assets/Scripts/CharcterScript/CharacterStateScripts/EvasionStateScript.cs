using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
			}
			else
			{
				_myOwner.MyCharcterStatus.Stamina.Value -= _myOwner.MyCharcterStatus.DecreaseEvasionStamina;
				isMove = true;
				if (_myOwner.CanCollision)
				{
					NoDamageTimer(_myOwner).Forget();
				}
			}
		}
	}
	
	private async UniTaskVoid NoDamageTimer(BaseCharacterScript myOwner)
	{
		myOwner.CanCollision = false;
		await UniTask.Delay(System.TimeSpan.FromSeconds(myOwner.MyCharcterStatus.EvasionNoDamageTime));
		myOwner.CanCollision = true;
		myOwner = null;
	}

	public override void Exit()
	{
		_myOwner.CanCollision = true;
		base.Exit();
	}
}
