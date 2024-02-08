using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// プレイヤーのステートマシン
/// </summary>

public class PlayerCharacterStateMachineScript : BaseCharacterStateMachineScript
{
	private Transform _cameraTransform = default;
	public PlayerCharacterStateMachineScript(PlayerCharacterScript myOwner,Animator myOwnerAnimator
		, IInputCharcterAction playerInput,Transform cameraTransform) : base(playerInput,myOwner,myOwnerAnimator)
	{
		_nowState = new PlayerWalkStateScript(myOwner,myOwnerAnimator, playerInput,cameraTransform);
		_nowState.Enter();
		_cameraTransform = cameraTransform;
	}
    public override BaseCharcterStateScript UpdateState()
    {
		if(_myOwner.MyCharcterStatus.Hp <= 0)
		{

		}
		if (!_nowState.CanInterruption)
		{
			return _nowState;
		}
		if (_input.IsAttack())
		{
			if (!(_nowState is PlayerAttackStateScript))
			{
				ChangeState(new PlayerAttackStateScript(_myOwner, _myOwnerAnimator, _input));
			}
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
