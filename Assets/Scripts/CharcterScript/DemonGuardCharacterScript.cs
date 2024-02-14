using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonGuardCharacterScript : BaseCharacterScript
{
	public override void Init()
	{
		base.Init();
		IInputCharcterAction input = default;
		if (isDebugInputPlayer)
		{
			input = new InGamePlayerInput();
		}
		else
		{
			input = new DemonGuardAIInputScript(_myTransform,_objectManagerScript.PlayerCharcterScript.MyTransform,
				_myWeapon.MyCollisionAreaData.HalfAreaSize.z);
		}

		_myStateMachine = new DemonGuardCharacterStateMachineScript
			(input, this, _myAnimator);
	}
}
