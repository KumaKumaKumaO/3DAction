using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 回避ステート
/// </summary>
public class EvasionStateScript : BaseCharcterStateScript
{
	private int _evationAnimationHash = default;
	private int _nowAnimationHash = default;
	private bool isMove = default;

	public EvasionStateScript(BaseCharacterScript myOwner, Animator ownerAnimator
		, IInputCharcterActionGetable input) : base(myOwner, ownerAnimator, input)
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
					NoDamageLogic(_myOwner).Forget();
				}
			}
		}
	}
	
	/// <summary>
	/// 当たり判定を一時的に消す
	/// </summary>
	/// <param name="myOwner"></param>
	/// <returns></returns>
	private async UniTaskVoid NoDamageLogic(BaseCharacterScript myOwner)
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
