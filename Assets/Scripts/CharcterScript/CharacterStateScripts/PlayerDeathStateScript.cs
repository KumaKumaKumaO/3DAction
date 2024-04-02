using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

/// <summary>
/// プレイヤーの死亡ステート
/// </summary>
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
	
	/// <summary>
	/// 死亡後の待機処理
	/// </summary>
	/// <returns></returns>
	private async UniTaskVoid Wait()
	{
		await UniTask.Delay(System.TimeSpan.FromSeconds(5f));
		SceneChange();
	}

	/// <summary>
	/// シーン変更
	/// </summary>
	private void SceneChange()
	{
		if (GameManagerScript.Instance.NowState is InGameStateScript)
		{
			SceneManager.LoadScene("GameOver");
		}
	}
}
