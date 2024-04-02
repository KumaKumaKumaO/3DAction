using UnityEngine.SceneManagement;

/// <summary>
/// タイトルステート
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