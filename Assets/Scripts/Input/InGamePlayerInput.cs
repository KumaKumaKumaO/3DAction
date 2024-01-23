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
	public virtual int ChangeWeapon()
	{
		return 0;
	}
	public virtual bool IsEvasion()
	{
		return Input.GetKeyDown(KeyCode.Space);
	}
	public virtual bool IsAttack()
	{
		return Input.GetKeyDown(KeyCode.Return);
	}
	public virtual Vector2 MoveInput()
	{
		return Vector2.right * Input.GetAxisRaw("Horizontal") + Vector2.up * Input.GetAxisRaw("Vertical");
	}
	public virtual int UseItem()
	{
		return 0;
	}
	public virtual bool IsOpenInventory()
	{
		return Input.GetKeyDown(KeyCode.I);
	}
	public virtual bool IsOpenPose()
	{
		return Input.GetKeyDown(KeyCode.Escape);
	}
}
