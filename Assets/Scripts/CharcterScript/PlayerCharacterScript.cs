using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーにアタッチするクラス
/// </summary>
public class PlayerCharacterScript : BaseCharacterScript
{
	public void SetPlayerInput(IInputCharcterAction input)
	{
		Animator myAnimator = GetComponent<Animator>();
		if (myAnimator == null)
		{
			ErrorManagerScript.MyInstance.NullCompornentError("Animator");
		}
		_myAnimator = myAnimator;
		_myStateMachine = new PlayerCharacterStateMachineScript(this, _myAnimator, input
			,_objectManagerScript.CameraScript.CameraTransform);
	}
	public override void Init()
	{
		base.Init();
	}
}
