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
	private InGamePlayerInput _playerInput = default;

	/// <summary>
	/// ������
	/// </summary>
	/// <param name="initState">�����̃X�e�[�g</param>
	public InGameStateMachineScript(BaseInGameStateScript initState,InGamePlayerInput input)
	{
		_playerInput = input;
		this._nowState = initState;
		_nowState.Enter();
	}

	/// <summary>
	/// �X�e�[�g���X�V����
	/// </summary>
	/// <returns>���݂̃X�e�[�g</returns>
	public BaseInGameStateScript UpdateState()
	{
		//���͂����݂���ꍇ
		if (_playerInput != null)
		{
			//�C���x���g���[���J���{�^������������
			if (_playerInput.IsOpenInventory)
			{
				//�v���C����������
				if (_nowState is PlayStateScript)
				{
					//���݂̃X�e�[�g��ێ�����
					_beforeState = _nowState;
					//���݂̃X�e�[�g���C���x���g���[�ɂ���
					_nowState = new InventoryStateScript(_playerInput);
					
				}
				//�C���x���g���[���J���Ă�����
				else if (_nowState is InventoryStateScript)
				{
					_nowState.Exit();
					//�O��̃X�e�[�g�ɖ߂�
					_nowState = _beforeState;
					//�O��̃X�e�[�g�̃A�h���X��j������
					_beforeState = null;
				}
				_nowState.Enter();
			}
			//�|�[�Y���J���{�^������������
			else if (_playerInput.IsOpenPose)
			{
				//�v���C����������
				if (_nowState is PlayStateScript)
				{
					//���݂̃X�e�[�g��ێ�����
					_beforeState = _nowState;
					//���݂̃X�e�[�g���|�[�Y�X�e�[�g�ɂ���
					_nowState = new PoseStateScript(_playerInput);
					
				}
				//�|�[�Y���J���Ă�����
				else if (_nowState is PoseStateScript)
				{
					_nowState.Exit();
					//�O��̃X�e�[�g�ɖ߂�
					_nowState = _beforeState;
					//�O��̃X�e�[�g�̃A�h���X��j������
					_beforeState = null;
				}
				_nowState.Enter();
			}

		}
		//���݂̃X�e�[�g��Ԃ�
		return _nowState;
	}
}