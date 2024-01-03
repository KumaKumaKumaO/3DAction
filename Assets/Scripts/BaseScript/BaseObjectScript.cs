using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObjectScript : MonoBehaviour
{
	private ObjectManagerScript _objectControllerScript = default;
	[SerializeField]
	private bool isDebugColliderVisible = false;
	[SerializeField]
	private ColliderType _myCollisionType = default;
	[SerializeField]
	private Vector3 _collisionAreaSize = default;
	[SerializeField]
	private Vector3 _collisionAreaOffSetSize = default;
	private Transform _myTransform;
	protected ObjectManagerScript ObjectControllerScript { get { return _objectControllerScript; } }
	public ColliderType MyColisionType { get { return _myCollisionType; } }
	public Vector3 ColisionAreaSize { get { return _collisionAreaSize; } }
	public Vector3 CollisionAreaOffSetSize { get { return _collisionAreaOffSetSize; } }
	public bool IsDebugColliderVisible { get { return isDebugColliderVisible; } }
	public Transform MyTransform { get { return _myTransform; } }
	private void OnEnable()
	{
		GameObject objectControllerObj = GameObject.FindWithTag("ObjectController");
		if (objectControllerObj == null)
		{
			Debug.LogError("ObjectControllerÇ™éÊìæÇ≈Ç´Ç‹ÇπÇÒÇ≈ÇµÇΩÅB");
		}
		_objectControllerScript = objectControllerObj.GetComponent<ObjectManagerScript>();
		_myTransform = transform;
	}
}
