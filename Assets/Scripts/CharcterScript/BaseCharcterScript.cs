using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラクターの操作や実際の動き
/// </summary>
public abstract class BaseCharcterScript : BaseObjectScript
{
	protected ICharacterStateMachine _myStateMachine = default;
	protected ObjectManagerScript _objectManagerScript = default;
	private void Start()
	{
		GameObject objectManagerObject = GameObject.FindWithTag("ObjectManager");
		if(objectManagerObject == null)
		{
			ErrorManagerScript.MyInstance.NullGameObjectError("ObjectManagerのタグがついたオブジェクト");
		}
		else if(objectManagerObject.TryGetComponent<ObjectManagerScript>(out _objectManagerScript))
		{
			ErrorManagerScript.MyInstance.NullScriptError("ObjectManagerScript");
		}
	}
	public void CharcterUpdate()
	{

	}
}
