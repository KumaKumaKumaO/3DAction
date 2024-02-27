using UnityEngine;

public class DemonGuardNormalAIInputScript : IInputCharcterAction
{
	protected BaseCharacterScript _playerCharcterScript = default;
	protected DemonGuardCharacterScript _myCharcterScript = default;
	protected float _attackDistance = default;
	public DemonGuardNormalAIInputScript(DemonGuardCharacterScript myCharcterScript
		, BaseCharacterScript playerCharcterScript, float attackDistance)
	{
		this._attackDistance = attackDistance;
		this._playerCharcterScript = playerCharcterScript;
		this._myCharcterScript = myCharcterScript;
	}
	public virtual void Delete()
	{
		_playerCharcterScript = null;
		_myCharcterScript = null;
	}

	protected Vector3 CutYValue(Vector3 vector)
	{
		return vector - (Vector3.up * vector.y);
	}

	public virtual Vector2 MoveInput
	{
		get
		{
			if(_playerCharcterScript == null || _playerCharcterScript.IsDeath)
			{
				return Vector2.zero;
			}
			Vector3 toPlayerVector = CutYValue(_playerCharcterScript.MyTransform.position - _myCharcterScript.MyTransform.position);
			if (Mathf.Abs(toPlayerVector.normalized.x) < 0.05f && toPlayerVector.magnitude < _attackDistance)
			{
				return Vector2.zero;
			}
			return Vector2.right * toPlayerVector.x + Vector2.up * toPlayerVector.z;
		}
	}
	public virtual bool IsJump
	{
		get
		{
			return false;
		}
	}
	public virtual bool IsAttack
	{
		get
		{
			if(_playerCharcterScript == null || _playerCharcterScript.IsDeath)
			{
				return false;
			}

			Vector3 toPlayerVector = CutYValue(_playerCharcterScript.MyTransform.position 
				- _myCharcterScript.MyTransform.position);

			if (toPlayerVector.magnitude < _attackDistance 
				&& Mathf.Abs(Vector3.SignedAngle(
					  _myCharcterScript.MyTransform.forward
					, toPlayerVector
					, _myCharcterScript.MyTransform.up)) < 0.1f)
			{
				return true;
			}
			return false;
		}
	}
	public virtual bool IsEvasion
	{
		get
		{
			return false;
		}
	}
	public virtual bool IsRun
	{
		get
		{
			return false;
		}
	}
	public virtual int ChangeWeapon()
	{
		return 0;
	}
	public virtual int UseItem()
	{
		return 0;
	}
}
