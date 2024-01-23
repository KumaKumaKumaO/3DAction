/// <summary>
/// キャラクター用のステートマシンのベース
/// </summary>
public abstract class BaseCharcterStateMachineScript:ICharacterStateMachine
{
	protected IInputCharcterAction _input = default;
	protected BaseCharcterStateScript _nowState = default;

	public BaseCharcterStateMachineScript(IInputCharcterAction input)
	{
		this._input = input;
	}
	public abstract BaseCharcterStateScript UpdateState();
}
