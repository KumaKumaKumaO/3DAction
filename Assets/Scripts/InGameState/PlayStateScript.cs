/// <summary>
/// プレイしているときの処理
/// </summary>
public class PlayStateScript : BaseInGameStateScript
{
	private ObjectManagerScript _objectManagerScript = default;
	public override void Enter()
	{
		base.Enter();
		//当たり判定のロジックを生成
		new CollisionSystem().InstancateMyInstance();
		_objectManagerScript = new ObjectManagerScript();
	}
	public override void Execute()
	{
		base.Execute();
	}
	public override void Exit()
	{
		base.Exit();
		CollisionSystem.Instance.DeleteMyInstance();
	}
}