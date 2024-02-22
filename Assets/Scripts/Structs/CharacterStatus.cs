using UnityEngine;
using UniRx;
using System;
using Cysharp.Threading.Tasks;

[System.Serializable]
public class CharcterStatus
{
	[SerializeField]
	private ReactiveProperty<float> _hp = new ReactiveProperty<float>();
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
	private ReactiveProperty<float> _stamina  = new ReactiveProperty<float>();
	[SerializeField]
	private float _maxStamina;
	[SerializeField]
	private ReactiveProperty<float> _staggerThreshold = new ReactiveProperty<float>();
	[SerializeField]
	private float _maxStaggerThreshold = default;
	[SerializeField]
	private float _staggerRecoveryStartTime = default;
	[SerializeField]
	protected float _evasionNoDamageTime = default;
	[SerializeField]
	protected float _decreaseEvasionStamina = default;

	public float MaxHP { get { return _maxHp; } set { _maxHp = value; } }
	public ReactiveProperty<float> Hp { get { return _hp; } }
	public float DefaultDefence { get { return _defaultDefence; } set { _defaultDefence = value; } }
	public float Defense { get { return _defense; } set { _defense = value; } }
	public float MaxAttack { get { return _defaultAttack; } set { _defaultAttack = value; } }
	public float Attack { get { return _attack; } set { _attack = value; } }
	public float DefaultSpeed { get { return _defaultSpeed; } set { _defaultSpeed = value; } }
	public float Speed { get { return _speed; } set { _speed = value; } }
	public float AirSpeed { get { return _speed / 2; } }
	public float MaxStamina { get { return _maxStamina; } set { _maxStamina = value; } }
	public ReactiveProperty<float> Stamina { get { return _stamina; }}
	public ReactiveProperty<float> StaggerThreshold { get { return _staggerThreshold; } }
	public float EvasionNoDamageTime { get { return _evasionNoDamageTime; } }
	public float DecreaseEvasionStamina { get { return _decreaseEvasionStamina; } }
	[SerializeField]
	protected float _staggerRecoveryTime = default;
	public void Init()
	{
		_hp.Value = _maxHp;
		_stamina.Value = _maxStamina;
		_staggerThreshold.Where(value => value <= 0).Subscribe(_ => OverStaggerThreshold() );
	}
	private void OverStaggerThreshold()
	{
		_staggerThreshold.Value = 0;
		RecoveryStagger().Forget();
	}
	private async UniTaskVoid RecoveryStagger()
	{
		await UniTask.Delay(TimeSpan.FromSeconds(_staggerRecoveryStartTime));
		float recoveryValue = _maxStaggerThreshold / _staggerRecoveryTime;
		while(_staggerThreshold.Value >= _maxStaggerThreshold)
		{
			
			_staggerThreshold.Value += recoveryValue;
		}
	}

}