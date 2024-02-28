using UnityEngine;
public class DemonGuardCharacterScript : BaseCharacterScript
{
	[SerializeField]
	private AIDifficulty myDifficulty = default;
	public override void Init()
	{
		base.Init();
		IInputCharcterAction input;
		if (isDebugInputPlayer)
		{
			input = new InGamePlayerInput();
		}
		else
		{
			if(myDifficulty == AIDifficulty.Easy)
			{
				input = new DemonGuardAIInputScript(this,null);
			}
			else if(myDifficulty == AIDifficulty.Normal)
			{
				input = new DemonGuardAIInputScript(this
					,new DemonGuardNormalAIStateMachineScript(_objectManagerScript.PlayerCharcterScript));
			}
			//ÉnÅ[Éh
			else
			{
				input = new DemonGuardAIInputScript(this,null);
			}
			
		}

		_myStateMachine = new DemonGuardCharacterStateMachineScript
			(input, this, _myAnimator);
	}
}
