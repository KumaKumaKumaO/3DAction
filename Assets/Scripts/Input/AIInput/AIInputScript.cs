using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// AI—p“ü—Í
/// </summary>
public class AIInputScript : IInputCharcterActionGetable,IAIInputInitializable,IInputCharcterActionControlable
{
	private Vector2 _moveInput = default;
	private bool isJump = default;
	private bool isAttack = default;
	private bool isEvasion = default;
	private bool isRun = default;
	protected BaseAIStateMachineScript _myStateMachine = default;

	public void Init(BaseCharacterScript baseCharacterScript,BaseAIStateMachineScript stateMachineScript)
	{
		_myStateMachine = stateMachineScript;
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
		get
		{
			if (isAttack)
			{
				isAttack = false;
				return true;
			}
			return false;
		}
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
