using UnityEngine;
using Cysharp.Threading.Tasks;

public class DeathStateScript : BaseCharcterStateScript
{
	private int _nowAnimationHash = default;
	public DeathStateScript(BaseCharacterScript myOwner,Animator animator,IInputCharcterAction input)
		:base(myOwner,animator,input)
	{
	}
	public override void Enter()
	{
		base.Enter();
		_myOwnerAnimator.SetTrigger("DeathTrigger");
		_nowAnimationHash = _myOwnerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
	}

	public override void Execute()
	{
		//アニメーションが変わったら
		if(_nowAnimationHash != _myOwnerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash)
		{
			Wait();
			//現在のアニメーションのハッシュを保持する
			_nowAnimationHash = _myOwnerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
		}
		base.Execute();
	}
	
	private async void Wait()
	{
		await UniTask.Delay(System.TimeSpan.FromSeconds(2f));
		if (_myOwner is null) { return; }
		_myOwner.Delete();
	}
}
