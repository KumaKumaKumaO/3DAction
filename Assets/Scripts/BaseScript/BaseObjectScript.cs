using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 全てのオブジェクトの元のクラス
/// </summary>
public abstract class BaseObjectScript : MonoBehaviour
{
	[SerializeField]
	protected bool isGravity = false;
	[SerializeField]
	protected CollisionAreaData _myCollisionData = default;
	protected ObjectManagerScript _objectManagerScript = default;
	protected List<CollisionResultData> _myCollisionObjects = new List<CollisionResultData>();
	[Header("デバッグ用")]
	[SerializeField]
	private bool isDebugColliderVisible = false;
	[SerializeField]
	protected bool isGround = false;

	public CollisionAreaData MyCollisionData { get { return _myCollisionData; } }

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
		 _objectManagerScript.GetCollisionObject(_myCollisionData,_myCollisionObjects);
		if(_myCollisionObjects.Count <= 0)
		{
			_myCollisionData.MyTransform.position -= Vector3.up * (_objectManagerScript.GravityPower * Time.deltaTime);
			isGround = false;
			return;
		}
		

		//下側が衝突しているか
		//側面が衝突しているか
		//上面が衝突しているか

		//下側の中でも一番上で衝突しているオブジェクトの上に着地する
	}

	private bool SortHigherPosColObjectResult()
	{
		bool isSorted = false;
        for (int i = 1;i < _myCollisionObjects.Count ;i++)
        {
            if (_myCollisionObjects[i].IsCollisionBottom)
            {
				//上辺を比較する

            }
        }
		return isSorted; 
	}


	protected virtual void Landing(BaseObjectScript baseObjectScript)
	{
		isGround = true;
		//貫通しないように高さを調整する
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
