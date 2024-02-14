using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathStateScript : BaseCharcterStateScript
{
	private int _nowAnimationHash = default;
	public PlayerDeathStateScript(BaseCharacterScript myOwner,Animator animator,IInputCharcterAction input)
		:base(myOwner,animator,input)
	{
	}
	public override void Enter()
	{
		base.Enter();
		_ownerAnimator.SetBool("IsDeath", true);
		_nowAnimationHash = _ownerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
	}

	public override void Execute()
	{
		//�A�j���[�V�������ς������
		if(_nowAnimationHash != _ownerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash)
		{
			Wait();
			//���݂̃A�j���[�V�����̃n�b�V����ێ�����
			_nowAnimationHash = _ownerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
		}
		base.Execute();
	}
	
	private async void Wait()
	{
		await System.Threading.Tasks.Task.Delay(5000);
		SceneManager.LoadScene("GameOver");
	}
}
