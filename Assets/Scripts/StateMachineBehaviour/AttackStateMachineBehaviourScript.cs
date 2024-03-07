using UnityEngine;

public class AttackStateMachineBehaviourScript : StateMachineBehaviour
{
	[SerializeField,Range(0,1)]
	private float _startRatio = default;
	[SerializeField,Range(0,1)]
	private float _endRatio = 1;
	[SerializeField]
	private BaseWeaponScript _weaponScript = default;
	[SerializeField]
	private bool isHitting = default;

	[SerializeField]
	private float test = default;
	
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateEnter(animator, stateInfo, layerIndex);
		_weaponScript = animator.gameObject.GetComponent<BaseCharacterScript>().MyWeapon;
		isHitting = false;
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		test = stateInfo.normalizedTime;
		if (!isHitting && stateInfo.normalizedTime >= _startRatio)
		{
			isHitting = true;
			_weaponScript.IsAttacking = true;
		}
		else if (isHitting && stateInfo.normalizedTime >= _endRatio)
		{
			_weaponScript.IsAttacking = false;
		}
		base.OnStateUpdate(animator, stateInfo, layerIndex);

	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		isHitting = false;
		_weaponScript.IsAttacking = false;
		base.OnStateExit(animator, stateInfo, layerIndex);
		_weaponScript = null;
	}
}
