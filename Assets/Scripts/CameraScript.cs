using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ÉJÉÅÉâÇêßå‰Ç∑ÇÈÉNÉâÉX
/// </summary>
public class CameraScript
{
	private Transform _cameraTransform = default;
	private ObjectManagerScript _objectManagerScript = default;
	private Transform _playerCharcterTransform = default;
	private IInputCameraControl _input = default;
	private Vector2 _inputVector = default;
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
		_playerCharcterTransform = _objectManagerScript.PlayerCharcterScript.transform;
		_beforePlayerPos = _playerCharcterTransform.position;
		_initPos
			= Vector3.up
			* (_objectManagerScript.PlayerCharcterScript.MyCollisionAreaData.HalfAreaSize.y * 2.5f)
			+ Vector3.forward
			* (_objectManagerScript.PlayerCharcterScript.MyCollisionAreaData.HalfAreaSize.y * -3);

		_cameraTransform.position = _playerCharcterTransform.position
			+ _playerCharcterTransform.right * _initPos.x
			+ _playerCharcterTransform.up * _initPos.y
			+ _playerCharcterTransform.forward * _initPos.z;

		_cameraTransform.rotation = _playerCharcterTransform.rotation * _initRotaion;
	}



	public void UpdateCameraControl()
	{
		_inputVector = _input.CameraMoveInput;
		_cameraTransform.position += _playerCharcterTransform.position - _beforePlayerPos;
		_beforePlayerPos = _playerCharcterTransform.position;
		if (_inputVector.x != 0)
		{
			_cameraTransform.RotateAround(_playerCharcterTransform.position
				, Vector3.up, _inputVector.x * _objectManagerScript.CameraSpeed * Time.deltaTime);
		}
		if (_inputVector.y != 0)
		{
			_cameraTransform.RotateAround(_playerCharcterTransform.position
				, _cameraTransform.right, -_inputVector.y * _objectManagerScript.CameraSpeed * Time.deltaTime);
		}
	}
}
