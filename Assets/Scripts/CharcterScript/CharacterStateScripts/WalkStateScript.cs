using UnityEngine;

/// <summary>
/// �����X�e�[�g
/// </summary>
public class WalkStateScript : BaseCharcterStateScript
{
	private Vector2 _inputVector = default;
	private int _isMoveAnimamtorHashValue = default;
	private int _motionSpeedAnimatorHashValue = default;
	private float _nowCharcterSpeed = default;
	private Transform _cameraTransform = default;

	public WalkStateScript(BaseCharacterScript myOwner, Animator ownerAnimator
		, IInputCharcterActionGetable input, Transform cameraTransform) : base(myOwner, ownerAnimator, input)
	{
		_cameraTransform = cameraTransform;
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
		if (_myOwner.IsGround)
		{
			canInterruption = true;
		}
		else
		{
			canInterruption = false;
		}
		base.Execute();
		_inputVector = _input.MoveInput;

		if (_nowCharcterSpeed != _myOwner.MyCharcterStatus.Speed)
		{
			_nowCharcterSpeed = _myOwner.MyCharcterStatus.Speed;
			_myOwnerAnimator.SetFloat(_motionSpeedAnimatorHashValue, _nowCharcterSpeed
				/ _myOwner.MyCharcterStatus.DefaultSpeed);
		}

		_myOwnerAnimator.SetBool(_isMoveAnimamtorHashValue, _inputVector != Vector2.zero);

		if (_inputVector != Vector2.zero)
		{
			if (_myOwner.IsInputTowards)
			{
				if (_myOwner.IsDebugInputPlayer)
				{
					_myOwner.MyCollisionAreaData.MyTransform.rotation
						= Quaternion.Euler(0
						, _cameraTransform.eulerAngles.y + Mathf.Atan2(_inputVector.x, _inputVector.y) * Mathf.Rad2Deg
						, 0);

					_myOwner.ObjectMove(_myOwner.MyCollisionAreaData.MyTransform.forward
						* _myOwner.MyCharcterStatus.Speed * Time.deltaTime);
				}
				else
				{
					_myOwner.MyCollisionAreaData.MyTransform.rotation
						= Quaternion.Euler(0,
						Vector3.SignedAngle(_cameraTransform.forward, _inputVector, _myOwner.MyTransform.up), 0);

					_myOwner.ObjectMove(_myOwner.MyCollisionAreaData.MyTransform.forward
						* _myOwner.MyCharcterStatus.Speed * Time.deltaTime);
				}
			}
			else
			{
				_myOwner.ObjectMove((_myOwner.MyTransform.forward * _inputVector.y
					+ _myOwner.MyTransform.right * _inputVector.x)
					* _myOwner.MyCharcterStatus.Speed * Time.deltaTime);
			}
		}

	}
}
