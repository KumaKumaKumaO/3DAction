using UnityEngine;
using System.Collections.Generic;

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
	protected List<BaseObjectScript> _myCollisionObjects = new List<BaseObjectScript>();
	[Header("�f�o�b�O�p")]
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
		 _objectManagerScript.GetCollisionObject(_myCollisionData,_myCollisionObjects);
		if(_myCollisionObjects.Count <= 0)
		{
			_myCollisionData.MyTransform.position -= Vector3.up * (_objectManagerScript.GravityPower * Time.deltaTime);
			isGround = false;
			return;
		}else if (!isGround)
		{
			isGround = true;
		}

		//�������Փ˂��Ă��邩
		//���ʂ��Փ˂��Ă��邩
		//��ʂ��Փ˂��Ă��邩

		//�����̒��ł���ԏ�ŏՓ˂��Ă���I�u�W�F�N�g�̏�ɒ��n����
	}

	private void CollisionBottomData()
	{
		//if(_myCollisionData.BottomYPos )
	}


	protected virtual void Landing(BaseObjectScript baseObjectScript)
	{
		isGround = true;
		//�ђʂ��Ȃ��悤�ɍ����𒲐�����
		_myCollisionData.MyTransform.position += Vector3.up
			* (_myCollisionData.MyTransform.position.y + _myCollisionData.Offset.y - _myCollisionData.HalfAreaSize.y
			- baseObjectScript.MyCollisionData.MyTransform.position.y + baseObjectScript.MyCollisionData.Offset.y 
			- baseObjectScript.MyCollisionData.HalfAreaSize.y);
	}


	private void OnDrawGizmos()
	{
		if (isDebugColliderVisible)
		{
			Gizmos.DrawWireCube(transform.position + _myCollisionData.Offset, _myCollisionData.HalfAreaSize * 2);
		}
	}
}
