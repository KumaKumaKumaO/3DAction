public struct CharcterStatus
{
	private float _hp;
	private float _defense;
	private float _attack;
	private float _speed;
	private float _stamina;
	public float Hp { get { return _hp; } }
	public float Defense { get { return _defense; } }
	public float Attack { get { return _attack; } }
	public float Speed { get { return _speed; } }
	public float Stamina { get { return _stamina; } }
	public CharcterStatus(float hp, float defense, float attack, float speed, float stamina)
	{
		this._hp = hp;
		this._defense = defense;
		this._attack = attack;
		this._speed = speed;
		this._stamina = stamina;
	}
}