using UnityEngine;

/// <summary>
/// ErrorManagerのNullObject
/// </summary>
public class NullErrorManagerScript : IErrorManager
{
	public void InstantiationMyInstance()
	{
		NullObjectMsg();
	}
	public void NullScriptError(string classname)
	{
		NullObjectMsg();
	}
	public void SingleTonError(string className)
	{
		NullObjectMsg();
	}
	private void NullObjectMsg()
	{
		Debug.LogError(this +"はNullObjectです。");
	}
	public void NullGameObjectError(string className)
	{
		NullObjectMsg();
	}
	public void DeleteMyInstance()
	{
		NullObjectMsg();
	}
}
