using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonGuardNormalAIStateMachineScript : BaseAIStateMachineScript
{
	private BaseCharacterScript _targetCharcter = default;
	private BaseCharacterScript _myCharcter = default;

	public DemonGuardNormalAIStateMachineScript(BaseCharacterScript targetCharacterScript)
	{
		_targetCharcter = targetCharacterScript;
		_nowState = new AIIdolStateScript();
	}

	public override BaseAIStateScript UpdateState()
	{

		if ((_myCharcter.MyTransform.position - _targetCharcter.MyTransform.position).magnitude > 20)
		{
			
		}
		return null;
	}

}
