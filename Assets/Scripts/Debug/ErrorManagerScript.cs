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
			if(_myInstance == null)
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

	public void NullError(string className)
	{
		Debug.LogError(HEAD_MSG + className + "のが存在しません。");
	}

	public void DeleteMyInstance()
	{
		_myInstance = null;
	}
}
