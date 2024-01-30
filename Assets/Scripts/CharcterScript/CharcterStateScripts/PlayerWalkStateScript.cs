using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkStateScript : BaseCharcterStateScript
{
    private Vector2 _inputVector = default;
    private int _xVelocityAnimamtorHashValue = default;
    private int _zVelocityAnimamtorHashValue = default;
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
        _xVelocityAnimamtorHashValue = Animator.StringToHash("XVelocity");
        _zVelocityAnimamtorHashValue = Animator.StringToHash("ZVelocity");
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
        _ownerAnimator.SetFloat(_xVelocityAnimamtorHashValue, _inputVector.x);
        //入力のyはキャラクターの前進入力だから
        _ownerAnimator.SetFloat(_zVelocityAnimamtorHashValue, _inputVector.y);


        if (_inputVector != Vector2.zero)
        {
            _myOwner.ObjectMove((_cameraTansform.right - (Vector3.up * _cameraTansform.forward.y))
            * _inputVector.x * _myOwner.MyCharcterStatus.Speed * Time.deltaTime
            + (_cameraTansform.forward - (Vector3.up * _cameraTansform.forward.y))
            * _inputVector.y * _myOwner.MyCharcterStatus.Speed * Time.deltaTime);

            _myOwner.MyCollisionAreaData.MyTransform.rotation
                *= Quaternion.FromToRotation(_myOwner.MyCollisionAreaData.MyTransform.forward
                , Vector3.right * _inputVector.x + Vector3.forward * _inputVector.y);
        }

        
    }
}
