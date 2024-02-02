using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonGuardAIInputScript : IInputCharcterAction
{
	private Transform _playerTransform = default;
	private Transform _myTransform = default;
	private float _attackDistance = default;
	public DemonGuardAIInputScript(Transform myTransform,Transform playerTransform,float attackDistance)
	{
		this._attackDistance = attackDistance;
		this._playerTransform = playerTransform;
		this._myTransform = myTransform;
	}
	public Vector2 MoveInput()
	{
		Vector3 toPlayerVector = _playerTransform.position - _myTransform.position;
		if(toPlayerVector.magnitude  < 1.5f)
		{
			return Vector2.zero;
		}
		return Vector2.right * toPlayerVector.x + Vector2.up * toPlayerVector.z;
	}
	public bool IsJump()
	{
		return false;
	}
	public bool IsAttack()
	{
		Vector3 toPlayerVector = _playerTransform.position - _myTransform.position;
		if(toPlayerVector.magnitude <= _attackDistance)
		{
			return true;
		}
		return false;
	}
	public bool IsEvasion()
	{
		return false;
	}
	public bool IsRun()
	{
		return false;
	}
	public int ChangeWeapon()
	{
		return 0;
	}
	public int UseItem()
	{
		return 0;
	}
}
