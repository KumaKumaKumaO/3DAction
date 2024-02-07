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
	[SerializeField]
	private float _cameraSpeed = default;

	private List<StageFloorScript> _stageFloors = new List<StageFloorScript>();
	private List<BaseStageObjectScript> _stageObjects = new List<BaseStageObjectScript>();
	private List<BaseCharacterScript> _charcterObjects = new List<BaseCharacterScript>();
	private List<BaseWeaponScript> _weaponObjects = new List<BaseWeaponScript>();
	private CollisionSystem _collisionSystem = new CollisionSystem();
	private CollisionResultData collisionResultDataTemp = default;
	private CameraScript _cameraScript = default;
	private PlayerCharacterScript _playerCharcterScript = default;

	public float CameraSpeed { get { return _cameraSpeed; } }
	public CameraScript CameraScript { get { return _cameraScript; } }
	public float GravityPower { get { return _grivityPower; } }
	public PlayerCharacterScript PlayerCharcterScript
	{
		get
		{
			if (_playerCharcterScript != null)
			{
				return _playerCharcterScript;
			}
			foreach (GameObject item in GameObject.FindGameObjectsWithTag("Object"))
			{
				if (item.TryGetComponent<PlayerCharacterScript>(out PlayerCharacterScript playerCharcterScript))
				{
					this._playerCharcterScript = playerCharcterScript;
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
		foreach (BaseCharacterScript item in _charcterObjects)
		{
			item.Init();
			if (item is PlayerCharacterScript playerScript)
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
		foreach (BaseCharacterScript item in _charcterObjects)
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
		GetCollisionStageObject(charcterColAreaData, collisionObjectDatas);
		GetCollisionCharcter(charcterColAreaData, collisionObjectDatas);
	}

	public void GetCollisionCharcter(CollisionAreaData charcterColAreaData, List<CollisionResultData> collisionObjectDatas)
	{
		foreach (BaseObjectScript item in _charcterObjects)
		{
			if (charcterColAreaData.MyTransform.root == item.MyCollisionAreaData.MyTransform)
			{
				continue;
			}
			AddCollisionObject(charcterColAreaData, item, collisionObjectDatas);
		}
	}

	public void GetCollisionFloor(CollisionAreaData charcterColAreaData, List<CollisionResultData> collisionObjectDatas)
	{
		foreach (BaseObjectScript item in _stageFloors)
		{
			if (charcterColAreaData.MyTransform == item.MyCollisionAreaData.MyTransform)
			{
				continue;
			}
			AddCollisionObject(charcterColAreaData, item, collisionObjectDatas);
		}
	}
	public void GetCollisionStageObject(CollisionAreaData colAreaData, List<CollisionResultData> collisionObjectDatas)
	{
		foreach (BaseObjectScript item in _stageObjects)
		{
			AddCollisionObject(colAreaData, item, collisionObjectDatas);
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

	public CollisionResultData CollisionObject(CollisionAreaData myData, BaseObjectScript targetObject)
	{
		return _collisionSystem.GetCollisionResult(myData, targetObject);
	}

	public BaseWeaponScript GetMyWeapon(BaseCharacterScript myData)
	{
		foreach (BaseWeaponScript item in _weaponObjects)
		{
			if (item.transform.root == myData.transform)
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
			_stageFloors.Add(stageFloor);
		}
		else if (obj is BaseStageObjectScript stageObj)
		{
			_stageObjects.Add(stageObj);
		}
		else if (obj is BaseCharacterScript charcterObj)
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
