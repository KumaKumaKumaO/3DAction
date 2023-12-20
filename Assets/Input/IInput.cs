using UnityEngine;
public interface IInputCharcterAction
{
	void Move(Vector2 input);
	void Attack();
	void Evasion(Vector2 input);
	void ChangeWeapon(BaseWeaponScript weapon);
}
public interface IInputPlayerAction
{
	void OpenPose();
	void OpenInventory();
	void UseItem(int index);

}
