using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStateScript : BaseCharcterStateScript
{
	private float _jumpPowerTemp = 0;
	private Vector2 _inputVector = default;
	private float _fallSpeed = 9.8f;
	private int _isJumpAnimamtorHashValue = default;
	public JumpStateScript(BaseCharacterScript myOwner, Animator myAnimator, IInputCharcterAction input) : base(myOwner, myAnimator, input)
	{

	}
	public override void Enter()
	{
		base.Enter();
		_isJumpAnimamtorHashValue = Animator.StringToHash("IsJump");
		JumpInit();
	}
	public override void Execute()
	{
		base.Execute();
		Debug.LogWarning(_jumpPowerTemp);
		_inputVector = _input.MoveInput;
		if (_jumpPowerTemp > 0)
		{
			//_myOwner.ObjectMove((Vector3.up * _myOwner.MyCharcterStatus.JumpPower 
			//	+ Vector3.right * _inputVector.x * _myOwner.MyCharcterStatus.AirSpeed
			//	+ Vector3.forward * _inputVector.y * _myOwner.MyCharcterStatus.AirSpeed) * Time.deltaTime);
			_jumpPowerTemp -= Time.deltaTime * _fallSpeed;
		}
		else if (_jumpPowerTemp <= 0)
		{
			if (!_myOwner.IsGravity)
			{
				_myOwner.IsGravity = true;
				_ownerAnimator.SetBool(_isJumpAnimamtorHashValue, false);
			}
			if (_myOwner.IsGround)
			{
				canInterruption = true;
				_jumpPowerTemp = 0;
			}
		}
	}
	private void JumpInit()
	{
		_ownerAnimator.SetBool(_isJumpAnimamtorHashValue,true);
		//_jumpPowerTemp = _myOwner.MyCharcterStatus.JumpPower;
		_myOwner.IsGravity = false;
		canInterruption = false;
	}
}
