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
	protected CollisionAreaData _myCollisionAreaData = default;
	protected List<CollisionResultData> _myCollisionObjects = new List<CollisionResultData>();
	[SerializeField]
	protected int _bottomCollisionIndex = -1;
	[SerializeField]
	protected int _topCollisionAreaDataIndex = -1;
	[SerializeField]
	protected int _rightCollisionAreaDataIndex = -1;
	[SerializeField]
	protected int _leftCollisionAreaDataIndex = -1;
	[SerializeField]
	protected int _forwardCollisionAreaDataIndex = -1;
	[SerializeField]
	protected int _backCollisionAreaDataIndex = -1;
	protected ObjectManagerScript _objectManagerScript = default;
	protected Matrix4x4 _matrixTemp = default;
	[SerializeField]
	protected bool isGround = false;
	protected Transform _myTransform = default;
	protected int _forwardCollisionCount = 0;
	private bool isDestroyObject = false;


	[SerializeField,Tooltip("�f�o�b�O�p")]
	protected bool isDebugColliderVisible = false;
	public CollisionAreaData MyCollisionAreaData { get { return _myCollisionAreaData; } }
	public Transform MyTransform { get { return _myTransform; } }
	public bool IsGround { get { return isGround; } }
	public bool IsDestroyObject { get { return isDestroyObject; } }
	public virtual void Init()
	{
		_myTransform = this.transform;
		GameObject objectManagerObject = GameObject.FindWithTag("ObjectManager");
		if (objectManagerObject == null)
		{
			ErrorManagerScript.MyInstance.NullGameObjectError("ObjectManager�̃^�O�������I�u�W�F�N�g");
		}
		else if (!objectManagerObject.TryGetComponent<ObjectManagerScript>(out _objectManagerScript))
		{
			ErrorManagerScript.MyInstance.NullScriptError("ObjectManagerScript");
		}
		_myCollisionAreaData.Init(transform);
	}
	public virtual void ObjectUpdate()
	{
		if (isGravity)
		{
			GravityFall();
		}

	}
	//���̌����ɑ΂���
	protected void SelectColObjectResult()
	{
		for (int i = 0; i < _myCollisionObjects.Count; i++)
		{
			if (_myCollisionObjects[i].IsCollisionBottom)
			{
				if (_bottomCollisionIndex < 0
					|| !(_myCollisionObjects[_bottomCollisionIndex].ObjectData.MyCollisionAreaData.TopYPos
					< _myCollisionObjects[i].ObjectData.MyCollisionAreaData.TopYPos))
				{
					_bottomCollisionIndex = i;
				}
			}
			if (_myCollisionObjects[i].IsCollisionTop)
			{
				if (_topCollisionAreaDataIndex < 0
					|| _myCollisionObjects[_topCollisionAreaDataIndex].ObjectData.MyCollisionAreaData.BottomYPos
					> _myCollisionObjects[i].ObjectData.MyCollisionAreaData.BottomYPos)
				{
					_topCollisionAreaDataIndex = i;
				}
			}

			if (_myCollisionObjects[i].IsCollisionRight)
			{
				if (_rightCollisionAreaDataIndex < 0
					|| _myCollisionObjects[_rightCollisionAreaDataIndex].ObjectData.MyCollisionAreaData.LeftXPos
					> _myCollisionObjects[i].ObjectData.MyCollisionAreaData.LeftXPos)
				{
					_rightCollisionAreaDataIndex = i;
				}
			}
			else if (_myCollisionObjects[i].IsCollisionLeft)
			{
				if (_leftCollisionAreaDataIndex < 0
					|| _myCollisionObjects[_leftCollisionAreaDataIndex].ObjectData.MyCollisionAreaData.RightXPos
					< _myCollisionObjects[i].ObjectData.MyCollisionAreaData.RightXPos)
				{
					_leftCollisionAreaDataIndex = i;
				}
			}
			//���̂��猩�đO���Ƀq�b�g���Ă���
			if (_myCollisionObjects[i].IsCollisionForward)
			{
				_forwardCollisionCount += 1;
				if (_forwardCollisionAreaDataIndex < 0
					//
					|| _myCollisionObjects[_forwardCollisionAreaDataIndex].ObjectData.MyCollisionAreaData.BackZPos
					> _myCollisionObjects[i].ObjectData.MyCollisionAreaData.BackZPos)
				{
					_forwardCollisionAreaDataIndex = i;
				}
			}
			else if (_myCollisionObjects[i].IsCollisionBack)
			{
				if (_backCollisionAreaDataIndex < 0
					|| _myCollisionObjects[_backCollisionAreaDataIndex].ObjectData.MyCollisionAreaData.ForwardZPos
					< _myCollisionObjects[i].ObjectData.MyCollisionAreaData.ForwardZPos)
				{
					_backCollisionAreaDataIndex = i;
				}
			}
		}
	}

	/// <summary>
	/// �|�����o�Ȃǂ��I��������Ƃ̏���
	/// </summary>
	public virtual void Delete()
	{
		if(_myCollisionObjects is not null)
		{
			_myCollisionObjects.Clear();
			_myCollisionObjects = null;
		}
		if(_objectManagerScript is not null)
		{
			_objectManagerScript.SubtractObject(this);
			_objectManagerScript = null;
		}
		if(_myTransform is not null)
		{
			_myTransform = null;
			_myCollisionAreaData.Delete();
		}

		if(isDestroyObject) { return; }
		Destroy(gameObject);
	}
	protected virtual void Reset()
	{
		gameObject.tag = "Object";
	}
	protected void CollisionIndexInit()
	{
		_bottomCollisionIndex = -1;
		_topCollisionAreaDataIndex = -1;
		_rightCollisionAreaDataIndex = -1;
		_leftCollisionAreaDataIndex = -1;
		_forwardCollisionAreaDataIndex = -1;
		_backCollisionAreaDataIndex = -1;
		_forwardCollisionCount = 0;
	}
	/// <summary>
	/// �I�u�W�F�N�g���ړ�����
	/// </summary>
	/// <param name="vector">���E���̈ړ��x�N�g��</param>
	public virtual void ObjectMove(Vector3 vector)
	{
		Vector3 beforPos = _myCollisionAreaData.MyTransform.position;
		_myCollisionAreaData.MyTransform.position += vector;
		SearchHitObjects();

		_myCollisionAreaData.MyTransform.position
			= GetClampVector(_myCollisionAreaData.MyTransform.position - beforPos);
	}
	protected virtual void SearchHitObjects()
	{
		CollisionIndexInit();
		_myCollisionObjects.Clear();
		_objectManagerScript.GetCollisionAllObject(_myCollisionAreaData, _myCollisionObjects);
		SelectColObjectResult();
	}

	/// <summary>
	/// ���蔲�����C������
	/// </summary>
	/// <param name="moveVector">�ړ������x�N�g��</param>
	/// <returns>�C����̃|�W�V����</returns>
	public virtual Vector3 GetClampVector(Vector3 moveVector)
	{
		//�ړ���̈ʒu����
		Vector3 returnValue = _myCollisionAreaData.MyTransform.position;

		//�㉺�ɓ����Ă��邩�̊m�F
		//���Ɉړ����Ă���ꍇ
		if (moveVector.y < 0)
		{
			if (_bottomCollisionIndex >= 0)
			{
				returnValue += Vector3.up * (_myCollisionObjects[_bottomCollisionIndex]
					.ObjectData.MyCollisionAreaData.TopYPos - _myCollisionAreaData.BottomYPos
					+ _myCollisionAreaData.AreaWidth);
			}
		}
		//��Ɉړ����Ă���ꍇ
		else if (moveVector.y > 0)
		{
			if (_forwardCollisionAreaDataIndex >= 0)
			{
				returnValue += Vector3.up * (_myCollisionObjects[_topCollisionAreaDataIndex]
					.ObjectData.MyCollisionAreaData.BottomYPos - _myCollisionAreaData.TopYPos
					+ _myCollisionAreaData.AreaWidth);
			}
		}
		return returnValue;
	}

	protected virtual void GravityFall()
	{
		SearchHitObjects();
		if (_bottomCollisionIndex < 0)
		{
			ObjectMove(Vector3.down * (_objectManagerScript.GravityPower * Time.deltaTime));
			isGround = false;
		}
		else
		{
			isGround = true;
		}
	}

	protected virtual void OnDrawGizmos()
	{
		if (isDebugColliderVisible)
		{
			_matrixTemp = Gizmos.matrix;
			Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
		}
	}

	private void OnDestroy()
	{
		isDestroyObject = true;
		//Debug.LogWarning("Destroy:" + gameObject.name);
	}
}
