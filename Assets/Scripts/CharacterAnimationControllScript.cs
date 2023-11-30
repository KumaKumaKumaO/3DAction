using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationControllScript
{
    private Animator _myAnimator;
    public void Initialize(Animator animator)
    {
        this._myAnimator = animator;
    }
    public void AnimationControl(Vector3 myVelocity)
    {
        _myAnimator.SetFloat("ZVelocity", myVelocity.z);
        _myAnimator.SetFloat("XVelocity", myVelocity.x);
        _myAnimator.SetFloat("YVelocity", myVelocity.y);
    }
}
