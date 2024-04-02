/// <summary>
/// AIのロジック用のステート
/// </summary>
public class BaseAIStateScript : BaseStateScript
{
	protected IInputCharcterActionControlable _input = default;

	public BaseAIStateScript(IInputCharcterActionControlable input)
	{
		_input = input;
	}

	public override void Exit()
	{
		base.Exit();
		_input = null;
	}
}
