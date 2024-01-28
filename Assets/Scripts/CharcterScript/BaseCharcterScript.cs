using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラクターの操作や実際の動き
/// </summary>
public abstract class BaseCharcterScript : BaseObjectScript
{

    protected ICharacterStateMachine _myStateMachine = default;
    protected IInputCharcterAction _myInput = default;
    [SerializeField]
    protected CharcterStatus _myCharcterStatus = default;
    protected int _myJumpCount = default;

    public CharcterStatus MyCharcterStatus { get { return _myCharcterStatus; } }

    private void CollisionIndexInit()
    {
        _bottomCollisionIndex = -1;
        _topCollisionAreaDataIndex = -1;
        _rightCollisionAreaDataIndex = -1;
        _leftCollisionAreaDataIndex = -1;
        _forwardCollisionAreaDataIndex = -1;
        _backCollisionAreaDataIndex = -1;
    }
    public override void ObjectUpdate()
    {
        base.ObjectUpdate();
        _myCollisionObjects.Clear();
        _objectManagerScript.GetCollisionAllObject(_myCollisionAreaData, _myCollisionObjects);
        CollisionIndexInit();
        SelectColObjectResult();
        if (isGravity)
        {
            GravityFall();
        }
        _myStateMachine.UpdateState().Execute();
        JumpCharcter();

    }
    protected override void Reset()
    {
        base.Reset();
        if(gameObject.GetComponent<Animator>() == null)
        {
            gameObject.AddComponent<Animator>();
        }
    }
    protected override void GravityFall()
    {
        base.GravityFall();
        if (_myCollisionObjects.Count <= 0) { return; }
        if (_myCollisionObjects[0].CollisionObjectData is StageFloorScript floorScriptTemp)
        {
            floorScriptTemp.OnTopCharcter(this);
        }
    }

    protected void JumpCharcter()
    {
        if(_myInput.IsJump() &&  isGround)
        {
            _myJumpCount = (int)_myCharcterStatus.JumpPower;
            isGravity = false;
        }
        else if(_myJumpCount > 0)
        {
            ObjectMove(Vector3.up * _myCharcterStatus.JumpPower * _myJumpCount * Time.deltaTime);
            _myJumpCount--;
            if(_myJumpCount <= 0)
            {
                isGravity = true;
            }
        }
    }
    public virtual void HealHP(float healValue)
    {
        _myCharcterStatus.Hp += healValue;
    }

