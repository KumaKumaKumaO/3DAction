using UnityEngine.SceneManagement;

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
	public override void Exit()
	{
		base.Exit();
	}
}
