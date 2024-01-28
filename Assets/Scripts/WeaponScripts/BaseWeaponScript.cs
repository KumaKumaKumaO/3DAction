using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeaponScript : BaseObjectScript
{
    [SerializeField]
    protected WeaponStatus _myStatus = default;
    public override void Init()
    {
        base.Init();
    }
    public override void ObjectUpdate()
    {
        _myCollisionObjects.Clear();
        _objectManagerScript.GetCollisionCharcter(_myCollisionAreaData, _myCollisionObjects);
        foreach(CollisionResultData resultData in _myCollisionObjects)
        {
            (resultData.CollisionObjectData as BaseCharcterScript).ReceiveDamage(_myStatus.Attack);
            Debug.LogWarning("aa");
        }
    }
    protected override void OnDrawGizmos()
    {
        if (isDebugColliderVisible)
        {
            //自分が使う当たり判定
            //上
            Gizmos.DrawWireCube(transform.position + _myCollisionAreaData.Offset
                + transform.up * (_myCollisionAreaData.HalfAreaSize.y + _myCollisionAreaData.HalfAreaWidth)
                , Vector3.right * 2 * _myCollisionAreaData.HalfAreaSize.x
                + Vector3.up * _myCollisionAreaData.AreaWidth
                + Vector3.forward * 2 * _myCollisionAreaData.HalfAreaSize.z);
            //下
            Gizmos.DrawWireCube(transform.position + _myCollisionAreaData.Offset
                + -transform.up * (_myCollisionAreaData.HalfAreaSize.y + _myCollisionAreaData.HalfAreaWidth)
                , Vector3.right * 2 * _myCollisionAreaData.HalfAreaSize.x
                + Vector3.up * _myCollisionAreaData.AreaWidth
                + Vector3.forward * 2 * _myCollisionAreaData.HalfAreaSize.z);
            //右
            Gizmos.DrawWireCube(transform.position + _myCollisionAreaData.Offset
                + transform.right * (_myCollisionAreaData.HalfAreaSize.x + _myCollisionAreaData.HalfAreaWidth)
                , Vector3.right * _myCollisionAreaData.AreaWidth
                + Vector3.up * 2 * _myCollisionAreaData.HalfAreaSize.y
                + Vector3.forward * 2 * _myCollisionAreaData.HalfAreaSize.z);
            //左
            Gizmos.DrawWireCube(transform.position + _myCollisionAreaData.Offset
                + -transform.right * (_myCollisionAreaData.HalfAreaSize.x + _myCollisionAreaData.HalfAreaWidth)
                , Vector3.right * _myCollisionAreaData.AreaWidth
                + Vector3.up * 2 * _myCollisionAreaData.HalfAreaSize.y
                + Vector3.forward * 2 * _myCollisionAreaData.HalfAreaSize.z);
            //前
            Gizmos.DrawWireCube(transform.position + _myCollisionAreaData.Offset
                + transform.forward * (_myCollisionAreaData.HalfAreaSize.z + _myCollisionAreaData.HalfAreaWidth)
                , Vector3.right * 2 * _myCollisionAreaData.HalfAreaSize.x
                + Vector3.up * 2 * _myCollisionAreaData.HalfAreaSize.y
                + Vector3.forward * _myCollisionAreaData.AreaWidth);
            //後
            Gizmos.DrawWireCube(transform.position + _myCollisionAreaData.Offset
                + -transform.forward * (_myCollisionAreaData.HalfAreaSize.z + _myCollisionAreaData.HalfAreaWidth)
                , Vector3.right * 2 * _myCollisionAreaData.HalfAreaSize.x
                + Vector3.up * 2 * _myCollisionAreaData.HalfAreaSize.y
                + Vector3.forward * _myCollisionAreaData.AreaWidth);
        }
    }
}
