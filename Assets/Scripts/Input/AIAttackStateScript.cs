/// <summary>
/// AIの攻撃ステート
/// </summary>
public class AIAttackStateScript : BaseAIStateScript
{
	public AIAttackStateScript(IInputCharcterActionControlable input) : base(input)
	{
	}

	public override void Enter()
	{
		base.Enter();
		_input.IsAttack = true;
	}

	public override void Execute()
	{
		base.Execute();
		if (_input.IsAttack)
		{
			_input.IsAttack = true;
		}

	}

	public override void Exit()
	{
		_input.IsAttack = false;
		base.Exit();
	}
}
