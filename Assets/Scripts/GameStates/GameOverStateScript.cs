using UnityEngine.SceneManagement;

/// <summary>
/// �Q�[���I�[�o�[�X�e�[�g
/// </summary>
public class GameOverStateScript : BaseGameStateScript
{
	private IInputUIContorl _input = default;

	public override void Enter()
	{
		base.Enter();
		_input = new MenuInputScript();
	}

	public override void Execute()
	{
		base.Execute();
		if (_input.IsSubmit)
		{
			SceneManager.LoadScene("Title");
		}
	}
}
