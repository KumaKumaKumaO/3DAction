using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonGuardNormalAIStateMachineScript : BaseAIStateMachineScript
{
	private BaseCharacterScript _targetCharcter = default;
	private BaseCharacterScript _myCharcter = default;

	public DemonGuardNormalAIStateMachineScript(BaseCharacterScript targetCharacterScript
		,BaseCharacterScript myCharcterScript,IInputCharcterActionControlable input):base(input)
	{
		_myCharcter = myCharcterScript;
		_targetCharcter = targetCharacterScript;
		_nowState = new AIIdleStateScript(input);
		_nowState.Enter();
	}

	public override BaseAIStateScript UpdateState()
	{
		const float IDLE_DISTANCE = 20;
		const float ATTACK_DISTANCE = 5;
		Vector3 targetToVector = (_targetCharcter.MyTransform.position - _myCharcter.MyTransform.position );
		targetToVector -= targetToVector.y * Vector3.up;
		float targetToDistance = targetToVector.magnitude;
		//”½‰ž‹——£“à‚©‚Ç‚¤‚©
		if (targetToDistance < IDLE_DISTANCE)
		{
			if(targetToDistance < ATTACK_DISTANCE && _nowState is not AIAttackStateScript 
				&& IsEnemyToWard(targetToVector))
			{
				ChangeState(new AIAttackStateScript(_input));
			}
			else if (_nowState is not AITargetMoveStateScript)
			{
				ChangeState(new AITargetMoveStateScript
					(_input, _targetCharcter.MyTransform, _myCharcter.MyTransform));
			}
		}
		else if (_nowState is not AIIdleStateScript)
		{
			ChangeState(new AIIdleStateScript(_input));
		}


		return _nowState;
	}
	private bool IsEnemyToWard(Vector3 direction)
	{
		const float TO_WARD_ANGLE = 5f;

		return TO_WARD_ANGLE > Mathf.Abs(Vector3.SignedAngle
			(_myCharcter.MyTransform.forward,direction,_myCharcter.MyTransform.up));
	}
}
