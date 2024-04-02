using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �f�[�����K�[�h�̃L�����N�^�[�X�N���v�g
/// </summary>
public class DemonGuardCharacterScript : BaseCharacterScript
{
	[SerializeField,Header("AI�̋���")]
	private AIDifficulty myDifficulty = default;

	public override void Init()
	{
		base.Init();
		IInputCharcterActionGetable input;
#if UNITY_EDITOR
		if (isDebugInputPlayer)
		{
			input = new InGamePlayerInput();
		}
		else
		{
#endif
			input = new AIInputScript();
			//if (myDifficulty == AIDifficulty.Easy)
			//{
			//}
			if(myDifficulty == AIDifficulty.Normal)
			{
				((IAIInputInitializable)input).Init(this
					, new DemonGuardNormalAIStateMachineScript(_objectManagerScript.PlayerCharcterScript
						,this
						, (IInputCharcterActionControlable)input)
					);
			}
			//�n�[�h
			else
			{
			}
#if UNITY_EDITOR
		}
#endif

		_myStateMachine = new DemonGuardCharacterStateMachineScript
			(input, this, _myAnimator,_cameraTransform);
	}

    private void OnDestroy()
    {
		if (isDeath && GameManagerScript.Instance.NowState is InGameStateScript)
		{
			SceneManager.LoadScene("GameClear");
		}
	}
}
