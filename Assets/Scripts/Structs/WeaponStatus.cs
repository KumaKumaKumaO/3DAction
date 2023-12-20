public struct WeaponStatus
{
	private float _attack;
	private float _weight;
	public float Attack { get { return _attack; } }
	public float Weight { get { return _weight; } }
	public WeaponStatus(float attack, float weight)
	{
		this._attack = attack;
		this._weight = weight;
	}
}