using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateScript : BaseCharcterStateScript
{
	public AttackStateScript(BaseCharcterScript myOwner, Animator ownerAnimator, IInputCharcterAction input) : base(myOwner, ownerAnimator, input)
	{

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
