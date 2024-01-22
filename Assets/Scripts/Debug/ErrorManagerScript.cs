using UnityEngine;

/// <summary>
/// �G���[���b�Z�[�W��\������
/// </summary>
public class ErrorManagerScript : IErrorManager
{
	private static IErrorManager _myInstance = default;

	private const string HEAD_MSG = "�G���[�F";

	public static IErrorManager MyInstance
	{
		get
		{
			if (_myInstance == null)
			{
				_myInstance = new NullErrorManagerScript();
			}
			return _myInstance;
		}
	}

	public void InstantiationMyInstance()
	{
		if (_myInstance == null)
		{
			_myInstance = this;
		}
		else
		{
			_myInstance.SingleTonError(this.GetType().Name);
		}
	}

	public void SingleTonError(string className)
	{
		Debug.LogError(HEAD_MSG + className + "�̃C���X�^���X���ߏ�ł��B");
	}

	public void NullScriptError(string className)
	{
		Debug.LogError(HEAD_MSG + className + "�����݂��܂���B");
	}

	public void NullGameObjectError(string gameObjectName)
	{
		Debug.LogError(HEAD_MSG + gameObjectName + "�����݂��܂���B");
	}

	public void DeleteMyInstance()
	{
		_myInstance = null;
	}
	public void NullSceneNameError(string sceneName)
	{
		Debug.LogError(HEAD_MSG + sceneName + "�Ƃ������O�̃V�[���͑��݂��܂���B");
	}
	public void CantExistObject(string objName)
	{
		Debug.LogError(HEAD_MSG + objName + "�͑��݂���͂��ł͂���܂���B");
	}
}
