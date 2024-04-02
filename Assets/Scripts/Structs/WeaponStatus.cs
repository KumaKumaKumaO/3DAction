using UnityEngine;

/// <summary>
/// 武器のステータス
/// </summary>
[System.Serializable]
public struct WeaponStatus
{
	[SerializeField]
	private float _attack;
	[SerializeField]
	private float _staggerValue;
	public float Attack { get { return _attack; } }
	public float StaggerValue { get { return _staggerValue; } }
}