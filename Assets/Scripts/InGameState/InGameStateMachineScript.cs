/// <summary>
/// InGame�̃X�e�[�g��J�ڂ�����
/// </summary>
public class InGameStateMachineScript
{
    private BaseInGameStateScript _beforeState = default;
    private IInputPlayerAction _inputPlayerAction = default;
    /// <summary>
    /// �X�e�[�g���X�V����
    /// </summary>
    /// <returns>���݂̃X�e�[�g</returns>
    public BaseInGameStateScript UpdateState()
    {
        if (_inputPlayerAction.IsOpenInventory())
        {

        }else if (_inputPlayerAction.IsOpenPose())
        {

        }
        

        return null;
    }
}