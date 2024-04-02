using UnityEngine;

/// <summary>
/// 武器の基本クラス
/// </summary>
public class BaseWeaponScript : BaseObjectScript
{
	[SerializeField]
	protected bool isHit = default;
	[SerializeField]
	protected bool isAttacking = default;
	[SerializeField]
	protected WeaponStatus _myStatus = default;

	public bool IsAttacking { get { return isAttacking; } set { isAttacking = value; } }


	public override void ObjectUpdate()
	{
		if (isAttacking && !IsDestroyObject)
		{
			CollisionIndexInit();
			_myCollisionObjects.Clear();
			_objectManagerScript.GetCollisionCharcter(_myCollisionAreaData,_myCollisionObjects);
			isHit = _myCollisionObjects.Count > 0;
			if (isHit)
			{
				isAttacking = false;
			}
			foreach (BaseObjectScript resultData in _myCollisionObjects)
			{
				(resultData as BaseCharacterScript)
					.ReceiveDamage(_myStatus.Attack,_myStatus.StaggerValue);
			}
		}
	}

#if UNITY_EDITOR
	protected override void OnDrawGizmos()
	{
		base.OnDrawGizmos();
		if (isDebugColliderVisible)
		{
			if (isAttacking)
			{
				Gizmos.color = Color.red;
			}
			else
			{
				Gizmos.color = Color.white;
			}
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
			Gizmos.DrawWireCube(_myCollisionAreaData.Offset
				+ Vector3.right * (_myCollisionAreaData.HalfAreaSize.x + _myCollisionAreaData.HalfAreaWidth)
				, Vector3.right * _myCollisionAreaData.AreaWidth
				+ Vector3.up * _myCollisionAreaData.HalfAreaSize.y * 2
				+ Vector3.forward * _myCollisionAreaData.HalfAreaSize.z * 2);
			//左
			Gizmos.DrawWireCube(_myCollisionAreaData.Offset
				 + Vector3.left * (_myCollisionAreaData.HalfAreaSize.x + _myCollisionAreaData.HalfAreaWidth)
				, Vector3.right * _myCollisionAreaData.AreaWidth
				+ Vector3.up * _myCollisionAreaData.HalfAreaSize.y * 2
				+ Vector3.forward * _myCollisionAreaData.HalfAreaSize.z * 2);
			//前
			Gizmos.DrawWireCube(_myCollisionAreaData.Offset
				+ Vector3.forward * (_myCollisionAreaData.HalfAreaSize.z + _myCollisionAreaData.HalfAreaWidth)
				, Vector3.right * _myCollisionAreaData.HalfAreaSize.x * 2
				+ Vector3.up * _myCollisionAreaData.HalfAreaSize.y * 2
				+ Vector3.forward * _myCollisionAreaData.AreaWidth);
			//後
			Gizmos.DrawWireCube(_myCollisionAreaData.Offset
				+ Vector3.back * (_myCollisionAreaData.HalfAreaSize.z + _myCollisionAreaData.HalfAreaWidth)
				, Vector3.right * _myCollisionAreaData.HalfAreaSize.x * 2
				+ Vector3.up * _myCollisionAreaData.HalfAreaSize.y * 2
				+ Vector3.forward * _myCollisionAreaData.AreaWidth);
			Gizmos.matrix = _matrixTemp;
		}
	}
#endif
}
