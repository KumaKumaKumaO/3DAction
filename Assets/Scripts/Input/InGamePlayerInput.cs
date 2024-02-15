using UnityEngine;

/// <summary>
/// “ü—Í‚ð—pˆÓ‚·‚é
/// </summary>
public class InGamePlayerInput : IInputPlayerAction, IInputCharcterAction, IInputCameraControl
{
	public Vector2 CameraMoveInput
	{
		get
		{
			return Vector2.right * Input.GetAxis("Mouse X") + Vector2.up * Input.GetAxis("Mouse Y");
		}
	}
	public bool IsRun
	{
		get
		{
			return Input.GetKey(KeyCode.LeftShift);
		}
	}
	public int ChangeWeapon()
	{
		return 0;
	}
	public bool IsEvasion
	{
		get
		{
			return Input.GetKeyDown(KeyCode.Space);
		}
	}
	public bool IsAttack
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
	public int UseItem()
	{
		return 0;
	}
	public bool IsOpenInventory
	{
		get
		{
			return Input.GetKeyDown(KeyCode.I);
		}
	}
	public bool IsOpenPose
	{
		get
		{
			return Input.GetKeyDown(KeyCode.Escape);
		}
	}
	public bool IsJump
	{
		get
		{
			return Input.GetKeyDown(KeyCode.LeftControl);
		}
	}
	public void Delete()
	{
	}
}
