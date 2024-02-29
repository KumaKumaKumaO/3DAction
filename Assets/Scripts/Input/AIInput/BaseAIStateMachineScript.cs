public abstract class BaseAIStateMachineScript
{
	protected BaseAIStateScript _nowState = default;
	protected BaseAIStateScript _beforeState = default;
	protected IInputCharcterActionSetable _input = default;
	public BaseAIStateMachineScript(IInputCharcterActionSetable input)
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
