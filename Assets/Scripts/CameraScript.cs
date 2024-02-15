using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラを制御するクラス
/// </summary>
public class CameraScript
{
	private Transform _cameraTransform = default;
	private ObjectManagerScript _objectManagerScript = default;
	private BaseCharacterScript _playerCharcterScript = default;
	private IInputCameraControl _input = default;
	private Vector3 _beforePlayerPos = default;
	private Vector3 _initPos = new Vector3(0, 2, -3);
	private Quaternion _initRotaion = Quaternion.Euler(10, 0, 0);
	public Transform CameraTransform { get { return _cameraTransform; } }
	public CameraScript(IInputCameraControl input)
	{
		this._input = input;
		GameObject cameraObj = GameObject.FindWithTag("MainCamera");
		if (cameraObj == null)
		{
			ErrorManagerScript.MyInstance.NullGameObjectError("MainCameraTagObject");
		}
		_cameraTransform = cameraObj.transform;
		GameObject objectManager = GameObject.FindWithTag("ObjectManager");
		if (objectManager == null)
		{
			ErrorManagerScript.MyInstance.NullGameObjectError("ObjectManagerTagObject");
		}
		else if (!objectManager.TryGetComponent<ObjectManagerScript>(out _objectManagerScript))
		{
			ErrorManagerScript.MyInstance.NullCompornentError("ObjectManagerScript");
		}
		_playerCharcterScript = _objectManagerScript.PlayerCharcterScript;
		_beforePlayerPos = _playerCharcterScript.transform.position;
		_initPos
			= Vector3.up
			* (_objectManagerScript.PlayerCharcterScript.MyCollisionAreaData.HalfAreaSize.y * 2.5f)
			+ Vector3.forward
			* (_objectManagerScript.PlayerCharcterScript.MyCollisionAreaData.HalfAreaSize.y * -3);

		_cameraTransform.position = _playerCharcterScript.transform.position
			+ _playerCharcterScript.transform.right * _initPos.x
			+ _playerCharcterScript.transform.up * _initPos.y
			+ _playerCharcterScript.transform.forward * _initPos.z;

		_cameraTransform.rotation = _playerCharcterScript.transform.rotation * _initRotaion;
	}

	public void Delete()
	{
		_cameraTransform = null;
		_objectManagerScript = null;
		_playerCharcterScript = null;
		_input = null;
	}

	/// <summary>
	/// カメラを制御する
	/// </summary>
	public void UpdateCameraControl()
	{
		if (_playerCharcterScript.IsDestroyObject) { return; }
		UpdateCameraPos();
		UpdateCameraRotation(_input.CameraMoveInput);
	}

	/// <summary>
	/// カメラの場所を更新する
	/// </summary>
	private void UpdateCameraPos()
	{
		_cameraTransform.position += _playerCharcterScript.MyTransform.position - _beforePlayerPos;
		_beforePlayerPos = _playerCharcterScript.MyTransform.position;
	}
	/// <summary>
	/// カメラの角度を更新する
	/// </summary>
	/// <param name="inputVector"></param>
	private void UpdateCameraRotation(Vector2 inputVector)
	{
		if (inputVector.x != 0)
		{
			_cameraTransform.RotateAround(_playerCharcterScript.MyTransform.position
				, Vector3.up, inputVector.x * _objectManagerScript.CameraSpeed * Time.deltaTime);
		}
		if (inputVector.y != 0)
		{
			_cameraTransform.RotateAround(_playerCharcterScript.MyTransform.position
				, _cameraTransform.right, inputVector.y * _objectManagerScript.CameraSpeed * Time.deltaTime);
		}
	}
}
