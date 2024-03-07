using UnityEngine;
using UnityEngine.SceneManagement;
public class DemonGuardCharacterScript : BaseCharacterScript
{
	[SerializeField]
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
			if (myDifficulty == AIDifficulty.Easy)
			{
			}
			else if(myDifficulty == AIDifficulty.Normal)
			{
				((IAIInputInitializable)input).Init(this
					, new DemonGuardNormalAIStateMachineScript(_objectManagerScript.PlayerCharcterScript
						,this
						, (IInputCharcterActionControlable)input)
					);
			}
			//ÉnÅ[Éh
			else
			{
			}
#if UNITY_EDITOR
		}
#endif

		_myStateMachine = new DemonGuardCharacterStateMachineScript
			(input, this, _myAnimator);
	}
	public override void Delete()
	{
		base.Delete();
		if (isDeath && GameManagerScript.Instance.NowState is InGameStateScript)
		{
			SceneManager.LoadScene("GameClear");
		}
	}
}
