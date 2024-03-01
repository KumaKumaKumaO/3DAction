public abstract class BaseAIStateMachineScript
{
	protected BaseAIStateScript _nowState = default;
	protected BaseAIStateScript _beforeState = default;
	protected IInputCharcterActionControlable _input = default;
	public BaseAIStateMachineScript(IInputCharcterActionControlable input)
	{
		_input = input;
	}
	public abstract BaseAIStateScript UpdateState();

	public void ChangeState(BaseAIStateScript state)
	{
		_nowState.Exit();
		_beforeState = _nowState;
		_nowState = state;
		_nowState.Enter();
	}
}
