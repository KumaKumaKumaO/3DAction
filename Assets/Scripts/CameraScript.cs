using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ÉJÉÅÉâÇêßå‰Ç∑ÇÈÉNÉâÉX
/// </summary>
public class CameraScript
{
    private Transform _cameraTransform = default;
    private GameManagerScript _gameManagerScript = default;
    private Transform _playerCharcterTransform = default;
    private IInputCameraControl _input = default;
    private Vector2 _inputVector = default;
    private Vector3 _beforePlayerPos = default;
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
        GameObject gameManager = GameObject.FindWithTag("GameController");
        if (gameManager == null)
        {
            ErrorManagerScript.MyInstance.NullGameObjectError("GameControllerTagObject");
        }
        else if (!gameManager.TryGetComponent<GameManagerScript>(out _gameManagerScript))
        {
            ErrorManagerScript.MyInstance.NullCompornentError("GameManagerScript");
        }
        GameObject objectManager = GameObject.FindWithTag("ObjectManager");
        ObjectManagerScript objectManagerScript = default;
        if (objectManager == null)
        {
            ErrorManagerScript.MyInstance.NullGameObjectError("ObjectManagerTagObject");
        }
        else if (!objectManager.TryGetComponent<ObjectManagerScript>(out objectManagerScript))
        {
            ErrorManagerScript.MyInstance.NullCompornentError("ObjectManagerScript");
        }
        _playerCharcterTransform = objectManagerScript.PlayerCharcter.transform;
        _beforePlayerPos = _playerCharcterTransform.position;
    }



    public void UpdateCameraControl()
    {
        _inputVector = _input.CameraMoveInput();
        _cameraTransform.position += _playerCharcterTransform.position - _beforePlayerPos;
        _beforePlayerPos = _playerCharcterTransform.position;
        if (_inputVector.x != 0)
        {
            _cameraTransform.RotateAround(_playerCharcterTransform.position
                , Vector3.up, _inputVector.x * _gameManagerScript.CameraSpeed * Time.deltaTime);
        }
        if (_inputVector.y != 0)
        {
            _cameraTransform.RotateAround(_playerCharcterTransform.position
                , _cameraTransform.right, -_inputVector.y * _gameManagerScript.CameraSpeed * Time.deltaTime);
        }
    }
}
