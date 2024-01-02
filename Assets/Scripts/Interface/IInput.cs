using UnityEngine;
public interface IInputCharcterAction
{
	void Move(Vector2 input);
	void Attack();
	void Evasion(Vector2 input);
	void ChangeWeapon(BaseWeaponScript weapon);
	void UseItem(int index);
}
public interface IInputPlayerAction
{
	void OpenPose();
	void OpenInventory();
}
