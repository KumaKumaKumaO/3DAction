using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManagerScript : MonoBehaviour
{
	private List<StageFloorScript> _stageFloors = new List<StageFloorScript>();
	private List<BaseStageObjectScript> _stageObjects = new List<BaseStageObjectScript>();
	private List<BaseCharcterScript> _charcterObjects = new List<BaseCharcterScript>();
	private CollisionSystem _collisionSystem = new CollisionSystem();

	private bool IsCollisionFloor(CollisionData charcterColData)
	{
		foreach (StageFloorScript item in _stageFloors)
		{
			if(_collisionSystem.IsCollision(charcterColData, item.MyCollisionData))
			{
				return true;
			}
		}
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
