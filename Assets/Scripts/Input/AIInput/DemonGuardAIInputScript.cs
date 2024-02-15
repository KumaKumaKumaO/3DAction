using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonGuardAIInputScript : IInputCharcterAction
{
	private BaseCharacterScript _playerCharcterScript = default;
	private Transform _myTransform = default;
	private float _attackDistance = default;
	public DemonGuardAIInputScript(Transform myTransform, BaseCharacterScript playerCharcterScript, float attackDistance)
	{
		this._attackDistance = attackDistance;
		this._playerCharcterScript = playerCharcterScript;
		this._myTransform = myTransform;
	}
	public void Delete()
	{
		_playerCharcterScript = null;
		_myTransform = null;
	}

	private Vector3 CutYValue(Vector3 vector)
	{
		return vector - (Vector3.up * vector.y);
	}

	public Vector2 MoveInput
	{
		get
		{
			if(_playerCharcterScript == null || _playerCharcterScript.IsDeath)
			{
				return Vector2.zero;
			}
			Vector3 toPlayerVector = CutYValue(_playerCharcterScript.MyTransform.position - _myTransform.position);
			if (Mathf.Abs(toPlayerVector.normalized.x) < 0.05f && toPlayerVector.magnitude < _attackDistance)
			{
				return Vector2.zero;
			}
			return Vector2.right * toPlayerVector.x + Vector2.up * toPlayerVector.z;
		}
	}
	public bool IsJump
	{
		get
		{
			return false;
		}
	}
	public bool IsAttack
	{
		get
		{
			if(_playerCharcterScript == null || _playerCharcterScript.IsDeath)
			{
				return false;
			}
			Vector3 toPlayerVector = CutYValue(_playerCharcterScript.MyTransform.position - _myTransform.position);
			if (toPlayerVector.magnitude < _attackDistance && Mathf.Abs(Vector3.SignedAngle(_myTransform.forward, toPlayerVector, _myTransform.up)) < 0.1f)
			{
				return true;
			}
			return false;
		}
	}
	public bool IsEvasion
	{
		get
		{
			return false;
		}
	}
	public bool IsRun
	{
		get
		{
			return false;
		}
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
