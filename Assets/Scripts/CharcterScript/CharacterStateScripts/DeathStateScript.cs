using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathStateScript : BaseCharcterStateScript
{
	private int _nowAnimationHash = default;
	private bool isPlayAnimation = false;
	public DeathStateScript(BaseCharacterScript myOwner,Animator animator,IInputCharcterAction input)
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
			Debug.LogWarning("bbbbbbb");
			Wait();
			//���݂̃A�j���[�V�����̃n�b�V����ێ�����
			_nowAnimationHash = _ownerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
			if (isPlayAnimation)
			{
				//���S���o�����Ă�����

			}
			else
			{
				isPlayAnimation = true;
			}
		}
		base.Execute();
	}
	
	private async void Wait()
	{
		Debug.LogWarning("adada");
		await System.Threading.Tasks.Task.Delay(1000);
		SceneManager.LoadScene("GameOver");
	}
}
