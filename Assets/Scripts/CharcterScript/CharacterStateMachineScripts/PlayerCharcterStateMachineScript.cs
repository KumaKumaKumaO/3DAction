using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// プレイヤーのステートマシン
/// </summary>

public class PlayerCharcterStateMachineScript : BaseCharcterStateMachineScript
{
	public PlayerCharcterStateMachineScript(PlayerCharcterScript myOwner,Animator myOwnerAnimator, IInputCharcterAction playerInput) : base(playerInput,myOwner)
	{
		_nowState = new WalkStateScript(myOwner,myOwnerAnimator, playerInput);
		_nowState.Enter();
	}
}
