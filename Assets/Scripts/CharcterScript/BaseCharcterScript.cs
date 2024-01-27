using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �L�����N�^�[�̑������ۂ̓���
/// </summary>
public abstract class BaseCharcterScript : BaseObjectScript
{

    protected ICharacterStateMachine _myStateMachine = default;
    protected IInputCharcterAction _myInput = default;
    [SerializeField]
    protected CharcterStatus _myCharcterStatus = default;
    public override void ObjectUpdate()
    {
        base.ObjectUpdate();
        MoveCharcter(_myInput.MoveInput());
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

    protected void MoveCharcter(Vector2 inputData)
    {
        ObjectMove(Vector3.right * inputData.x * _myCharcterStatus.Speed * Time.deltaTime
            + Vector3.forward * inputData.y * _myCharcterStatus.Speed * Time.deltaTime);
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
            //�������g�������蔻��
            //��
            Gizmos.DrawWireCube(transform.position + _myCollisionAreaData.Offset
                + Vector3.up * (_myCollisionAreaData.HalfAreaSize.y + _myCollisionAreaData.HalfAreaWidth)
                , Vector3.right * 2 * _myCollisionAreaData.HalfAreaSize.x
                + Vector3.up * _myCollisionAreaData.AreaWidth
                + Vector3.forward * 2 * _myCollisionAreaData.HalfAreaSize.z);
            //��
            Gizmos.DrawWireCube(transform.position + _myCollisionAreaData.Offset
                + Vector3.down * (_myCollisionAreaData.HalfAreaSize.y + _myCollisionAreaData.HalfAreaWidth)
                , Vector3.right * 2 * _myCollisionAreaData.HalfAreaSize.x
                + Vector3.up * _myCollisionAreaData.AreaWidth
                + Vector3.forward * 2 * _myCollisionAreaData.HalfAreaSize.z);
            //�E
            Gizmos.DrawWireCube(transform.position + _myCollisionAreaData.Offset
                + Vector3.right * (_myCollisionAreaData.HalfAreaSize.y + _myCollisionAreaData.HalfAreaWidth)
                , Vector3.right * _myCollisionAreaData.AreaWidth
                + Vector3.up * 2 * _myCollisionAreaData.HalfAreaSize.y
                + Vector3.forward * 2 * _myCollisionAreaData.HalfAreaSize.z);
            //��
            Gizmos.DrawWireCube(transform.position + _myCollisionAreaData.Offset
                + Vector3.left * (_myCollisionAreaData.HalfAreaSize.y + _myCollisionAreaData.HalfAreaWidth)
                , Vector3.right * _myCollisionAreaData.AreaWidth
                + Vector3.up * 2 * _myCollisionAreaData.HalfAreaSize.y
                + Vector3.forward * 2 * _myCollisionAreaData.HalfAreaSize.z);
            //�O
            Gizmos.DrawWireCube(transform.position + _myCollisionAreaData.Offset
                + Vector3.forward * (_myCollisionAreaData.HalfAreaSize.y + _myCollisionAreaData.HalfAreaWidth)
                , Vector3.right * 2 * _myCollisionAreaData.HalfAreaSize.x
                + Vector3.up * 2 * _myCollisionAreaData.HalfAreaSize.y
                + Vector3.forward * _myCollisionAreaData.AreaWidth);
            //��
            Gizmos.DrawWireCube(transform.position + _myCollisionAreaData.Offset
                + Vector3.back * (_myCollisionAreaData.HalfAreaSize.y + _myCollisionAreaData.HalfAreaWidth)
                , Vector3.right * 2 * _myCollisionAreaData.HalfAreaSize.x
                + Vector3.up * 2 * _myCollisionAreaData.HalfAreaSize.y
                + Vector3.forward * _myCollisionAreaData.AreaWidth);
        }
    }
}
