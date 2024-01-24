using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラクターの操作や実際の動き
/// </summary>
public abstract class BaseCharcterScript : BaseObjectScript
{

	protected ICharacterStateMachine _myStateMachine = default;
	protected IInputCharcterAction _myInput = default;
	[SerializeField]
	protected CharcterStatus _myCharcterStatus = default;
	public override void ObjectUpdate()
	{
		base.ObjectUpdate();
		MoveCharcter(_myInput.MoveInput());
	}
	protected override void GravityFall()
	{
		base.GravityFall();
		if (_myCollisionObjects.Count <= 0) { return; }
		if (_myCollisionObjects[0].CollisionObjectData is StageFloorScript floorScriptTemp)
		{
			floorScriptTemp.OnTopCharcter(this);
		}
	}

	protected void MoveCharcter(Vector2 vector)
	{
		_myCollisionAreaData.MyTransform.position += Vector3.right * vector.x * _myCharcterStatus.Speed * Time.deltaTime 
			+ Vector3.forward * vector.y * _myCharcterStatus.Speed * Time.deltaTime;
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
