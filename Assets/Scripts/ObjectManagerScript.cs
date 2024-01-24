using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// インスタンスされているオブジェクトを管理するクラス
/// </summary>
public class ObjectManagerScript : MonoBehaviour
{
	[SerializeField]
	private float _grivityPower = 9.8f;
	private List<StageFloorScript> _stageFloors = new List<StageFloorScript>();
	private List<BaseStageObjectScript> _stageObjects = new List<BaseStageObjectScript>();
	private List<BaseCharcterScript> _charcterObjects = new List<BaseCharcterScript>();
	private CollisionSystem _collisionSystem = new CollisionSystem();
	private CollisionResultData collisionResultDataTemp = default;

	public float GravityPower { get { return _grivityPower; } }

	public void Init(InGamePlayerInput playerInput)
	{
		BaseObjectScript baseObjectScriptTemp = default;
		foreach (GameObject item in GameObject.FindGameObjectsWithTag("Object"))
		{
			baseObjectScriptTemp = item.GetComponent<BaseObjectScript>();
			if (baseObjectScriptTemp != null)
			{
				AddMyObject(baseObjectScriptTemp);
			}
			else
			{
				ErrorManagerScript.MyInstance.NullScriptError("BaseObjectScript");
			}
		}
		AllObjectInit(playerInput);
	}

	private void AllObjectInit(InGamePlayerInput input)
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
			if (item is PlayerCharcterScript playerScript)
			{
				playerScript.SetPlayerInput(input);
			}
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


	public void GetCollisionObject(CollisionAreaData charcterColAreaData,List<CollisionResultData> collisionObjectDatas)
	{
		collisionObjectDatas.Clear();
        foreach (BaseObjectScript item in _stageFloors)
        {
			AddCollisionObject(charcterColAreaData, item, collisionObjectDatas);
		}
		foreach (BaseObjectScript item in _stageObjects)
        {
			AddCollisionObject(charcterColAreaData, item, collisionObjectDatas);

		}
		foreach (BaseObjectScript item in _charcterObjects)
        {
			AddCollisionObject(charcterColAreaData, item, collisionObjectDatas);
		}
    }

	private void AddCollisionObject(CollisionAreaData colData,BaseObjectScript checkObject
		, List<CollisionResultData> collisionObjects)
    {
		if (colData.MyTransform != checkObject.MyCollisionData.MyTransform)
		{
			collisionResultDataTemp = _collisionSystem.GetCollisionResult(colData, checkObject);
			if (collisionResultDataTemp.IsCollision)
			{
				collisionObjects.Add(collisionResultDataTemp);
			}
		}
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
