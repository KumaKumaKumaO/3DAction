using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkStateScript : BaseCharcterStateScript
{
	private Vector2 _inputVector = default;
	private int _isMoveAnimamtorHashValue = default;
	private int _motionSpeedAnimatorHashValue = default;
	private float _nowCharcterSpeed = default;
	private Transform _cameraTansform = default;
	public PlayerWalkStateScript(BaseCharacterScript myOwner, Animator ownerAnimator
	 , IInputCharcterAction input, Transform cameraTransform) : base(myOwner, ownerAnimator, input)
	{
		this._cameraTansform = cameraTransform;
	}
	public override void Enter()
	{
		base.Enter();
		_motionSpeedAnimatorHashValue = Animator.StringToHash("MoveMotionSpeed");
		_isMoveAnimamtorHashValue = Animator.StringToHash("IsMove");
		_nowCharcterSpeed = _myOwner.MyCharcterStatus.Speed;

		_ownerAnimator.SetFloat(_motionSpeedAnimatorHashValue, _nowCharcterSpeed
			/ _myOwner.MyCharcterStatus.DefaultSpeed);
	}
	public override void Execute()
	{
		base.Execute();
		if (_myOwner.IsGround)
		{
			canInterruption = true;
		}
		else
		{
			canInterruption = false;
		}
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
				= Quaternion.Euler(0
				, _cameraTansform.eulerAngles.y + Mathf.Atan2(_inputVector.x, _inputVector.y) * Mathf.Rad2Deg
				, 0);

			_myOwner.ObjectMove(_myOwner.MyCollisionAreaData.MyTransform.forward
				* _myOwner.MyCharcterStatus.Speed * Time.deltaTime);
		}


	}
}
