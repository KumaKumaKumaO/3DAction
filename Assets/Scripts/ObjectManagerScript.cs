using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManagerScript
{
	private List<StageFloorScript> _stageFloors = new List<StageFloorScript>();
	private List<BaseStageObjectScript> _stageObjects = new List<BaseStageObjectScript>();
	private List<BaseCharcterScript> _charcterObjects = new List<BaseCharcterScript>();
	private CollisionSystem _collisionSystem = new CollisionSystem();
	private static ObjectManagerScript _myInstance = default;


	public static ObjectManagerScript MyInstance { get { return _myInstance; } }

	public void InstantiationMyInstance()
	{
		if (_myInstance == null)
		{
			_myInstance = this;
		}
		else
		{
			ErrorManagerScript.MyInstance.SingleTonError(this.GetType().Name);
		}
	}

	private bool isCollisionFloor(CollisionData charcterColData)
	{
		return false;
	}


	public void AddMyObject(BaseObjectScript obj)
	{
		if (obj is StageFloorScript stageFloor)
		{
			_stageFloors.Add(stageFloor);
		}
		else if (obj is BaseStageObjectScript stageObj)
		{
			_stageObjects.Add(stageObj);
		}
		else if (obj is BaseCharcterScript charcterObj)
		{
			_charcterObjects.Add(charcterObj);
		}
	}
}
