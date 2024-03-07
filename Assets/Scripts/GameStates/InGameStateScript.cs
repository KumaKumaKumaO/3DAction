using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// InGameステートでのメイン処理
/// </summary>
public class InGameStateScript : BaseGameStateScript
{
	//自分のステートマシン
	private InGameStateMachineScript _myStateMachineScript = default;

	public override void Enter()
	{
		base.Enter();
		InGamePlayerInput input = new InGamePlayerInput();
		BaseInGameStateScript initState = new PlayStateScript(input);
		_myStateMachineScript = new InGameStateMachineScript(initState,input);
	}

	public override void Execute()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("Title");
		}
		base.Execute();
		//ステート更新して実行する
		_myStateMachineScript.UpdateState().Execute();
	}

	public override void Exit()
	{
		_myStateMachineScript.Delete();
		_myStateMachineScript = null;
		base.Exit();
	}
}
