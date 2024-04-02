/// <summary>
/// AI�̃X�e�[�g�}�V���̃x�[�X
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
	/// �X�e�[�g�̍X�V
	/// </summary>
	/// <returns>���݂̃X�e�[�g</returns>
	public abstract BaseAIStateScript UpdateState();

	/// <summary>
	/// �X�e�[�g��ύX����
	/// </summary>
	/// <param name="state">�ύX��̃X�e�[�g</param>
	public void ChangeState(BaseAIStateScript state)
	{
		_nowState.Exit();
		_beforeState = _nowState;
		_nowState = state;
		_nowState.Enter();
	}
}
