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
        _myStateMachineScript = new InGameStateMachineScript();
    }
    public override void Execute()
    {
        base.Execute();
        //�X�e�[�g�X�V���Ď��s����
        _myStateMachineScript.UpdateState().Execute();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
