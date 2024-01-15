/// <summary>
/// InGame�X�e�[�g�ł̃��C������
/// </summary>
public class InGameStateScript : BaseGameStateScript
{
	//�����̃X�e�[�g�}�V��
	private InGameStateMachineScript _myStateMachineScript = default;

	public override void Enter()
	{
		base.Enter();
		BaseInGameStateScript initState = new PlayStateScript();
		_myStateMachineScript = new InGameStateMachineScript(initState);
	}

	public override void Execute()
	{
		base.Execute();
		//�X�e�[�g�X�V���Ď��s����
		_myStateMachineScript.UpdateState().Execute();
	}

	public override void Exit()
	{
		_myStateMachineScript = null;
		base.Exit();
	}
}
