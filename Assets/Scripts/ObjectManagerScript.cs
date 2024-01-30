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
	private List<BaseWeaponScript> _weaponObjects = new List<BaseWeaponScript>();
	private CollisionSystem _collisionSystem = new CollisionSystem();
	private CollisionResultData collisionResultDataTemp = default;
	private CameraScript _cameraScript = default;

	public CameraScript CameraScript { get { return _cameraScript; } }

	public float GravityPower { get { return _grivityPower; } }
	public PlayerCharcterScript PlayerCharcter
	{
		get
		{
			foreach (BaseCharcterScript item in _charcterObjects)
			{
				if (item is PlayerCharcterScript playerCharcterScript)
				{
					return playerCharcterScript;
				}
			}
			foreach (GameObject item in GameObject.FindGameObjectsWithTag("Object"))
			{
				if (item.TryGetComponent<PlayerCharcterScript>(out PlayerCharcterScript playerCharcterScript))
				{
					return playerCharcterScript;
				}
			}
			return null;
		}
	}

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
		_cameraScript = new CameraScript(playerInput);
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
		foreach (BaseWeaponScript item in _weaponObjects)
		{
			item.Init();
		}
	}
	public void AllObjectUpdate()
	{
		foreach (StageFloorScript item in _stageFloors)
		{
			item.ObjectUpdate();
		}
		foreach (BaseStageObjectScript item in _stageObjects)
		{
			item.ObjectUpdate();
		}
		foreach (BaseCharcterScript item in _charcterObjects)
		{
			item.ObjectUpdate();
		}
		foreach (BaseWeaponScript item in _weaponObjects)
		{
			item.ObjectUpdate();
		}
		_cameraScript.UpdateCameraControl();
	}


	public void GetCollisionAllObject(CollisionAreaData charcterColAreaData, List<CollisionResultData> collisionObjectDatas)
	{
		GetCollisionFloor(charcterColAreaData, collisionObjectDatas);
		GetCollisionObject(charcterColAreaData, collisionObjectDatas);
		GetCollisionCharcter(charcterColAreaData, collisionObjectDatas);
	}

	public void GetCollisionCharcter(CollisionAreaData charcterColAreaData, List<CollisionResultData> collisionObjectDatas)
	{
		foreach (BaseObjectScript item in _charcterObjects)
		{
			AddCollisionObject(charcterColAreaData, item, collisionObjectDatas);
		}
	}

	public void GetCollisionFloor(CollisionAreaData charcterColAreaData, List<CollisionResultData> collisionObjectDatas)
	{
		foreach (BaseObjectScript item in _stageFloors)
		{
			AddCollisionObject(charcterColAreaData, item, collisionObjectDatas);
		}
	}
	public void GetCollisionObject(CollisionAreaData charcterColAreaData, List<CollisionResultData> collisionObjectDatas)
	{
		foreach (BaseObjectScript item in _stageObjects)
		{
			AddCollisionObject(charcterColAreaData, item, collisionObjectDatas);

		}
	}

	private void AddCollisionObject(CollisionAreaData colData, BaseObjectScript checkObject
		, List<CollisionResultData> collisionObjects)
	{
		if (colData.MyTransform != checkObject.MyCollisionAreaData.MyTransform)
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
		else if (obj is BaseWeaponScript weaponObj)
		{
			_weaponObjects.Add(weaponObj);
		}
		else
		{
			ErrorManagerScript.MyInstance.CantExistObject(obj.name);
		}
	}
}
