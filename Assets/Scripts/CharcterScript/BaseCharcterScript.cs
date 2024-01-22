using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �L�����N�^�[�̑������ۂ̓���
/// </summary>
public abstract class BaseCharcterScript : BaseObjectScript
{
	protected ICharacterStateMachine _myStateMachine = default;
	protected BaseObjectScript _baseObjectScript = default;
	protected StageFloorScript floorScriptTemp = default;
	public override void ObjectUpdate()
	{
		base.ObjectUpdate();
		
	}
	protected override void GravityFall()
	{
		base.GravityFall();
		floorScriptTemp = _collisionObjectTemp as StageFloorScript;
		if(floorScriptTemp != null)
		{
			floorScriptTemp.OnTopCharcter(this);
		}
	}
}
