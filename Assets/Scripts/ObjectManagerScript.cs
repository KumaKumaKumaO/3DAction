using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManagerScript : MonoBehaviour
{
	[SerializeField]
	private float _grivityPower = 9.8f;
	private List<StageFloorScript> _stageFloors = new List<StageFloorScript>();
	private List<BaseStageObjectScript> _stageObjects = new List<BaseStageObjectScript>();
	private List<BaseCharcterScript> _charcterObjects = new List<BaseCharcterScript>();
	private CollisionSystem _collisionSystem = new CollisionSystem();
	
	public float GravityPower { get { return _grivityPower; } }

	public void Init()
	{
		foreach(GameObject item in GameObject.FindGameObjectsWithTag("Object"))
		{
			AddMyObject(item.GetComponent<BaseObjectScript>());
		}
		AllObjectInit();
	}

	private void AllObjectInit()
	{
		foreach (StageFloorScript item in _stageFloors)
		{
			item.Init();
		}
		foreach (BaseStageObjectScript item in _stageObjects)
		{
			item.Init();
		}
		foreach (BaseCharcterScript item in _charcterObjects)
		{
			item.Init();
		}
	}
	public void AllObjectUpdate()
	{
		foreach(StageFloorScript item in _stageFloors)
		{
			item.ObjectUpdate();
		}
		foreach(BaseStageObjectScript item in _stageObjects)
		{
			item.ObjectUpdate();
		}
		foreach(BaseCharcterScript item in _charcterObjects)
		{
			item.ObjectUpdate();
		}

	}


	public BaseObjectScript GetCollisionObject(CollisionData charcterColData)
	{
		foreach (StageFloorScript item in _stageFloors)
		{
			if(_collisionSystem.IsCollision(charcterColData, item.MyCollisionData) 
				&& charcterColData.MyTransform != item.MyCollisionData.MyTransform)
			{
				return item;
			}
		}
		foreach(BaseStageObjectScript item in _stageObjects)
		{
			if (_collisionSystem.IsCollision(charcterColData, item.MyCollisionData)
				&& charcterColData.MyTransform != item.MyCollisionData.MyTransform)
			{
				return item;
			}
		}
		foreach(BaseCharcterScript item in _charcterObjects)
		{
			if (_collisionSystem.IsCollision(charcterColData, item.MyCollisionData)
				&& charcterColData.MyTransform != item.MyCollisionData.MyTransform)
			{
				return item;
			}
		}
		return null;
	}


	public void AddMyObject(BaseObjectScript obj)
	{
		if (obj is StageFloorScript stageFloor)
		{
			Debug.Log("ADDStageFloor:" + obj.name);
			_stageFloors.Add(stageFloor);
		}
		else if (obj is BaseStageObjectScript stageObj)
		{
			Debug.Log("ADDStageObj:" + obj.name);
			_stageObjects.Add(stageObj);
		}
		else if (obj is BaseCharcterScript charcterObj)
		{
			Debug.Log("ADDCharcter:" + obj.name);
			_charcterObjects.Add(charcterObj);
		}
		else
		{
			ErrorManagerScript.MyInstance.CantExistObject(obj.name);
		}
	}
}
