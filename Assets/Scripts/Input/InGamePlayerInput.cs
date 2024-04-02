using UnityEngine;

/// <summary>
/// InGameのプレイヤー入力
/// </summary>
public class InGamePlayerInput : IInputPlayerAction, IInputCharcterActionGetable, IInputCameraControl
{
	public Vector2 CameraMoveInput
	{
		get
		{
			Vector2 returnValue = default;
			if (Input.GetKey(KeyCode.Keypad4) || Input.GetKey(KeyCode.Alpha4))
			{
				returnValue += Vector2.left;
			}
			if (Input.GetKey(KeyCode.Keypad6) || Input.GetKey(KeyCode.Alpha6))
			{
				returnValue += Vector2.right;
			}
			if (Input.GetKey(KeyCode.Keypad8) || Input.GetKey(KeyCode.Alpha8))
			{
				returnValue += Vector2.up;
			}
			if (Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.Alpha2))
			{
				returnValue += Vector2.down;
			}
			return returnValue;
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
