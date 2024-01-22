using UnityEngine;

/// <summary>
/// エラーメッセージを表示する
/// </summary>
public class ErrorManagerScript : IErrorManager
{
	private static IErrorManager _myInstance = default;

	private const string HEAD_MSG = "エラー：";

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
		Debug.LogError(HEAD_MSG + className + "のインスタンスが過剰です。");
	}

	public void NullScriptError(string className)
	{
		Debug.LogError(HEAD_MSG + className + "が存在しません。");
	}

	public void NullGameObjectError(string gameObjectName)
	{
		Debug.LogError(HEAD_MSG + gameObjectName + "が存在しません。");
	}

	public void DeleteMyInstance()
	{
		_myInstance = null;
	}
	public void NullSceneNameError(string sceneName)
	{
		Debug.LogError(HEAD_MSG + sceneName + "という名前のシーンは存在しません。");
	}
	public void CantExistObject(string objName)
	{
		Debug.LogError(HEAD_MSG + objName + "は存在するはずではありません。");
	}
}
