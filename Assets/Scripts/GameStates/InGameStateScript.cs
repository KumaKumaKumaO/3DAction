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
		BaseInGameStateScript initState = new PlayStateScript();
		_myStateMachineScript = new InGameStateMachineScript(initState);
	}

	public override void Execute()
	{
		base.Execute();
		//ステート更新して実行する
		_myStateMachineScript.UpdateState().Execute();
	}

	public override void Exit()
	{
		_myStateMachineScript = null;
		base.Exit();
	}
}
