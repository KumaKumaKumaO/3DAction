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
		Debug.LogError(this +"��NullObject�ł��B");
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
