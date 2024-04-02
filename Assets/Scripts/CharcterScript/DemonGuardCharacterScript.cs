using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// デーモンガードのキャラクタースクリプト
/// </summary>
public class DemonGuardCharacterScript : BaseCharacterScript
{
	[SerializeField,Header("AIの強さ")]
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
			//ハード
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
