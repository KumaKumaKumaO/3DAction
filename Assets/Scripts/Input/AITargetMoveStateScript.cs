using UnityEngine;

/// <summary>
/// AI�̖ړI�n�܂œ����X�e�[�g
/// </summary>
public class AITargetMoveStateScript : BaseAIStateScript
{
	private UnityEngine.Transform _targetTransform = default;
	private UnityEngine.Transform _myTransform = default;

	public AITargetMoveStateScript(IInputCharcterActionControlable input
		, UnityEngine.Transform targetTransform, UnityEngine.Transform myTransform):base(input)
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

	/// <summary>
	/// ���s���������ɂ���
	/// </summary>
	/// <param name="vector">�ϊ��������x�N�g��</param>
	/// <returns>�ϊ���x�N�g��</returns>
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
