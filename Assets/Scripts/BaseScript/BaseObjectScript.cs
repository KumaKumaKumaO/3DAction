using UnityEngine;

/// <summary>
/// 全てのオブジェクトの元のクラス
/// </summary>
public abstract class BaseObjectScript : MonoBehaviour
{
	[SerializeField]
	private bool isDebugColliderVisible = false;
	[SerializeField]
	protected bool isGravity = false;
	[SerializeField]
	protected CollisionData _myCollisionData = default;
	protected ObjectManagerScript _objectManagerScript = default;
	[SerializeField]
	protected BaseObjectScript _collisionObjectTemp = default;
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

		if (_collisionObjectTemp == null)
		{
			isGround = false;
			//重力によって落ちるところが当たり判定の境界を貫通しそうか
			if ( _collisionObjectTemp.MyCollisionData.MyTransform.position.y
				+ _collisionObjectTemp.MyCollisionData.Offset.y + _collisionObjectTemp.MyCollisionData.HalfAreaSize.y
				> _myCollisionData.MyTransform.position.y + _myCollisionData.Offset.y - _myCollisionData.HalfAreaSize.y 
				- _objectManagerScript.GravityPower * Time.deltaTime)
			{
				//当たり判定のところに高さを移動する
				_myCollisionData.MyTransform.position -= Vector3.up
					* (_myCollisionData.MyTransform.position.y - _collisionObjectTemp.MyCollisionData.MyTransform.position.y 
					- _collisionObjectTemp.MyCollisionData.MyTransform.position.y
					+ _collisionObjectTemp.MyCollisionData.Offset.y + _collisionObjectTemp.MyCollisionData.HalfAreaSize.y
					+ _myCollisionData.Offset.y + _myCollisionData.HalfAreaSize.y);
			}
			else
			{
				_myCollisionData.MyTransform.position -= Vector3.up * (_objectManagerScript.GravityPower * Time.deltaTime);
			}
			return;
		}
		else
		{
			isGround = true;
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
