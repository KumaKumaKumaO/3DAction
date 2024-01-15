/// <summary>
/// InGame�̃X�e�[�g��J�ڂ�����
/// </summary>
public class InGameStateMachineScript
{
	//�O��̃X�e�[�g
	private BaseInGameStateScript _beforeState = default;
	//���݂̃X�e�[�g
	private BaseInGameStateScript _nowState = default;
	//�v���C���[�̓���
	private IInputPlayerAction _inputPlayerAction = default;

	/// <summary>
	/// ������
	/// </summary>
	/// <param name="initState">�����̃X�e�[�g</param>
	public InGameStateMachineScript(BaseInGameStateScript initState)
	{
		this._nowState = initState;
	}

	/// <summary>
	/// ���͂�ݒ肷��
	/// </summary>
	/// <param name="inputPlayerAction">�v���C���[����</param>
	public void SetPlayerInput(IInputPlayerAction inputPlayerAction)
	{
		this._inputPlayerAction = inputPlayerAction;
	}

	/// <summary>
	/// �X�e�[�g���X�V����
	/// </summary>
	/// <returns>���݂̃X�e�[�g</returns>
	public BaseInGameStateScript UpdateState()
	{
		//���͂����݂���ꍇ
		if (_inputPlayerAction != null)
		{
			//�C���x���g���[���J���{�^������������
			if (_inputPlayerAction.IsOpenInventory())
			{
				//�v���C����������
				if (_nowState.GetType() == typeof(PlayStateScript))
				{
					//���݂̃X�e�[�g��ێ�����
					_beforeState = _nowState;
					//���݂̃X�e�[�g���C���x���g���[�ɂ���
					_nowState = new InventoryStateScript();
					_nowState.Enter();
				}
				//�C���x���g���[���J���Ă�����
				else if (_nowState.GetType() == typeof(InventoryStateScript))
				{
					_nowState.Exit();
					//�O��̃X�e�[�g�ɖ߂�
					_nowState = _beforeState;
					//�O��̃X�e�[�g�̃A�h���X��j������
					_beforeState = null;
				}
			}
			//�|�[�Y���J���{�^������������
			else if (_inputPlayerAction.IsOpenPose())
			{
				//�v���C����������
				if (_nowState.GetType() == typeof(PlayStateScript))
				{
					//���݂̃X�e�[�g��ێ�����
					_beforeState = _nowState;
					//���݂̃X�e�[�g���|�[�Y�X�e�[�g�ɂ���
					_nowState = new PoseStateScript();
					_nowState.Enter();
				}
				//�|�[�Y���J���Ă�����
				else if (_nowState.GetType() == typeof(PoseStateScript))
				{
					_nowState.Exit();
					//�O��̃X�e�[�g�ɖ߂�
					_nowState = _beforeState;
					//�O��̃X�e�[�g�̃A�h���X��j������
					_beforeState = null;
				}
			}
		}
		//���݂̃X�e�[�g��Ԃ�
		return _nowState;
	}
}