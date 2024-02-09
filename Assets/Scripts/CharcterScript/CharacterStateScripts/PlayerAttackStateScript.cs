using UnityEngine;

public class PlayerAttackStateScript : BaseAttackStateScript
{


	public PlayerAttackStateScript(BaseCharacterScript myOwner, Animator ownerAnimator
		, IInputCharcterAction input) : base(myOwner, ownerAnimator, input)
	{
		_attackDistanceData = Resources.Load<AttackDistanceScriptableScript>("Scriptable/PlayerAttackMoveData");
	}
}
