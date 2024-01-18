using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �Q�[���S�̂̃X�e�[�g��J�ڂ�����
/// </summary>
public class GameStateMachineScript
{
	//�O��̃V�[���̖��O
	private string _beforeSceneName = default;
	//���݂̃V�[���̖��O
	private string _nowSceneName = default;
	//���݂̃X�e�[�g
	private BaseGameStateScript _nowState = default;
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
			if (_nowState != null)
			{
				//���݂̃X�e�[�g���I��������
				_nowState.Exit();
			}
			//���݂̃V�[������Title�̏ꍇ
			if (_nowSceneName == "Title")
			{
				//�V�����X�e�[�g���C���X�^���X����
				_nowState = new TitleGameState();
			}
			//���݂̃V�[������InGame�̏ꍇ
			else if (_nowSceneName == "InGame")
			{
				//�V�����X�e�[�g���C���X�^���X����
				_nowState = new InGameStateScript();
			}
			//���݂̃V�[������GameOver�̏ꍇ
			else if (_nowSceneName == "GameOver")
			{
				//�V�����X�e�[�g���C���X�^���X����
				_nowState = new GameOverStateScript();
			}
			//���݂̃V�[����������ł��Ȃ��ꍇ
			else
			{
				//�G���[���b�Z�[�W���o��
				Debug.LogError("���݂��Ȃ��V�[�����ł��B");
				return null;
			}
			//�f�o�b�O�p�̃��b�Z�[�W
			Debug.Log(_nowSceneName + "�V�[���ł��B");
			//�V�����X�e�[�g���J�n������
			_nowState.Enter();
			//�O��̃V�[�����ɍ���̃V�[����������
			_beforeSceneName = _nowSceneName;
		}
		return _nowState;
	}
}
