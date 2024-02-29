using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class BaseAIInputScript : IInputCharcterActionGetable
{
	private Vector2 _moveInput = default;
	private bool isJump = default;
	private bool isAttack = default;
	private bool isEvasion = default;
	private bool isRun = default;
	protected BaseAIStateMachineScript _myStateMachine = default;
	public BaseAIStateMachineScript MyStateMachineScript { set { _myStateMachine = value; } }

	public BaseAIInputScript(BaseCharacterScript baseCharacterScript)
	{
		baseCharacterScript.UpdateAsObservable()
			.Subscribe(_ => _myStateMachine.UpdateState().Execute())
			.AddTo(baseCharacterScript);
	}

	public Vector2 MoveInput
	{
		get { return _moveInput; }
		set { _moveInput = value; }
	}

	public bool IsJump
	{
		get { return isJump; }
		set { isJump = value; }
	}

	public bool IsAttack
	{
		get { return isAttack; }
		set { isAttack = value; }
	}

	public bool IsEvasion
	{
		get { return isEvasion; }
		set { isEvasion = value; }
	}

	public bool IsRun
	{
		get { return isRun; }
		set { isRun = value; }
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
