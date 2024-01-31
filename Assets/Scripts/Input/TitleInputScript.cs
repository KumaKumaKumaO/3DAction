using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleInputScript : IInputUIContorl
{
	public bool IsSubmit()
	{
		return Input.GetKeyDown(KeyCode.Return);
	}
	public Vector2 MoveInput()
	{
		return Vector2.right * Input.GetAxisRaw("Horizontal") + Vector2.up * Input.GetAxisRaw("Vertical");
	}
}
