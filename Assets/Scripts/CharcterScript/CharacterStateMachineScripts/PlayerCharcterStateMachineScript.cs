using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// プレイヤーのステートマシン
/// </summary>

public class PlayerCharcterStateMachineScript : BaseCharcterStateMachineScript
{
	public PlayerCharcterStateMachineScript(InGamePlayerInput playerInput) : base(playerInput)
	{
	}

	public override BaseCharcterStateScript UpdateState()
	{
		if (_input.IsAttack())
		{

		}
		else if (_input.IsEvasion())
		{
			
		}
		else if (_input.ChangeWeapon() != 0)
		{

		}
		else if(_input.UseItem() != 0)
		{

		}
		//止まる
		else if(_input.MoveInput() ==  Vector2.zero)
		{

		}
		else if(_input.IsRun())
		{

		}
		//歩く
		else
		{
			if(_nowState is WalkStateScript)
			{

			}
			else
			{
				return new WalkStateScript();
			}
		}
		return null;
	}
}
