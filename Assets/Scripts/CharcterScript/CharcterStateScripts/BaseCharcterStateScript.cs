using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharcterStateScript : BaseStateScript
{
    protected BaseCharcterScript _myOwner = default;
    protected Animator _ownerAnimator = default;

    public BaseCharcterStateScript(BaseCharcterScript myOwner, Animator ownerAnimator, IInputCharcterAction input)
    {
        this._ownerAnimator = ownerAnimator;
        this._myOwner = myOwner;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log(_myOwner.name + "‚ª" + this + "‚É‘JˆÚ");
    }
}
