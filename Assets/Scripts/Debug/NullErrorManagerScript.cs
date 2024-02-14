using UnityEngine;

/// <summary>
/// ErrorManagerÇÃNullObject
/// </summary>
public class NullErrorManagerScript : IErrorManager
{
	private bool isDisplayNullMSG = false;
	public void InstantiationMyInstance()
	{
		NullObjectMsg();
	}
	public void NullScriptError(string className)
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
			Debug.LogWarning(this + "ÇÕNullObjectÇ≈Ç∑ÅB");
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
	public void OverFlow(string className,int i)
	{
		NullObjectMsg();
	}
	public void Delete()
	{

	}
}
