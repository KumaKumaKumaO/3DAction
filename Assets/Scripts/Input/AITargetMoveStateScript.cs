using UnityEngine;
/// <summary>
/// –Ú“I’n‚Ü‚Å“®‚­
/// </summary>
public class AITargetMoveStateScript : BaseAIStateScript
{
	private Transform _targetTransform = default;
	private Transform _myTransform = default;
	public AITargetMoveStateScript(IInputCharcterActionControlable input
		,Transform targetTransform,Transform myTransform):base(input)
	{
		this._targetTransform = targetTransform;
		this._myTransform = myTransform;
	}

	public override void Execute()
	{
		base.Execute();
		Vector3 targetToVector = _targetTransform.position - _myTransform.position;
		_input.MoveInput = ZVectorToYVector(targetToVector);
	}

	private Vector3 ZVectorToYVector(Vector3 vector)
	{
		return Vector3.right * vector.x + Vector3.up * vector.z;
	}

	public override void Exit()
	{
		_input.MoveInput = Vector2.zero;
		_targetTransform = null;
		_myTransform = null;
		base.Exit();
	}
}