    public virtual void ReceiveDamage(float damage)
    {
        _myCharcterStatus.Hp -= damage;
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        if (isDebugColliderVisible)
        {
            //自分が使う当たり判定
            //上
            Gizmos.DrawWireCube(transform.position + _myCollisionAreaData.Offset
                + transform.up * (_myCollisionAreaData.HalfAreaSize.y + _myCollisionAreaData.HalfAreaWidth)
                , transform.right * 2 * _myCollisionAreaData.HalfAreaSize.x
                + transform.up * _myCollisionAreaData.AreaWidth
                + transform.forward * 2 * _myCollisionAreaData.HalfAreaSize.z);
            //下
            Gizmos.DrawWireCube(transform.position + _myCollisionAreaData.Offset
                + -transform.up * (_myCollisionAreaData.HalfAreaSize.y + _myCollisionAreaData.HalfAreaWidth)
                , transform.right * 2 * _myCollisionAreaData.HalfAreaSize.x
                + transform.up * _myCollisionAreaData.AreaWidth
                + transform.forward * 2 * _myCollisionAreaData.HalfAreaSize.z);
            //右
            Gizmos.DrawWireCube(transform.position + _myCollisionAreaData.Offset
                + transform.right * (_myCollisionAreaData.HalfAreaSize.x + _myCollisionAreaData.HalfAreaWidth)
                , transform.right * _myCollisionAreaData.AreaWidth
                + transform.up * 2 * _myCollisionAreaData.HalfAreaSize.y
                + transform.forward * 2 * _myCollisionAreaData.HalfAreaSize.z);
            //左
            Gizmos.DrawWireCube(transform.position + _myCollisionAreaData.Offset
                + -transform.right * (_myCollisionAreaData.HalfAreaSize.x + _myCollisionAreaData.HalfAreaWidth)
                , transform.right * _myCollisionAreaData.AreaWidth
                + transform.up * 2 * _myCollisionAreaData.HalfAreaSize.y
                + transform.forward * 2 * _myCollisionAreaData.HalfAreaSize.z);
            //前
            Gizmos.DrawWireCube(transform.position + _myCollisionAreaData.Offset
                + transform.forward * (_myCollisionAreaData.HalfAreaSize.z + _myCollisionAreaData.HalfAreaWidth)
                , transform.right * 2 * _myCollisionAreaData.HalfAreaSize.x
                + transform.up * 2 * _myCollisionAreaData.HalfAreaSize.y
                + transform.forward * _myCollisionAreaData.AreaWidth);
            //後
            Gizmos.DrawWireCube(transform.position + _myCollisionAreaData.Offset
                + -transform.forward * (_myCollisionAreaData.HalfAreaSize.z + _myCollisionAreaData.HalfAreaWidth)
                , transform.right * 2 * _myCollisionAreaData.HalfAreaSize.x
                + transform.up * 2 * _myCollisionAreaData.HalfAreaSize.y
                + transform.forward * _myCollisionAreaData.AreaWidth);
        }
    }
    private void SelectColObjectResult()
    {
        for (int i = 0; i < _myCollisionObjects.Count; i++)
        {
            if (_myCollisionObjects[i].IsCollisionBottom)
            {
                if (_bottomCollisionIndex < 0
                    || !(_myCollisionObjects[_bottomCollisionIndex].CollisionObjectData.MyCollisionAreaData.TopYPos
                    < _myCollisionObjects[i].CollisionObjectData.MyCollisionAreaData.TopYPos))
                {
                    _bottomCollisionIndex = i;

                }
            }
            if (_myCollisionObjects[i].IsCollisionTop)
            {
                if (_topCollisionAreaDataIndex < 0
                    || _myCollisionObjects[_topCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData.BottomYPos
                    > _myCollisionObjects[i].CollisionObjectData.MyCollisionAreaData.BottomYPos)
                {
                    _topCollisionAreaDataIndex = i;
                }
            }
            if (_myCollisionObjects[i].IsCollisionRight)
            {
                if (_rightCollisionAreaDataIndex < 0
                    || _myCollisionObjects[_rightCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData.LeftXPos
                    > _myCollisionObjects[i].CollisionObjectData.MyCollisionAreaData.LeftXPos)
                {
                    _rightCollisionAreaDataIndex = i;
                }
            }
            if (_myCollisionObjects[i].IsCollisionLeft)
            {
                if (_leftCollisionAreaDataIndex < 0
                    || _myCollisionObjects[_leftCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData.RightXPos
                    < _myCollisionObjects[i].CollisionObjectData.MyCollisionAreaData.RightXPos)
                {
                    _leftCollisionAreaDataIndex = i;
                }
            }
            if (_myCollisionObjects[i].IsCollisionForward)
            {
                if (_forwardCollisionAreaDataIndex < 0
                    || _myCollisionObjects[_forwardCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData.BackZPos
                    > _myCollisionObjects[i].CollisionObjectData.MyCollisionAreaData.BackZPos)
                {
                    _forwardCollisionAreaDataIndex = i;
                }
            }
            if (_myCollisionObjects[i].IsCollisionBack)
            {
                if (_backCollisionAreaDataIndex < 0
                    || _myCollisionObjects[_backCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData.ForwardZPos
                    < _myCollisionObjects[i].CollisionObjectData.MyCollisionAreaData.ForwardZPos)
                {
                    _backCollisionAreaDataIndex = i;
                }
            }
        }
    }

}
