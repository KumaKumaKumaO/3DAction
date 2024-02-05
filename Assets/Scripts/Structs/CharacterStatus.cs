using UnityEngine;
[System.Serializable]
public struct CharcterStatus
{
	[SerializeField]
	private float _hp;
	[SerializeField]
	private float _maxHp;
	[SerializeField]
	private float _defense;
	[SerializeField]
	private float _defaultDefence;
	[SerializeField]
	private float _attack;
	[SerializeField]
	private float _defaultAttack;
	[SerializeField]
	private float _speed;
	[SerializeField]
	private float _defaultSpeed;
	[SerializeField]
	private float _stamina;
	[SerializeField]
	private float _defaultStamina;
	[SerializeField]
	private float _staggerThreshold;
	[SerializeField]
	private float _staggerMaxThreshold;

	public float MaxHP { get { return _maxHp; }set { _maxHp = value; } }
	public float Hp { get { return _hp; } set { _hp = value; } }
	public float DefaultDefence { get { return _defaultDefence; } set { _defaultDefence = value; } }
	public float Defense { get { return _defense; } set { _defense = value; } }
	public float MaxAttack { get { return _defaultAttack; } set { _defaultAttack = value; } }
	public float Attack { get { return _attack; } set { _attack = value; } }
	public float DefaultSpeed { get { return _defaultSpeed; }  set { _defaultSpeed = value; } }
	public float Speed { get { return _speed; } set { _speed = value; } }
	public float AirSpeed { get { return _speed / 2; } }
	public float DefaultStamina { get { return _defaultStamina; } set { _defaultStamina = value; } }
	public float Stamina { get { return _stamina; } set { _stamina = value; } }
	public float StaggerThreshold { get { return _staggerThreshold; } set { _staggerThreshold = value; } }
	public float StaggerMaxThreshold { get { return _staggerMaxThreshold; } set { _staggerMaxThreshold = value; } }
}