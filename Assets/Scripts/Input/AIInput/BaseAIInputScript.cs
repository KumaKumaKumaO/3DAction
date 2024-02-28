using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class BaseAIInputScript : IInputCharcterAction
{
	private Vector2 _moveInput = default;
	private bool isJump = default;
	private bool isAttack = default;
	private bool isEvasion = default;
	private bool isRun = default;
	protected BaseAIStateMachineScript _myStateMachine = default;

	public BaseAIInputScript(BaseCharacterScript baseCharacterScript
		,BaseAIStateMachineScript stateMachine)
	{
		baseCharacterScript.UpdateAsObservable()
			.Subscribe(_ => _myStateMachine.UpdateState().Execute())
			.AddTo(baseCharacterScript);

		_myStateMachine = stateMachine;
	}

	public Vector2 MoveInput
	{
		get { return _moveInput; }
	}

	public bool IsJump
	{
		get { return isJump; }
	}

	public bool IsAttack
	{
		get { return isAttack; }
	}

	public bool IsEvasion
	{
		get { return isEvasion; }
	}

	public bool IsRun
	{
		get { return isRun; }
	}

	public int ChangeWeapon()
	{
		return 0;
	}

	public void Delete()
	{
		_myStateMachine = null;
	}

	public int UseItem()
	{
		return 0;
	}
}
