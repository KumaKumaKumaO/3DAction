using UnityEngine;

/// <summary>
/// 全てのオブジェクトの元のクラス
/// </summary>
public abstract class BaseObjectScript : MonoBehaviour
{
	[SerializeField]
	private bool isDebugColliderVisible = false;
	[SerializeField]
	protected CollisionData _myCollisionData = default;
	private void Start()
	{
		_myCollisionData.Init(transform);
	}

	private void OnDrawGizmos()
	{
		if (isDebugColliderVisible)
		{
			Gizmos.DrawWireCube(_myCollisionData.MyTransform.position + _myCollisionData.Offset,_myCollisionData.AreaHalfSize * 2);
		}
	}
}
