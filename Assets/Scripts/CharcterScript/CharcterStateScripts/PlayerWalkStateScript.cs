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
	public PlayerWalkStateScript(BaseCharcterScript myOwner, Animator ownerAnimator
	 , IInputCharcterAction input, Transform cameraTransform) : base(myOwner, ownerAnimator, input)
	{
		this._cameraTansform = cameraTransform;
	}
	public override void Enter()
	{
		base.Enter();
		canInterruption = true;
		_motionSpeedAnimatorHashValue = Animator.StringToHash("MotionSpeed");
		_isMoveAnimamtorHashValue = Animator.StringToHash("IsMove");
		_nowCharcterSpeed = _myOwner.MyCharcterStatus.Speed;

		_ownerAnimator.SetFloat(_motionSpeedAnimatorHashValue, _nowCharcterSpeed / _myOwner.MyCharcterStatus.DefaultSpeed);
	}
	public override void Execute()
	{
		base.Execute();
		_inputVector = _input.MoveInput();
		if (_nowCharcterSpeed != _myOwner.MyCharcterStatus.Speed)
		{
			_nowCharcterSpeed = _myOwner.MyCharcterStatus.Speed;
			_ownerAnimator.SetFloat(_motionSpeedAnimatorHashValue, _nowCharcterSpeed
				/ _myOwner.MyCharcterStatus.DefaultSpeed);
		}
		_ownerAnimator.SetBool(_isMoveAnimamtorHashValue, _inputVector != Vector2.zero);

		if (_inputVector != Vector2.zero)
		{
			Vector3 moveVector = _cameraTansform.right * _inputVector.x + _cameraTansform.forward * _inputVector.y;
			Debug.LogWarning(Vector3.SignedAngle(_myOwner.MyCollisionAreaData.MyTransform.forward, moveVector, Vector3.up)
				+":" + moveVector +":" + _myOwner.MyCollisionAreaData.MyTransform.forward);
			_myOwner.MyCollisionAreaData.MyTransform.Rotate(
				0
				, Vector3.Angle(_myOwner.MyCollisionAreaData.MyTransform.forward, moveVector)
				, 0) ;

			_myOwner.ObjectMove(_myOwner.MyCollisionAreaData.MyTransform.forward
				* _myOwner.MyCharcterStatus.Speed * Time.deltaTime);
		}


	}
}
