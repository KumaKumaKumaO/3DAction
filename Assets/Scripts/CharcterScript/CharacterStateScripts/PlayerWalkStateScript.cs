using UnityEngine;

/// <summary>
/// プレイヤーの歩くステート
/// </summary>
public class PlayerWalkStateScript : BaseCharcterStateScript
{
	private Vector2 _inputVector = default;
	private int _isMoveAnimamtorHashValue = default;
	private int _motionSpeedAnimatorHashValue = default;
	private float _nowCharcterSpeed = default;
	private Transform _cameraTransform = default;

	public PlayerWalkStateScript(BaseCharacterScript myOwner, Animator ownerAnimator
	 , IInputCharcterActionGetable input, UnityEngine.Transform cameraTransform) : base(myOwner, ownerAnimator, input)
	{
		this._cameraTransform = cameraTransform;
	}

	public override void Enter()
	{
		base.Enter();
		_motionSpeedAnimatorHashValue = Animator.StringToHash("MoveMotionSpeed");
		_isMoveAnimamtorHashValue = Animator.StringToHash("IsMove");
		_nowCharcterSpeed = _myOwner.MyCharcterStatus.Speed;

		_myOwnerAnimator.SetFloat(_motionSpeedAnimatorHashValue, _nowCharcterSpeed
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
		_myOwnerAnimator.SetBool(_isMoveAnimamtorHashValue, _inputVector != Vector2.zero);

		if (_nowCharcterSpeed != _myOwner.MyCharcterStatus.Speed)
		{
			_nowCharcterSpeed = _myOwner.MyCharcterStatus.Speed;
			_myOwnerAnimator.SetFloat(_motionSpeedAnimatorHashValue, _nowCharcterSpeed
				/ _myOwner.MyCharcterStatus.DefaultSpeed);
		}

		if (_inputVector != Vector2.zero)
		{
			_myOwner.MyCollisionAreaData.MyTransform.rotation
				= Quaternion.Euler(0
				, _cameraTransform.eulerAngles.y + Mathf.Atan2(_inputVector.x, _inputVector.y) * Mathf.Rad2Deg
				, 0);

			_myOwner.ObjectMove(_myOwner.MyCollisionAreaData.MyTransform.forward
				* _myOwner.MyCharcterStatus.Speed * Time.deltaTime);
		}
	}

	public override void Exit()
	{
		base.Exit();
		_cameraTransform = null;
	}
}
