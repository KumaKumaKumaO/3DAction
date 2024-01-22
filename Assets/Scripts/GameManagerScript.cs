using UnityEngine;

/// <summary>
/// �V���O���g���N���X
/// �Q�[���S�̂��R���g���[������
/// </summary>
public class GameManagerScript : MonoBehaviour
{
	//�����̃X�e�[�g�}�V��
	private GameStateMachineScript _myStateMachine = default;
	[SerializeField]
	private bool DebugFlag;
	private void Start()
	{
		//GameController�����ɑ��݂��邩���m�F����
		if (GameObject.FindGameObjectsWithTag("GameController").Length <= 1)
		{
			//�V�[�����ς���Ă������Ȃ��悤�ɂ���
			DontDestroyOnLoad(this);
			//�X�e�[�g�}�V���𐶐�
			_myStateMachine = new GameStateMachineScript();
			if (DebugFlag)
			{
				new ErrorManagerScript().InstantiationMyInstance();
			}
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

	private void OnDisable()
	{
		ErrorManagerScript.MyInstance.DeleteMyInstance();
	}
}