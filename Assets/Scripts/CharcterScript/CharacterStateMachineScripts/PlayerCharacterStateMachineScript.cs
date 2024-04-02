using UnityEngine;

/// <summary>
/// プレイヤーのステートマシン
/// </summary>

public class PlayerCharacterStateMachineScript : BaseCharacterStateMachineScript
{
	private Transform _cameraTransform = default;

	public PlayerCharacterStateMachineScript(PlayerCharacterScript myOwner, Animator myOwnerAnimator
		, IInputCharcterActionGetable playerInput, Transform cameraTransform) : base(playerInput, myOwner, myOwnerAnimator)
	{
		_nowState = new PlayerWalkStateScript(myOwner, myOwnerAnimator, playerInput, cameraTransform);
		_nowState.Enter();
		_cameraTransform = cameraTransform;
	}

	public override BaseCharcterStateScript UpdateState()
	{
		if (_nowState is PlayerDeathStateScript)
		{
			return _nowState;
		}
		else if (_myOwner.IsDeath)
		{
			ChangeState(new PlayerDeathStateScript(_myOwner, _myOwnerAnimator, _input));
		}
		else if (_nowState.CanInterruption)
		{
			if (_input.IsAttack)
			{
				if (!(_nowState is BaseAttackStateScript))
				{
					ChangeState(new BaseAttackStateScript(_myOwner, _myOwnerAnimator, _input));
				}
			}
			else if (_input.IsEvasion 
				&& _myOwner.MyCharcterStatus.Stamina.Value >= _myOwner.MyCharcterStatus.DecreaseEvasionStamina)
			{
				if (!(_nowState is EvasionStateScript))
				{
					ChangeState(new EvasionStateScript(_myOwner, _myOwnerAnimator, _input));
				}
			}
			else if (_input.ChangeWeapon() != 0)
			{

			}
			else if (_input.UseItem() != 0)
			{

			}
			else if (_input.IsRun)
			{

			}
			//歩く
			else
			{
				if (!(_nowState is PlayerWalkStateScript))
				{
					ChangeState(new PlayerWalkStateScript(_myOwner, _myOwnerAnimator, _input, _cameraTransform));
				}
			}
		}
		return _nowState;
	}

	public override void Delete()
	{
		base.Delete();
		_cameraTransform = null;
	}
}
