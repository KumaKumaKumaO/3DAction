using UnityEngine.SceneManagement;

/// <summary>
/// ゲームクリアステート
/// </summary>
public class GameClearStateScript:BaseGameStateScript
{
	private IInputUIContorl _input;

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

	public override void Exit()
	{
		base.Exit();
		_input = null;
	}
}
