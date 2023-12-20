public struct StageObjectStatus
{
	private float _hp;
	private float _attack;
	private bool _destructible;
	public float Hp { get { return _hp; } }
	public float Attack { get { return _attack; } }
	public bool Destructiable { get { return _destructible; } }
	public StageObjectStatus(float hp, float attack, bool destructiable)
	{
		this._hp = hp;
		this._attack = attack;
		this._destructible = destructiable;
	}
}