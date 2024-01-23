using UnityEngine;

/// <summary>
/// �S�ẴI�u�W�F�N�g�̌��̃N���X
/// </summary>
public abstract class BaseObjectScript : MonoBehaviour
{
	[SerializeField]
	protected bool isGravity = false;
	[SerializeField]
	protected CollisionData _myCollisionData = default;
	protected ObjectManagerScript _objectManagerScript = default;
	[Header("�f�o�b�O�p")]
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
			ErrorManagerScript.MyInstance.NullGameObjectError("ObjectManager�̃^�O�������I�u�W�F�N�g");
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

		//�ȂɂƂ��Ԃ����Ă��Ȃ�
		if (_collisionObjectTemp == null)
		{
			_myCollisionData.MyTransform.position -= Vector3.up * (_objectManagerScript.GravityPower * Time.deltaTime);
			isGround = false;
		}
		//�Ȃɂ��ƂԂ�����
		else if (!isGround)
		{
			isGround = true;
			//�ђʂ��Ȃ��悤�ɍ����𒲐�����
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
