using UnityEngine;

/// <summary>
/// AI�̑ҋ@�X�e�[�g
/// </summary>
public class AIIdleStateScript : BaseAIStateScript
{
	public AIIdleStateScript(IInputCharcterActionControlable input):base(input)
	{
	}

	public override void Enter()
	{
		base.Enter();
		_input.MoveInput = Vector2.zero;
	}
}
