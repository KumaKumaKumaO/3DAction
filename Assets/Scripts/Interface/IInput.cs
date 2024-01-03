using UnityEngine;
public interface IInputCharcterAction
{
    Vector2 Move();
    bool IsAttack();
    Vector2 Evasion();
    bool ChangeWeapon();
    int UseItem();
}
public interface IInputPlayerAction
{
    bool IsOpenPose();
    bool IsOpenInventory();
}
