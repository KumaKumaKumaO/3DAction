using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonGuardCharacterScript : BaseCharacterScript
{
	public override void Init()
	{
		base.Init();
		_myStateMachine = new DemonGuardCharacterStateMachineScript
			(new DemonGuardAIInputScript(transform
			,_objectManagerScript.PlayerCharcterScript.MyCollisionAreaData.MyTransform
			,_myWeapon.MyCollisionAreaData.HalfAreaSize.z * 2)
			, this, _myAnimator);
	}
}
