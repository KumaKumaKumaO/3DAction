using UnityEngine;
public class DemonGuardCharacterScript : BaseCharacterScript
{
	[SerializeField]
	private AIDifficulty myDifficulty = default;
	public override void Init()
	{
		base.Init();
		IInputCharcterActionGetable input;
		if (isDebugInputPlayer)
		{
			input = new InGamePlayerInput();
		}
		else
		{
			if(myDifficulty == AIDifficulty.Easy)
			{
				input = new DemonGuardAIInputScript(this);
			}
			else if(myDifficulty == AIDifficulty.Normal)
			{
				input = new DemonGuardAIInputScript(this);
				((BaseAIInputScript)input).MyStateMachineScript = 
					new DemonGuardNormalAIStateMachineScript(_objectManagerScript.PlayerCharcterScript
					,(IInputCharcterActionSetable)input);
			}
			//ÉnÅ[Éh
			else
			{
				input = new DemonGuardAIInputScript(this);
			}
			
		}

		_myStateMachine = new DemonGuardCharacterStateMachineScript
			(input, this, _myAnimator);
	}
}
