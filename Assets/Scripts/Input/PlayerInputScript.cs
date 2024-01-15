using UnityEngine;

/// <summary>
/// 入力を用意する
/// </summary>
public class PlayerInputScript : IInputPlayerAction,IInputCharcterAction
{
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
	public virtual Vector2 Move()
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
