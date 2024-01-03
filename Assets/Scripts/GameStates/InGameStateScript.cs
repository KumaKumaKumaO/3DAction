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
        _myStateMachineScript = new InGameStateMachineScript();
    }
    public override void Execute()
    {
        base.Execute();
        //ステート更新して実行する
        _myStateMachineScript.UpdateState().Execute();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
