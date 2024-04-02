/// <summary>
/// AIのステートマシンのベース
/// </summary>
public abstract class BaseAIStateMachineScript
{
	protected BaseAIStateScript _nowState = default;
	protected BaseAIStateScript _beforeState = default;
	protected IInputCharcterActionControlable _input = default;

	public BaseAIStateMachineScript(IInputCharcterActionControlable input)
	{
		_input = input;
	}

	/// <summary>
	/// ステートの更新
	/// </summary>
	/// <returns>現在のステート</returns>
	public abstract BaseAIStateScript UpdateState();

	/// <summary>
	/// ステートを変更する
	/// </summary>
	/// <param name="state">変更後のステート</param>
	public void ChangeState(BaseAIStateScript state)
	{
		_nowState.Exit();
		_beforeState = _nowState;
		_nowState = state;
		_nowState.Enter();
	}
}
