using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateMachineBehaviourScript : StateMachineBehaviour
{
	[SerializeField]
	private AvatarTarget _targetPart = AvatarTarget.Root;
	[SerializeField, Range(0, 1)]
	private float _startRatio = default;
	[SerializeField, Range(0, 1)]
	private float _endRatio = 1f;
	[SerializeField]
	private Vector3 _initGoalPos = default;
	[SerializeField]
	private Vector3 _goalPos = default;
	private MatchTargetWeightMask _mask = new MatchTargetWeightMask(Vector3.one, 0);

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_goalPos = animator.transform.position
			+ animator.transform.forward * _initGoalPos.z
			+ animator.transform.up * _initGoalPos.y
			+ animator.transform.right * _initGoalPos.x;
	}
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (animator.IsInTransition(layerIndex)) { return; }

		animator.MatchTarget(_goalPos, Quaternion.identity, _targetPart, _mask, _startRatio, _endRatio);
	}
}
