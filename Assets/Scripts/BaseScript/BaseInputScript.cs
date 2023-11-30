using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInputScript : MonoBehaviour
{
    private PlayerInput _input;
    private Vector2 _moveDirection;
    private bool isAttack = false;
    private bool isEvasion = false;
    public Vector2 MoveDirection { get => _moveDirection; }
    public bool IsAttack { get => isAttack; }
    public bool IsEvasion { get => isEvasion; }
    public virtual void InputUpdate()
    {
        isAttack = (_input.Play.Attack.ReadValue<float>() > 0);
        isEvasion = (_input.Play.Evasion.ReadValue<float>() > 0);
        _moveDirection = _input.Play.Move.ReadValue<Vector2>();
    }
}
