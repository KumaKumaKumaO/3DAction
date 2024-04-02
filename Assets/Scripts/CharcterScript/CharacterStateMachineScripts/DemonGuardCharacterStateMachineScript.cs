using UnityEngine;

/// <summary>
/// デーモンガードのステートマシン
/// </summary>
public class DemonGuardCharacterStateMachineScript : BaseCharacterStateMachineScript
{
	public DemonGuardCharacterStateMachineScript(IInputCharcterActionGetable input,BaseCharacterScript myOwner
		, Animator myOwnerAnimator) :base(input,myOwner,myOwnerAnimator)
	{

	}
}
