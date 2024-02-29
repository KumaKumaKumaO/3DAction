using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAIStateScript : BaseStateScript
{
	protected IInputCharcterActionSetable _input = default;
	public BaseAIStateScript(IInputCharcterActionSetable input)
	{
		_input = input;
	}
	public override void Enter()
	{
		base.Enter();
	}
	public override void Execute()
	{
		base.Execute();
	}
	public override void Exit()
	{
		base.Exit();
	}
}
