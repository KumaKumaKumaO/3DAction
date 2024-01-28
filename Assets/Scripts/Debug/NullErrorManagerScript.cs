using UnityEngine;

/// <summary>
/// ErrorManagerのNullObject
/// </summary>
public class NullErrorManagerScript : IErrorManager
{
	private bool isDisplayNullMSG = false;
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
		if (!isDisplayNullMSG)
		{
			Debug.LogError(this + "はNullObjectです。");
			isDisplayNullMSG = true;
		}
	}
	public void NullGameObjectError(string className)
	{
		NullObjectMsg();
	}
	public void DeleteMyInstance()
	{
		NullObjectMsg();
	}

	public void NullSceneNameError(string SceneName)
	{
		NullObjectMsg();
	}
	public void CantExistObject(string objName)
	{
		NullObjectMsg();
	}
	public void NullCompornentError(string compornentName)
    {
		NullObjectMsg();
    }

}
