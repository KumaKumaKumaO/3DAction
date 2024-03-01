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
			
		}

		_myStateMachine = new DemonGuardCharacterStateMachineScript
			(input, this, _myAnimator);
	}
}
