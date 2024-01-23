using UnityEngine;

/// <summary>
/// 全てのオブジェクトの元のクラス
/// </summary>
public abstract class BaseObjectScript : MonoBehaviour
{
	[SerializeField]
	protected bool isGravity = false;
	[SerializeField]
	protected CollisionData _myCollisionData = default;
	protected ObjectManagerScript _objectManagerScript = default;
	[Header("デバッグ用")]
	[SerializeField]
	protected BaseObjectScript _collisionObjectTemp = default;
	[SerializeField]
	private bool isDebugColliderVisible = false;
	[SerializeField]
	protected bool isGround = false;

	public CollisionData MyCollisionData { get { return _myCollisionData; } }

	public virtual void Init()
	{
		GameObject objectManagerObject = GameObject.FindWithTag("ObjectManager");
		if (objectManagerObject == null)
		{
			ErrorManagerScript.MyInstance.NullGameObjectError("ObjectManagerのタグがついたオブジェクト");
		}
		else if (!objectManagerObject.TryGetComponent<ObjectManagerScript>(out _objectManagerScript))
		{
			ErrorManagerScript.MyInstance.NullScriptError("ObjectManagerScript");
		}
		_myCollisionData.Init(transform);
		if (!isGravity)
		{
			isGround = true;
		}
	}
	private void Reset()
	{
		gameObject.tag = "Object";
	}

	public virtual void ObjectUpdate()
	{
		if (isGravity)
		{
			GravityFall();
		}
	}
	protected virtual void GravityFall()
	{
		_collisionObjectTemp = _objectManagerScript.GetCollisionObject(_myCollisionData);

		//なにともぶつかっていない
		if (_collisionObjectTemp == null)
		{
			_myCollisionData.MyTransform.position -= Vector3.up * (_objectManagerScript.GravityPower * Time.deltaTime);
			isGround = false;
		}
		//なにかとぶつかった
		else if (!isGround)
		{
			isGround = true;
			//貫通しないように高さを調整する
			_myCollisionData.MyTransform.position += Vector3.up
				* (_myCollisionData.MyTransform.position.y + _myCollisionData.Offset.y - _myCollisionData.HalfAreaSize.y
				- _collisionObjectTemp.MyCollisionData.MyTransform.position.y + _collisionObjectTemp.MyCollisionData.Offset.y
				- _collisionObjectTemp.MyCollisionData.HalfAreaSize.y);
		}
	}

	private void OnDrawGizmos()
	{
		if (isDebugColliderVisible)
		{
			Gizmos.DrawWireCube(transform.position + _myCollisionData.Offset, _myCollisionData.HalfAreaSize * 2);
		}
	}
}
