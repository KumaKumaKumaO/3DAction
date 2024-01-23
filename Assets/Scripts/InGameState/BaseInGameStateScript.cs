using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInGameStateScript : BaseStateScript
{
	protected InGamePlayerInput _playerInput = default;
	public BaseInGameStateScript(InGamePlayerInput playerInput)
	{
		this._playerInput = playerInput;
	}
	public override void Exit()
	{
		base.Exit();
		_playerInput = null;
	}
}
