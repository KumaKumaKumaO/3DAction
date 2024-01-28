using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーにアタッチするクラス
/// </summary>
public class PlayerCharcterScript : BaseCharcterScript
{
	public void SetPlayerInput(IInputCharcterAction input)
	{
		_myInput = input;
		Animator myAnimator = GetComponent<Animator>();
		if (myAnimator == null)
        {
			ErrorManagerScript.MyInstance.NullCompornentError("Animator");
        }
		_myStateMachine = new PlayerCharcterStateMachineScript(this,myAnimator, input);
	}
	public override void Init()
	{
		base.Init();
	}
}
