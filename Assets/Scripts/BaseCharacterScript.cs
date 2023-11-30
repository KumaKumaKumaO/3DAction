using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterScript : MonoBehaviour
{
    [SerializeField]
    private float _myHP = default;
    private Vector3 _myVelocity = Vector3.zero;
    protected Vector3 MyVelocity { get => _myVelocity; }
    protected float MyHP { get => _myHP; }
    public virtual void Initialize(InputScript inputScript)
    {
       
    }
    public virtual void MoveMyCharcter(Vector3 input)
    {

    }
}
