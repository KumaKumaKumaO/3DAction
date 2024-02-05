using UnityEngine;
[System.Serializable]
public struct WeaponStatus
{
	[SerializeField]
	private float _attack;
	[SerializeField]
	private float _weight;
	[SerializeField]
	private float _staggerValue;
	public float Attack { get { return _attack; } }
	public float Weight { get { return _weight; } }
	public float StaggerValue { get { return _staggerValue; } }
}