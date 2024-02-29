using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class PlayerDeathStateScript : BaseCharcterStateScript
{
	private int _nowAnimationHash = default;
	public PlayerDeathStateScript(BaseCharacterScript myOwner,Animator animator,IInputCharcterActionGetable input)
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
			Wait().Forget();
			//現在のアニメーションのハッシュを保持する
			_nowAnimationHash = _myOwnerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
		}
		base.Execute();
	}
	
	private async UniTaskVoid Wait()
	{
		await UniTask.Delay(System.TimeSpan.FromSeconds(5f));
		SceneManager.LoadScene("GameOver");
	}
}
