using UnityEngine;

/// <summary>
/// ���j���[��ʂ̃v���C���[����
/// </summary>
public class MenuInputScript : IInputUIContorl
{
	public bool IsSubmit 
	{
		get
		{
			return Input.GetKeyDown(KeyCode.Return);
		}
	}

	public Vector2 MoveInput
	{
		get
		{
			return Vector2.right * Input.GetAxisRaw("Horizontal") + Vector2.up * Input.GetAxisRaw("Vertical");
		}
	}

	public void Delete()
	{

	}
}
