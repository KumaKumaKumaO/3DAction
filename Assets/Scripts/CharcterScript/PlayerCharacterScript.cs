using UnityEngine;

/// <summary>
/// プレイヤーにアタッチするクラス
/// </summary>
public class PlayerCharacterScript : BaseCharacterScript
{
	public void SetPlayerInput(IInputCharcterActionGetable input)
	{
		 _myAnimator = GetComponent<Animator>();
#if UNITY_EDITOR
		if (_myAnimator is null)
		{
			ErrorManagerScript.MyInstance.NullCompornentError("Animator");
		}
#endif

		_myStateMachine = new PlayerCharacterStateMachineScript(this, _myAnimator, input
			, _objectManagerScript.CameraScript.CameraTransform);
	}
}
