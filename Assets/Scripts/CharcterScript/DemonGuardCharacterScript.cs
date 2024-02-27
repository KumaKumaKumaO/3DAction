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
				input = new DemoGuardEasyAIInputScript(this, _objectManagerScript.PlayerCharcterScript,
				_myWeapon.MyCollisionAreaData.HalfAreaSize.z);
			}
			else if(myDifficulty == AIDifficulty.Normal)
			{
				input = new DemonGuardNormalAIInputScript(this, _objectManagerScript.PlayerCharcterScript,
				_myWeapon.MyCollisionAreaData.HalfAreaSize.z);
			}
			else
			{
				input = new DemonGuardNormalAIInputScript(this, _objectManagerScript.PlayerCharcterScript,
				_myWeapon.MyCollisionAreaData.HalfAreaSize.z);
			}
			
		}

		_myStateMachine = new DemonGuardCharacterStateMachineScript
			(input, this, _myAnimator);
	}
}
