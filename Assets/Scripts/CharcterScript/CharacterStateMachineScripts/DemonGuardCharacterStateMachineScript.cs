using UnityEngine;

/// <summary>
/// デーモンガードのステートマシン
/// </summary>
public class DemonGuardCharacterStateMachineScript : BaseCharacterStateMachineScript
{
	public DemonGuardCharacterStateMachineScript(IInputCharcterActionGetable input,BaseCharacterScript myOwner
		, Animator myOwnerAnimator, UnityEngine.Transform cameraTransform) :base(input,myOwner,myOwnerAnimator,cameraTransform)
	{

	}
}
