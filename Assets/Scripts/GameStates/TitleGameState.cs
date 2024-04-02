using UnityEngine.SceneManagement;

/// <summary>
/// �^�C�g���X�e�[�g
/// </summary>
public class TitleGameState : BaseGameStateScript
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
			SceneManager.LoadScene("InGame");
		}
	}

	public override void Exit()
	{
		base.Exit();
		_input = null;
	}
}