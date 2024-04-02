/// <summary>
/// AI�̃��W�b�N�p�̃X�e�[�g
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
