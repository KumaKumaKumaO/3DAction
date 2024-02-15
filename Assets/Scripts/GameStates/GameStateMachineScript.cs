using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �Q�[���S�̂̃X�e�[�g��J�ڂ�����
/// </summary>
public class GameStateMachineScript
{
	public GameStateMachineScript()
	{
		_nowSceneName = SceneManager.GetActiveScene().name;
		_beforeSceneName = _nowSceneName;
		_nowState = SelectState();
		_nowState.Enter();
	}
	//�O��̃V�[���̖��O
	private string _beforeSceneName = default;
	//���݂̃V�[���̖��O
	private string _nowSceneName = default;
	//���݂̃X�e�[�g
	private BaseGameStateScript _nowState = default;

	public void Delete()
	{
		_nowState.Exit();
		_nowState = null;
	}
	/// <summary>
	/// �X�e�[�g���ύX�ł���ꍇ�͕ύX����
	/// </summary>
	/// <returns>���݂̃X�e�[�g</returns>
	public BaseGameStateScript UpdateState()
	{
		//���݂̃V�[���̖��O
		_nowSceneName = SceneManager.GetActiveScene().name;
		//���݂̃V�[���̖��O�ƑO��̃V�[���̖��O���Ⴄ���̊m�F
		if (_nowSceneName != _beforeSceneName)
		{
			//���݂̃X�e�[�g���I��������
			_nowState.Exit();
			//�ύX��X�e�[�g���擾����
			_nowState = SelectState();
			//�V�����X�e�[�g���J�n������
			_nowState.Enter();
			//�O��̃V�[�����ɍ���̃V�[����������
			_beforeSceneName = _nowSceneName;
		}
		return _nowState;
	}
	private BaseGameStateScript SelectState()
	{
		//���݂̃V�[������Title�̏ꍇ
		if (_nowSceneName == "Title")
		{
			//�V�����X�e�[�g���C���X�^���X����
			return new TitleGameState();
		}
		//���݂̃V�[������InGame�̏ꍇ
		else if (_nowSceneName == "InGame")
		{
			//�V�����X�e�[�g���C���X�^���X����
			return new InGameStateScript();
		}
		//���݂̃V�[������GameOver�̏ꍇ
		else if (_nowSceneName == "GameOver")
		{
			//�V�����X�e�[�g���C���X�^���X����
			return new GameOverStateScript();
		}
		//���݂̃V�[����������ł��Ȃ��ꍇ
		else
		{
			//�G���[���b�Z�[�W���o��
			Debug.LogError("���݂��Ȃ��V�[�����ł��B");
			return null;
		}
	}
}
