using UnityEngine;

public class PlayerAttackStateScript : BaseAttackStateScript
{
	private AttackDistanceScriptableScript _attackDistanceData = default;
	private Vector3 _attackMoveVectorTemp = default;
	private Vector3 _attackMoveVectorClac = default;
	private Vector3 _attackMoveVector = default;
	public PlayerAttackStateScript(BaseCharacterScript myOwner, Animator ownerAnimator
		, IInputCharcterAction input) : base(myOwner, ownerAnimator, input)
	{
		_attackDistanceData = Resources.Load<AttackDistanceScriptableScript>("Scriptable/PlayerAttackMoveData");
	}

	protected override void AttackCountUp()
	{
		_attackMoveVectorTemp = _attackDistanceData[_attackCount];
		base.AttackCountUp();
	}
	public override void AttackMove()
	{
		switch (_nowState)
		{
			case AttackState.Attacking:
				{
					_attackMoveVectorClac = (_myOwner.MyTransform.forward * _attackMoveVectorTemp.z
						+ _myOwner.MyTransform.right * _attackMoveVectorTemp.x
						+ _myOwner.MyTransform.up * _attackMoveVectorTemp.y)
						* Time.deltaTime;
					_myOwner.ObjectMove(_attackMoveVectorClac);
						break;
				}

		}
	}
}
