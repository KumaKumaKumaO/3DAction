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
#if UNITY_EDITOR
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
#endif
			}
			return _playerCharcterScript;
		}
	}

	/// <summary>
	/// 削除処理
	/// </summary>
	public void Delete()
	{
		DeleteListData(_stageFloors);
		DeleteListData(_weaponObjects);
		DeleteListData(_charcterObjects);
		_stageFloors = null;
		_weaponObjects = null;
		_charcterObjects = null;

		_cameraScript.Delete();
		_cameraScript = null;
		Destroy(this);
	}

	/// <summary>
	/// リストの中身に削除処理を呼び出す
	/// </summary>
	/// <typeparam name="T">リスト</typeparam>
	/// <param name="_objectList">オブジェクトが入っているリスト</param>
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

	/// <summary>
	/// 初期化
	/// </summary>
	/// <param name="playerInput">プレイヤー入力</param>
	public void Init(InGamePlayerInput playerInput)
	{
		BaseObjectScript baseObjectScriptTemp;
		foreach (GameObject item in GameObject.FindGameObjectsWithTag("Object"))
		{
			baseObjectScriptTemp = item.GetComponent<BaseObjectScript>();
#if UNITY_EDITOR
			if (baseObjectScriptTemp is null)
			{
				ErrorManagerScript.MyInstance.NullScriptError("BaseObjectScript");
			}
#endif

			AddObject(baseObjectScriptTemp);
		}
		_cameraScript = new CameraScript(playerInput);
		AllObjectInit(playerInput);
		_uiManagerScript.PlayerUIInit(_playerCharcterScript);
	}

	/// <summary>
	/// すべてのオブジェクトを初期化
	/// </summary>
	/// <param name="input">プレイヤー入力</param>
	private void AllObjectInit(InGamePlayerInput input)
	{
		foreach (StageFloorScript item in _stageFloors)
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

	/// <summary>
	/// すべてのオブジェクトを更新
	/// </summary>
	public void AllObjectUpdate()
	{
		foreach (StageFloorScript item in _stageFloors)
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

	/// <summary>
	/// UIの初期化していいかを確認して初期化する
	/// </summary>
	/// <param name="characterScript">キャラクタースクリプト</param>
	private void UICanInitCheck(BaseCharacterScript characterScript)
	{
		if (characterScript.IsActive)
		{
			if (characterScript.IsBoss)
			{
				_uiManagerScript.BossUIInit(characterScript);
				return;
			}
			if (characterScript == _playerCharcterScript.LockTarget)
			{
				_uiManagerScript.EnemyUIInit(characterScript);
				return;
			}
		}
	}

	/// <summary>
	/// 一番近いキャラクタースクリプトを取得する
	/// </summary>
	/// <param name="transform">自身のTransform</param>
	/// <returns></returns>
	public BaseCharacterScript GetNearCharcter(Transform transform)
	{
		float distanceTemp;
		float minDistance = float.MaxValue;
		BaseCharacterScript characterScriptTemp = default;
		foreach (BaseCharacterScript item in _charcterObjects)
		{
			if (transform == item.MyTransform) { continue; }

			distanceTemp = (item.MyTransform.position - transform.position).magnitude;
			if (minDistance > distanceTemp)
			{
				characterScriptTemp = item;
				minDistance = distanceTemp;
			}
		}
		return characterScriptTemp;
	}

	/// <summary>
	/// 当たっているオブジェクトをリストに格納する
	/// </summary>
	/// <param name="charcterColAreaData">自身のエリアデータ</param>
	/// <param name="collisionObjectDatas">確認するオブジェクト</param>
	/// <param name="moveDirection">確認する方向</param>
	public void GetCollisionAllObject(CollisionAreaData charcterColAreaData, List<BaseObjectScript> collisionObjectDatas
		, MoveDirection moveDirection)
	{
		GetCollisionFloor(charcterColAreaData, collisionObjectDatas, moveDirection);
		GetCollisionCharcter(charcterColAreaData, collisionObjectDatas);
	}

	/// <summary>
	/// キャラクターどうしが衝突しているかを確認する
	/// </summary>
	/// <param name="charcterColAreaData">自身のエリアデータ</param>
	/// <param name="collisionObjectDatas">格納するリスト</param>
	public void GetCollisionCharcter(CollisionAreaData charcterColAreaData, List<BaseObjectScript> collisionObjectDatas)
	{
		foreach (BaseObjectScript item in _charcterObjects)
		{
			//判定をとる必要がないものをはじく
			if (item is null  || charcterColAreaData.MyTransform is null
				|| charcterColAreaData.MyTransform.root == item.MyTransform
				|| (item is BaseCharacterScript charcterScript &&( !charcterScript.CanCollision || charcterScript.IsDeath))) { continue; }

			if (_collisionSystem.IsCollision(charcterColAreaData, item.MyCollisionAreaData))
			{
				collisionObjectDatas.Add(item);
			}
		}
	}

	/// <summary>
	/// 衝突している床を格納する
	/// </summary>
	/// <param name="charcterColAreaData">自身のエリアデータ</param>
	/// <param name="collisionObjectDatas">格納するリスト</param>
	/// <param name="moveDirection">確認したい方向</param>
	public void GetCollisionFloor(CollisionAreaData charcterColAreaData, List<BaseObjectScript> collisionObjectDatas
		, MoveDirection moveDirection)
	{
		foreach (BaseObjectScript item in _stageFloors)
		{
			if (charcterColAreaData.MyTransform == item.MyCollisionAreaData.MyTransform) { continue; }

			AddCollisionObject(charcterColAreaData, item, collisionObjectDatas, moveDirection);
		}
	}

	/// <summary>
	/// 衝突していたオブジェクトをリストに追加する
	/// </summary>
	/// <param name="colData">エリアデータ</param>
	/// <param name="targetObject">対象のオブジェクト</param>
	/// <param name="collisionObjects">格納するリスト</param>
	/// <param name="moveDirection">確認したい方向</param>
	private void AddCollisionObject(CollisionAreaData colData, BaseObjectScript targetObject
		, List<BaseObjectScript> collisionObjects, MoveDirection moveDirection)
	{
		if (_collisionSystem.IsCollision(colData, targetObject.MyCollisionAreaData, moveDirection))
		{
			collisionObjects.Add(targetObject);
		}
	}

	/// <summary>
	/// 当たり判定を確認する
	/// </summary>
	/// <param name="myData">エリアデータ</param>
	/// <param name="targetObject">対象のオブジェクト</param>
	/// <param name="moveDirection">移動方向</param>
	/// <returns></returns>
	public bool IsCollisionObject(CollisionAreaData myData, BaseObjectScript targetObject
		, MoveDirection moveDirection)
	{
		return _collisionSystem.IsCollision(myData, targetObject.MyCollisionAreaData, moveDirection);
	}

	/// <summary>
	/// 武器を取得する
	/// </summary>
	/// <param name="myData">自身のキャラクタースクリプト</param>
	/// <returns>自分の武器</returns>
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

	/// <summary>
	/// なにかのキャラクターに衝突しているか
	/// </summary>
	/// <param name="targetCharcterScript"></param>
	/// <returns></returns>
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

	/// <summary>
	/// オブジェクトリストにオブジェクトを追加する
	/// </summary>
	/// <param name="obj">追加するオブジェクト</param>
	public void AddObject(BaseObjectScript obj)
	{
		if (obj is StageFloorScript stageFloor)
		{
			_stageFloors.Add(stageFloor);
		}
		else if (obj is BaseCharacterScript charcterObj)
		{
			_charcterObjects.Add(charcterObj);
		}
		else if (obj is BaseWeaponScript weaponObj)
		{
			_weaponObjects.Add(weaponObj);
		}
#if UNITY_EDITOR
		else
		{
			ErrorManagerScript.MyInstance.CantExistObject(obj.name);
		}
#endif
	}

	/// <summary>
	/// オブジェクトリストからオブジェクトを削除する
	/// </summary>
	/// <param name="obj">削除するオブジェクト</param>
	public void SubtractObject(BaseObjectScript obj)
	{
		if (obj is StageFloorScript stageFloor)
		{
			_stageFloors.Remove(stageFloor);
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
#if UNITY_EDITOR
		else
		{
			ErrorManagerScript.MyInstance.CantExistObject(obj.name);
		}
#endif
	}
}
