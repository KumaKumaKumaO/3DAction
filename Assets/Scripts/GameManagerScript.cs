using UnityEngine;

/// <summary>
/// �V���O���g���N���X
/// �Q�[���S�̂��R���g���[������
/// </summary>
public class GameManagerScript : MonoBehaviour
{
	//�����̃C���X�^���X
	private static GameManagerScript _instanceGameManager = default;
	//�����̃X�e�[�g�}�V��
	private GameStateMachineScript _myStateMachine = default;
	private void Start()
	{
		//�C���X�^���X�����݂��邩���m�F����
		if (_instanceGameManager == null)
		{
			_instanceGameManager = this;
			//�V�[�����ς���Ă������Ȃ��悤�ɂ���
			DontDestroyOnLoad(this);
			//�X�e�[�g�}�V���𐶐�
			_myStateMachine = new GameStateMachineScript();
		}
		else
		{
			//�C���X�^���X�����݂���ꍇ�͎������폜����
			Destroy(gameObject);
		}
	}

	private void Update()
	{
		//���݂̃X�e�[�g�����s����
		_myStateMachine.UpdateState().Execute();
	}
}