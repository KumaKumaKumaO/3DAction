using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// プレイヤーのステートマシン
/// </summary>

public class PlayerCharcterStateMachineScript : BaseCharcterStateMachineScript
{
	private Transform _cameraTransform = default;
	public PlayerCharcterStateMachineScript(PlayerCharcterScript myOwner,Animator myOwnerAnimator
		, IInputCharcterAction playerInput,Transform cameraTransform) : base(playerInput,myOwner)
	{
		_nowState = new WalkStateScript(myOwner,myOwnerAnimator, playerInput);
		_nowState.Enter();
		_cameraTransform = cameraTransform;
	}
    public override BaseCharcterStateScript UpdateState()
    {
		if (!_nowState.CanInterruption)
		{
			return _nowState;
		}
		if (_input.IsAttack())
		{

		}
		else if (_input.IsEvasion())
		{

		}
		else if (_input.ChangeWeapon() != 0)
		{

		}
		else if (_input.UseItem() != 0)
		{

		}
		else if (_input.IsRun())
		{

		}
		//歩く
		else
		{
			if (!(_nowState is PlayerWalkStateScript))
			{
				ChangeState(new PlayerWalkStateScript(_myOwner, _myOwnerAnimator, _input,_cameraTransform));
			}
		}
		return _nowState;
	}
}
