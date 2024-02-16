using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MoveAnimationStateMachineBehaviourScript : StateMachineBehaviour
{
	[SerializeField]
	private AnimationSettingData _nowLoadSettingData = default;
	[SerializeField]
	private Vector3 _goalPos = default;
	[SerializeField]
	private float _startRatio = default;
	[SerializeField]
	private AnimationSettingData[] _settingDatas = default;

	private MatchTargetWeightMask _mask = new MatchTargetWeightMask(Vector3.one, 0);
	private IEnumerator _enumerator = default;
	private bool isEnd = default;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_enumerator = _settingDatas.GetEnumerator();
		_enumerator.MoveNext();
		LoadSettingData(animator.transform);

	}
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (animator.IsInTransition(layerIndex)) { return; }
		if (!isEnd && stateInfo.normalizedTime >= _nowLoadSettingData.EndRatio)
		{
			if (_enumerator.MoveNext())
			{
				LoadSettingData(animator.transform);
			}
			else
			{
				isEnd = true;
			}
		}
		animator.MatchTarget(_goalPos, Quaternion.identity, _nowLoadSettingData.TargetPart
			, _mask, _nowLoadSettingData.StartRatio, _nowLoadSettingData.EndRatio);
	}
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateExit(animator, stateInfo, layerIndex);
		_nowLoadSettingData = null;
		_enumerator = null;
	}
	private void LoadSettingData(Transform myTransform)
	{
		_nowLoadSettingData = _enumerator.Current as AnimationSettingData;

		_goalPos = myTransform.transform.position
		+ myTransform.transform.forward * _nowLoadSettingData.MoveVector.z
		+ myTransform.transform.up * _nowLoadSettingData.MoveVector.y
		+ myTransform.transform.right * _nowLoadSettingData.MoveVector.x;
	}
}
