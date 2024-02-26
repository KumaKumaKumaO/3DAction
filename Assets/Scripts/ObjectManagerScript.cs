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
	[SerializeField]
	private UIManagerScript _uiManagerScript = default;

	private List<StageFloorScript> _stageFloors = new List<StageFloorScript>();
	private List<BaseStageObjectScript> _stageObjects = new List<BaseStageObjectScript>();
	private List<BaseCharacterScript> _charcterObjects = new List<BaseCharacterScript>();
	private List<BaseWeaponScript> _weaponObjects = new List<BaseWeaponScript>();
	private CollisionSystem _collisionSystem = new CollisionSystem();
	private CameraScript _cameraScript = default;
	private BaseCharacterScript _playerCharcterScript = default;
	

	public float CameraSpeed { get { return _cameraSpeed; } }
	public CameraScript CameraScript { get { return _cameraScript; } }
	public float GravityPower { get { return _grivityPower; } }
	public BaseCharacterScript PlayerCharcterScript
	{
		get
		{
			if (_playerCharcterScript == null)
			{
				foreach (GameObject item in GameObject.FindGameObjectsWithTag("Object"))
				{
					if (item.TryGetComponent<PlayerCharacterScript>(out PlayerCharacterScript playerCharcterScript))
					{
						this._playerCharcterScript = playerCharcterScript;
					}
				}
				if (_playerCharcterScript == null)
				{
					foreach (GameObject item in GameObject.FindGameObjectsWithTag("Object"))
					{
						if (item.TryGetComponent<BaseCharacterScript>(out BaseCharacterScript charcterScript) && charcterScript.IsDebugInputPlayer)
						{
							this._playerCharcterScript = charcterScript;
						}
					}
				}
			}
			return _playerCharcterScript;
		}
	}

	public void Delete()
	{
		DeleteListData(_stageFloors);
		DeleteListData(_stageObjects);
		DeleteListData(_weaponObjects);
		DeleteListData(_charcterObjects);
		_stageFloors = null;
		_stageObjects = null;
		_weaponObjects = null;
		_charcterObjects = null;

		_cameraScript.Delete();
		_cameraScript = null;
		Destroy(this);
	}

	private void DeleteListData<T>(T _objectList) where T : IList
	{
		for (int i = 0; i < _objectList.Count; i++)
		{
			if (_objectList[i] is BaseObjectScript obj)
			{
				obj.Delete();
			}
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
				AddObject(baseObjectScriptTemp);
			}
			else
			{
				ErrorManagerScript.MyInstance.NullScriptError("BaseObjectScript");
			}
		}
		_cameraScript = new CameraScript(playerInput);
		AllObjectInit(playerInput);
		_uiManagerScript.PlayerUIInit(_playerCharcterScript.MyCharcterStatus);
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
			UICanInitCheck(item);
			
		}
		foreach (BaseWeaponScript item in _weaponObjects)
		{
			item.ObjectUpdate();
		}
		_cameraScript.UpdateCameraControl();
	}

	private void UICanInitCheck(BaseCharacterScript characterScript)
	{
		if (characterScript.IsActive)
		{
			if (characterScript.IsBoss)
			{
				_uiManagerScript.BossUIInit(characterScript);
				return;
			}
			_uiManagerScript.EnemyUIInit(characterScript);
		}
	}

	public void GetCollisionAllObject(CollisionAreaData charcterColAreaData, List<BaseObjectScript> collisionObjectDatas
		, MoveDirection moveDirection)
	{
		GetCollisionFloor(charcterColAreaData, collisionObjectDatas,moveDirection);
		GetCollisionStageObject(charcterColAreaData, collisionObjectDatas,moveDirection);
		GetCollisionCharcter(charcterColAreaData, collisionObjectDatas);
	}

	public void GetCollisionCharcter(CollisionAreaData charcterColAreaData, List<BaseObjectScript> collisionObjectDatas)
	{
		foreach (BaseObjectScript item in _charcterObjects)
		{
			if (charcterColAreaData.MyTransform.root == item.MyCollisionAreaData.MyTransform
			|| (item is BaseCharacterScript charcterScript && !charcterScript.CanCollision)) { continue; }

			if (_collisionSystem.IsCollision(charcterColAreaData,item.MyCollisionAreaData))
			{
				collisionObjectDatas.Add(item);
			}
		}
	}

	public void GetCollisionFloor(CollisionAreaData charcterColAreaData, List<BaseObjectScript> collisionObjectDatas
		,MoveDirection moveDirection)
	{
		foreach (BaseObjectScript item in _stageFloors)
		{
			if (charcterColAreaData.MyTransform == item.MyCollisionAreaData.MyTransform) { continue; }

			AddCollisionObject(charcterColAreaData, item, collisionObjectDatas,moveDirection);
		}
	}

	public void GetCollisionStageObject(CollisionAreaData colAreaData, List<BaseObjectScript> collisionObjectDatas
		,MoveDirection moveDirection)
	{
		foreach (BaseObjectScript item in _stageObjects)
		{
			AddCollisionObject(colAreaData, item, collisionObjectDatas,moveDirection);
		}
	}

	private void AddCollisionObject(CollisionAreaData colData, BaseObjectScript targetObject
		, List<BaseObjectScript> collisionObjects,MoveDirection moveDirection)
	{
		if (_collisionSystem.IsCollision(colData,targetObject.MyCollisionAreaData,moveDirection))
		{
			//Debug.Log(moveDirection + ":" + targetObject.name);
			collisionObjects.Add(targetObject);
		}
	}

	public bool IsCollisionObject(CollisionAreaData myData, BaseObjectScript targetObject
		,MoveDirection moveDirection)
	{
		return _collisionSystem.IsCollision(myData, targetObject.MyCollisionAreaData,moveDirection);
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

	public bool IsCollisionCharcter(BaseCharacterScript targetCharcterScript)
	{
		foreach (BaseObjectScript item in _charcterObjects)
		{
			if (item as BaseCharacterScript == targetCharcterScript)
			{
				return true;
			}
		}
		return false;
	}

	public void AddObject(BaseObjectScript obj)
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

	public void SubtractObject(BaseObjectScript obj)
	{
		if (obj is StageFloorScript stageFloor)
		{
			_stageFloors.Remove(stageFloor);
		}
		else if (obj is BaseStageObjectScript stageObj)
		{
			_stageObjects.Remove(stageObj);
		}
		else if (obj is BaseCharacterScript charcterObj)
		{
			_charcterObjects.Remove(charcterObj);
			if (charcterObj is PlayerCharacterScript)
			{
				_playerCharcterScript = null;
			}
		}
		else if (obj is BaseWeaponScript weaponObj)
		{
			_weaponObjects.Remove(weaponObj);
		}
		else
		{
			ErrorManagerScript.MyInstance.CantExistObject(obj.name);
		}
	}
}
