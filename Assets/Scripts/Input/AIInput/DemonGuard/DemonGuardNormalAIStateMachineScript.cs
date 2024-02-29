using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonGuardNormalAIStateMachineScript : BaseAIStateMachineScript
{
	private BaseCharacterScript _targetCharcter = default;
	private BaseCharacterScript _myCharcter = default;

	public DemonGuardNormalAIStateMachineScript(BaseCharacterScript targetCharacterScript
		,IInputCharcterActionSetable input):base(input)
	{
		_targetCharcter = targetCharacterScript;
		_nowState = new AIIdleStateScript(input);
	}

	public override BaseAIStateScript UpdateState()
	{
		const float IDLE_DISTANCE = 20;
		//”½‰ž‹——£“à‚©‚Ç‚¤‚©
		if ((_myCharcter.MyTransform.position - _targetCharcter.MyTransform.position).magnitude < IDLE_DISTANCE)
		{
			
		}
		else if(_nowState is not AIIdleStateScript)
		{
			ChangeState(new AIIdleStateScript(_input));
		}
		return _nowState;
	}

}
