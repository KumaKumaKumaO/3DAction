using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラクターの操作や実際の動き
/// </summary>
public abstract class BaseCharcterScript : BaseObjectScript
{

	protected ICharacterStateMachine _myStateMachine = default;
	protected StageFloorScript floorScriptTemp = default;
	[SerializeField]
	protected CharcterStatus _myCharcterStatus = default;
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

	public virtual void HealHP(float healValue)
	{
		_myCharcterStatus.Hp += healValue;
	}

	public virtual void ReceiveDamage(float damage)
	{
		_myCharcterStatus.Hp -= damage;
	}
}
