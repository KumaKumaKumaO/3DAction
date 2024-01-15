/// <summary>
/// ステートの基底クラス
/// </summary>
public abstract class BaseStateScript
{

	/// <summary>
	/// ステートに入ったときに1度だけ実行される
	/// </summary>
	public virtual void Enter()
	{

	}

	/// <summary>
	/// 毎フレーム実行される
	/// </summary>
	public virtual void Execute()
	{

	}

	/// <summary>
	/// ステートから出るときに1度だけ実行される
	/// </summary>
	public virtual void Exit()
	{

	}
}
