using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// 死亡ステート
/// </summary>
public class DeathStateScript : BaseCharcterStateScript
{
	private int _nowAnimationHash = default;

	public DeathStateScript(BaseCharacterScript myOwner,Animator animator,IInputCharcterActionGetable input)
		:base(myOwner,animator,input)
	{
	}

	public override void Enter()
	{
		base.Enter();
		_myOwnerAnimator.SetBool("DeathFlag",true);
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
	
	/// <summary>
	/// オブジェクトを削除するまでの処理
	/// </summary>
	private async void Wait()
	{
		await UniTask.Delay(System.TimeSpan.FromSeconds(5f));
		if (_myOwner is null) { return; }
		_myOwner.Delete();
	}
}
