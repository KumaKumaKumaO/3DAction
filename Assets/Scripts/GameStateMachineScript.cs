using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �Q�[���S�̂̏�Ԃ�J�ڂ�����
/// </summary>
public class GameStateMachineScript
{
	/// <summary>
	/// ��Ԃ�ς����ꍇ�͕ς���
	/// </summary>
	public BaseGameStateScript UpdateState()
	{
        switch (SceneManager.GetActiveScene().name)
        {
			case "Title":
                {
					return new TitleGameState();
                }
			case "InGame":
                {
					return new InGameGameStateScript();
                }
			case "GameOver":
                {
					return new GameOverStateScript();
                }
        }
		Debug.LogError("���݂��Ȃ��V�[�����ł��B");
		return null;
	}
}
