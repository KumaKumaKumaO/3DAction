using UnityEngine;

/// <summary>
/// ErrorManager��NullObject
/// </summary>
public class NullErrorManagerScript : IErrorManager
{
	public void InstantiationMyInstance()
	{
		NullObjectMsg();
	}
	public void NullError(string classname)
	{
		NullObjectMsg();
	}
	public void SingleTonError(string className)
	{
		NullObjectMsg();
	}
	private void NullObjectMsg()
	{
		Debug.LogError("NullObject�ł��B");
	}
	public void DeleteMyInstance()
	{
		NullObjectMsg();
		ErrorManagerScript.MyInstance.DeleteMyInstance();
	}
}
