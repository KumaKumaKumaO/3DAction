using UnityEngine;
[System.Serializable]
public struct CharcterStatus
{
	[SerializeField]
	private float _hp;
	[SerializeField]
	private float _defense;
	[SerializeField]
	private float _attack;
	[SerializeField]
	private float _speed;
	[SerializeField]
	private float _stamina;
	public float Hp { get { return _hp; } set { _hp = value; } }
	public float Defense { get { return _defense; } set { _defense = value; } }
	public float Attack { get { return _attack; } set { _attack = value; } }
	public float Speed { get { return _speed; } set { _speed = value; } }
	public float Stamina { get { return _stamina; } set { _stamina = value; } }
	public CharcterStatus(float hp, float defense, float attack, float speed, float stamina)
	{
		this._hp = hp;
		this._defense = defense;
		this._attack = attack;
		this._speed = speed;
		this._stamina = stamina;
	}
}