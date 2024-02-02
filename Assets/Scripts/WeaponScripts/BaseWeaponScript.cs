using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeaponScript : BaseObjectScript
{
	[SerializeField]
	protected WeaponStatus _myStatus = default;
	[SerializeField]
	protected bool isHit = default;
	protected bool isAttack = default;

	public bool IsAttack { get { return isAttack; } set { isAttack = value; } }
	public override void Init()
	{
		base.Init();
	}
	public override void ObjectUpdate()
	{
		if (isAttack)
		{
			SearchHitObjects();
			isHit = _myCollisionObjects.Count > 0;
			if (isHit)
			{
				isAttack = false;
			}
			foreach (CollisionResultData resultData in _myCollisionObjects)
			{
				(resultData.CollisionObjectData as BaseCharacterScript).ReceiveDamage(_myStatus.Attack);
			}
		}
	}
	protected override void SearchHitObjects()
	{
		_myCollisionObjects.Clear();
		CollisionIndexInit();
		_objectManagerScript.GetCollisionCharcter(_myCollisionAreaData, _myCollisionObjects);
	}

	protected override void OnDrawGizmos()
	{
		base.OnDrawGizmos();
		if (isDebugColliderVisible)
		{
			//é©ï™ÇÃìñÇΩÇËîªíË
			//è„
			Gizmos.DrawWireCube(_myCollisionAreaData.Offset
				+ Vector3.up * (_myCollisionAreaData.HalfAreaSize.y + _myCollisionAreaData.HalfAreaWidth)
				, Vector3.right * _myCollisionAreaData.HalfAreaSize.x * 2
				+ Vector3.up * _myCollisionAreaData.AreaWidth
				+ Vector3.forward * _myCollisionAreaData.HalfAreaSize.z * 2);
			//â∫
			Gizmos.DrawWireCube(_myCollisionAreaData.Offset
				+ Vector3.down * (_myCollisionAreaData.HalfAreaSize.y + _myCollisionAreaData.HalfAreaWidth)
				, Vector3.right * _myCollisionAreaData.HalfAreaSize.x * 2
				+ Vector3.up * _myCollisionAreaData.AreaWidth
				+ Vector3.forward * _myCollisionAreaData.HalfAreaSize.z * 2);
			//âE
			Gizmos.DrawWireCube(_myCollisionAreaData.Offset
				+ Vector3.right * (_myCollisionAreaData.HalfAreaSize.x + _myCollisionAreaData.HalfAreaWidth)
				, Vector3.right * _myCollisionAreaData.AreaWidth
				+ Vector3.up * _myCollisionAreaData.HalfAreaSize.y * 2
				+ Vector3.forward * _myCollisionAreaData.HalfAreaSize.z * 2);
			//ç∂
			Gizmos.DrawWireCube(_myCollisionAreaData.Offset
				 + Vector3.left * (_myCollisionAreaData.HalfAreaSize.x + _myCollisionAreaData.HalfAreaWidth)
				, Vector3.right * _myCollisionAreaData.AreaWidth
				+ Vector3.up * _myCollisionAreaData.HalfAreaSize.y * 2
				+ Vector3.forward * _myCollisionAreaData.HalfAreaSize.z * 2);
			//ëO
			Gizmos.DrawWireCube(_myCollisionAreaData.Offset
				+ Vector3.forward * (_myCollisionAreaData.HalfAreaSize.z + _myCollisionAreaData.HalfAreaWidth)
				, Vector3.right * _myCollisionAreaData.HalfAreaSize.x * 2
				+ Vector3.up * _myCollisionAreaData.HalfAreaSize.y * 2
				+ Vector3.forward * _myCollisionAreaData.AreaWidth);
			//å„
			Gizmos.DrawWireCube(_myCollisionAreaData.Offset
				+ Vector3.back * (_myCollisionAreaData.HalfAreaSize.z + _myCollisionAreaData.HalfAreaWidth)
				, Vector3.right * _myCollisionAreaData.HalfAreaSize.x * 2
				+ Vector3.up * _myCollisionAreaData.HalfAreaSize.y * 2
				+ Vector3.forward * _myCollisionAreaData.AreaWidth);
			Gizmos.matrix = _matrixTemp;
		}
	}
}
