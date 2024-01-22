using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �L�����N�^�[�̑������ۂ̓���
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
			ErrorManagerScript.MyInstance.NullGameObjectError("ObjectManager�̃^�O�������I�u�W�F�N�g");
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
