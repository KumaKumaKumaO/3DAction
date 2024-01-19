/// <summary>
/// プレイしているときの処理
/// </summary>
public class PlayStateScript : BaseInGameStateScript
{
	private ObjectManagerScript _objectManagerScript = default;
	public override void Enter()
	{
		base.Enter();
		_objectManagerScript = new ObjectManagerScript();
	}
	public override void Execute()
	{
		base.Execute();
	}
	public override void Exit()
	{
		base.Exit();
	}
}