/// <summary>
/// InGame�̃X�e�[�g��J�ڂ�����
/// </summary>
public class InGameStateMachineScript
{
    private BaseInGameStateScript _nowState = default;
    private IInputPlayerAction _inputPlayerAction = default;
    /// <summary>
    /// �X�e�[�g���X�V����
    /// </summary>
    /// <returns>���݂̃X�e�[�g</returns>
    public BaseInGameStateScript UpdateState()
    {
        if (_inputPlayerAction.IsOpenInventory())
        {
            //�v���C����������
            if(_nowState.GetType() == typeof(PlayStateScript))
            {
                _nowState = new InventoryStateScript();
			}
			else if(_nowState.GetType() == typeof(InventoryStateScript))
			{
                _nowState = new PlayStateScript();
			}
        }else if (_inputPlayerAction.IsOpenPose())
        {
            if(_nowState.GetType() == typeof(PlayStateScript))
			{
                _nowState = new PoseStateScript();
			}else if (_nowState.GetType() == typeof(PoseStateScript))
			{
                _nowState = new PlayStateScript();
			}
        }
        return _nowState;
    }
}