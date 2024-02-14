using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleGameState : BaseGameStateScript
{
	private GameObject _canvas = default;
	private IInputUIContorl _input = default;
	public override void Enter()
	{
		base.Enter();
		_input = new MenuInputScript();
		_canvas = GameObject.Find("Canvas");
	}
	public override void Execute()
	{
		base.Execute();
		if (_input.IsSubmit)
		{
			SceneManager.LoadScene("InGame");
		}
	}
}