using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// �S�ẴI�u�W�F�N�g�̌��̃N���X
/// </summary>
public abstract class BaseObjectScript : MonoBehaviour
{
    [SerializeField]
    protected bool isGravity = false;
    [SerializeField]
    protected CollisionAreaData _myCollisionAreaData = default;
    protected List<CollisionResultData> _myCollisionObjects = new List<CollisionResultData>();
    [SerializeField]
    protected int _bottomCollisionIndex = -1;
    [SerializeField]
    protected int _topCollisionAreaDataIndex = -1;
    [SerializeField]
    protected int _rightCollisionAreaDataIndex = -1;
    [SerializeField]
    protected int _leftCollisionAreaDataIndex = -1;
    [SerializeField]
    protected int _forwardCollisionAreaDataIndex = -1;
    [SerializeField]
    protected int _backCollisionAreaDataIndex = -1;
    protected ObjectManagerScript _objectManagerScript = default;
    protected Matrix4x4 _matrixTemp = default;

    [Header("�f�o�b�O�p")]

    [SerializeField]
    protected bool isGround = false;
    [SerializeField]
    protected bool isDebugColliderVisible = false;
    public CollisionAreaData MyCollisionAreaData { get { return _myCollisionAreaData; } }
    public virtual void Init()
    {
        GameObject objectManagerObject = GameObject.FindWithTag("ObjectManager");
        if (objectManagerObject == null)
        {
            ErrorManagerScript.MyInstance.NullGameObjectError("ObjectManager�̃^�O�������I�u�W�F�N�g");
        }
        else if (!objectManagerObject.TryGetComponent<ObjectManagerScript>(out _objectManagerScript))
        {
            ErrorManagerScript.MyInstance.NullScriptError("ObjectManagerScript");
        }
        _myCollisionAreaData.Init(transform);
    }
    public virtual void ObjectUpdate()
    {



    }

    protected virtual void Reset()
    {
        gameObject.tag = "Object";
    }
    protected void CollisionIndexInit()
    {
        _bottomCollisionIndex = -1;
        _topCollisionAreaDataIndex = -1;
        _rightCollisionAreaDataIndex = -1;
        _leftCollisionAreaDataIndex = -1;
        _forwardCollisionAreaDataIndex = -1;
        _backCollisionAreaDataIndex = -1;
    }
    public virtual void ObjectMove(Vector3 vector)
    {
        _myCollisionAreaData.MyTransform.position += vector;
        MoveClamp(vector);
    }

    private void MoveClamp(Vector3 moveDirection)
    {
        //��
        if (moveDirection.y < 0 && _bottomCollisionIndex >= 0)
        {
            _myCollisionAreaData.MyTransform.position += Vector3.up *
                (_myCollisionObjects[_bottomCollisionIndex].CollisionObjectData.MyCollisionAreaData.TopYPos
                - _myCollisionAreaData.BottomYPos + _myCollisionAreaData.AreaWidth);
            isGround = true;
        }
        //��
        else if (moveDirection.y > 0 && _topCollisionAreaDataIndex >= 0)
        {
            _myCollisionAreaData.MyTransform.position += Vector3.down *
                (_myCollisionObjects[_topCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData.TopYPos
                - _myCollisionAreaData.BottomYPos + _myCollisionAreaData.AreaWidth);
        }
        //�E
        if (moveDirection.x > 0 && _rightCollisionAreaDataIndex >= 0)
        {
            //Debug.Log("�݂�");
            _myCollisionAreaData.MyTransform.position += Vector3.left *
                (-_myCollisionObjects[_rightCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData.LeftXPos
                + _myCollisionAreaData.RightXPos + _myCollisionAreaData.AreaWidth);
        }
        //��
        else if (moveDirection.x < 0 && _leftCollisionAreaDataIndex >= 0)
        {
            //Debug.Log("��");
            _myCollisionAreaData.MyTransform.position += Vector3.right *
                (_myCollisionObjects[_leftCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData.RightXPos
                - _myCollisionAreaData.LeftXPos + _myCollisionAreaData.AreaWidth);
        }
        //�O
        if (moveDirection.z > 0 && _forwardCollisionAreaDataIndex >= 0)
        {
            _myCollisionAreaData.MyTransform.position += Vector3.back *
                (-_myCollisionObjects[_forwardCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData.BackZPos
                + _myCollisionAreaData.ForwardZPos + _myCollisionAreaData.AreaWidth);
            //Debug.Log("�O");
        }
        //��
        else if (moveDirection.z < 0 && _backCollisionAreaDataIndex >= 0)
        {
            _myCollisionAreaData.MyTransform.position += Vector3.forward *
                (_myCollisionObjects[_backCollisionAreaDataIndex].CollisionObjectData.MyCollisionAreaData.ForwardZPos
                - _myCollisionAreaData.BackZPos + _myCollisionAreaData.AreaWidth);
            //Debug.Log("���");
        }
    }

    protected virtual void GravityFall()
    {
        if (!isGround)
        {
            ObjectMove(Vector3.down * (_objectManagerScript.GravityPower * Time.deltaTime));
        }
        else if (_bottomCollisionIndex < 0)
        {
            isGround = false;
        }
    }

   
    protected virtual void OnDrawGizmos()
    {
        if (isDebugColliderVisible)
        {
            _matrixTemp = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        }
    }
}
