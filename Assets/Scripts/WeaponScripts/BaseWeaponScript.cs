using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeaponScript : BaseObjectScript
{
    [SerializeField]
    protected WeaponStatus _myStatus = default;
    [SerializeField]
    protected bool isHit = default;
    public override void Init()
    {
        base.Init();
    }
    public override void ObjectUpdate()
    {
        _myCollisionObjects.Clear();
        CollisionIndexInit();
        _objectManagerScript.GetCollisionCharcter(_myCollisionAreaData, _myCollisionObjects);
        isHit = _myCollisionObjects.Count > 0;
        foreach(CollisionResultData resultData in _myCollisionObjects)
        {
            (resultData.CollisionObjectData as BaseCharcterScript).ReceiveDamage(_myStatus.Attack);
        }
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        if (isDebugColliderVisible)
        {
            //自分の当たり判定
            //上
            Gizmos.DrawWireCube(_myCollisionAreaData.Offset
                + Vector3.up * (_myCollisionAreaData.HalfAreaSize.y + _myCollisionAreaData.HalfAreaWidth)
                , Vector3.right * _myCollisionAreaData.HalfAreaSize.x * 2
                + Vector3.up * _myCollisionAreaData.AreaWidth
                + Vector3.forward * _myCollisionAreaData.HalfAreaSize.z * 2);
            //下
            Gizmos.DrawWireCube(_myCollisionAreaData.Offset
                + Vector3.down * (_myCollisionAreaData.HalfAreaSize.y + _myCollisionAreaData.HalfAreaWidth)
                , Vector3.right * _myCollisionAreaData.HalfAreaSize.x * 2
                + Vector3.up * _myCollisionAreaData.AreaWidth
                + Vector3.forward * _myCollisionAreaData.HalfAreaSize.z * 2);
            //右
            Gizmos.DrawWireCube( _myCollisionAreaData.Offset
                + Vector3.right * (_myCollisionAreaData.HalfAreaSize.x + _myCollisionAreaData.HalfAreaWidth)
                , Vector3.right * _myCollisionAreaData.AreaWidth
                + Vector3.up * _myCollisionAreaData.HalfAreaSize.y * 2
                + Vector3.forward * _myCollisionAreaData.HalfAreaSize.z * 2);
            //左
            Gizmos.DrawWireCube( _myCollisionAreaData.Offset
                 + Vector3.left * (_myCollisionAreaData.HalfAreaSize.x + _myCollisionAreaData.HalfAreaWidth)
                , Vector3.right * _myCollisionAreaData.AreaWidth
                + Vector3.up * _myCollisionAreaData.HalfAreaSize.y * 2
                + Vector3.forward * _myCollisionAreaData.HalfAreaSize.z * 2);
            //前
            Gizmos.DrawWireCube( _myCollisionAreaData.Offset
                + Vector3.forward * (_myCollisionAreaData.HalfAreaSize.z + _myCollisionAreaData.HalfAreaWidth)
                , Vector3.right * _myCollisionAreaData.HalfAreaSize.x * 2
                + Vector3.up * _myCollisionAreaData.HalfAreaSize.y * 2
                + Vector3.forward * _myCollisionAreaData.AreaWidth);
            //後
            Gizmos.DrawWireCube( _myCollisionAreaData.Offset
                + Vector3.back * (_myCollisionAreaData.HalfAreaSize.z + _myCollisionAreaData.HalfAreaWidth)
                , Vector3.right * _myCollisionAreaData.HalfAreaSize.x * 2
                + Vector3.up * _myCollisionAreaData.HalfAreaSize.y * 2
                + Vector3.forward * _myCollisionAreaData.AreaWidth);
            Gizmos.matrix = _matrixTemp;
        }
    }
}
