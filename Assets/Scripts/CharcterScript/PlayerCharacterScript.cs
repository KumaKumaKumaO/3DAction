using UnityEngine;

/// <summary>
/// プレイヤーにアタッチするクラス
/// </summary>
public class PlayerCharacterScript : BaseCharacterScript
{
	/// <summary>
	/// プレイヤー入力を設定する
	/// </summary>
	/// <param name="input">プレイヤー入力</param>
	public void SetPlayerInput(IInputCharcterActionGetable input)
	{
		_myStateMachine = new PlayerCharacterStateMachineScript(this, _myAnimator, input
			, _objectManagerScript.CameraScript.CameraTransform);
	}
	public override void Init()
	{
		base.Init();
		_myAnimator = GetComponent<Animator>();
#if UNITY_EDITOR
		if (_myAnimator is null)
		{
			ErrorManagerScript.MyInstance.NullCompornentError("Animator");
		}
#endif

	}
}
