using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkStateScript : BaseCharcterStateScript
{
	private Vector2 _inputVector = default;
	private int _isMoveAnimamtorHashValue = default;
	private int _motionSpeedAnimatorHashValue = default;
	private float _nowCharcterSpeed = default;
	public WalkStateScript(BaseCharacterScript myOwner, Animator ownerAnimator
		, IInputCharcterAction input) : base(myOwner, ownerAnimator, input)
	{
	}
	public override void Enter()
	{
		base.Enter();
		canInterruption = true;
		_motionSpeedAnimatorHashValue = Animator.StringToHash("MoveMotionSpeed");
		_isMoveAnimamtorHashValue = Animator.StringToHash("IsMove");
		_nowCharcterSpeed = _myOwner.MyCharcterStatus.Speed;

		_ownerAnimator.SetFloat(_motionSpeedAnimatorHashValue, _nowCharcterSpeed
			/ _myOwner.MyCharcterStatus.DefaultSpeed);
	}
	public override void Execute()
	{
		base.Execute();
		_inputVector = _input.MoveInput;
		if (_nowCharcterSpeed != _myOwner.MyCharcterStatus.Speed)
		{
			_nowCharcterSpeed = _myOwner.MyCharcterStatus.Speed;
			_ownerAnimator.SetFloat(_motionSpeedAnimatorHashValue, _nowCharcterSpeed
				/ _myOwner.MyCharcterStatus.DefaultSpeed);
		}
		_ownerAnimator.SetBool(_isMoveAnimamtorHashValue, _inputVector != Vector2.zero);

		if (_inputVector != Vector2.zero)
		{
			_myOwner.MyCollisionAreaData.MyTransform.rotation
				= Quaternion.Euler(0,Mathf.Atan2(_inputVector.x, _inputVector.y) * Mathf.Rad2Deg, 0);

			_myOwner.ObjectMove(_myOwner.MyCollisionAreaData.MyTransform.forward
				* _myOwner.MyCharcterStatus.Speed * Time.deltaTime);
		}

	}
}
