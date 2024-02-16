using UnityEngine;
[System.Serializable]
public class AnimationSettingData 
{
	[SerializeField]
	private AvatarTarget _targetPart;
	[SerializeField, Range(0, 1)]
	private float _startRatio;
	[SerializeField, Range(0, 1)]
	private float _endRatio;
	[SerializeField]
	private Vector3 _moveVector;
	public AvatarTarget TargetPart { get { return _targetPart; } }
	public float StartRatio { get { return _startRatio; } }
	public float EndRatio { get { return _endRatio; } }
	public Vector3 MoveVector { get { return _moveVector; } }
}