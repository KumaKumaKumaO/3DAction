using UnityEngine;

/// <summary>
/// ErrorManagerÇÃNullObject
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
		Debug.LogError("NullObjectÇ≈Ç∑ÅB");
	}
	public void DeleteMyInstance()
	{
		NullObjectMsg();
		ErrorManagerScript.MyInstance.DeleteMyInstance();
	}
}
