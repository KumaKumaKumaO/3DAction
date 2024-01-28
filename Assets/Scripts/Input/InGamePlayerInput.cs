using UnityEngine;

/// <summary>
/// “ü—Í‚ð—pˆÓ‚·‚é
/// </summary>
public class InGamePlayerInput : IInputPlayerAction,IInputCharcterAction
{
	public bool IsRun()
	{
		return Input.GetKey(KeyCode.LeftShift);
	}
	public  int ChangeWeapon()
	{
		return 0;
	}
	public  bool IsEvasion()
	{
		return Input.GetKeyDown(KeyCode.Space);
	}
	public  bool IsAttack()
	{
		return Input.GetKeyDown(KeyCode.Return);
	}
	public  Vector2 MoveInput()
	{
		return Vector2.right * Input.GetAxisRaw("Horizontal") + Vector2.up * Input.GetAxisRaw("Vertical");
	}
	public  int UseItem()
	{
		return 0;
	}
	public  bool IsOpenInventory()
	{
		return Input.GetKeyDown(KeyCode.I);
	}
	public  bool IsOpenPose()
	{
		return Input.GetKeyDown(KeyCode.Escape);
	}
	public bool IsJump()
    {
		return Input.GetKeyDown(KeyCode.LeftControl);
    }
}
