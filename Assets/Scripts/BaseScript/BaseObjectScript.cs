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
    protected ObjectManagerScript _objectManagerScript = default;
    protected List<CollisionResultData> _myCollisionObjects = new List<CollisionResultData>();
    protected bool isSorted = false;
    [Header("�f�o�b�O�p")]
    [SerializeField]
    private bool isDebugColliderVisible = false;
    [SerializeField]
    protected bool isGround = false;



    [SerializeField]
    public CollisionResultData test;
    [SerializeField]
    float value;
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
        if (!isGravity)
        {
            isGround = true;
        }
    }
    private void Reset()
    {
        gameObject.tag = "Object";
    }

    public virtual void ObjectUpdate()
    {
        isSorted = false;

        if (isGravity)
        {
            _objectManagerScript.GetCollisionObject(_myCollisionAreaData, _myCollisionObjects);
            GravityFall();
        }
        //���ʂ��Փ˂��Ă��邩
        //��ʂ��Փ˂��Ă��邩

    }

    protected virtual void GravityFall()
    {
        value = _myCollisionObjects.Count;
        if (_myCollisionObjects.Count <= 0)
        {
            _myCollisionAreaData.MyTransform.position -= Vector3.up * (_objectManagerScript.GravityPower * Time.deltaTime);
            isGround = false;
            return;
        }

        if (isGround) { return; }

        SortHigherPosColObjectResult();
        if (isSorted)
        {
            _myCollisionAreaData.MyTransform.position += Vector3.up *
                (_myCollisionObjects[0].CollisionObjectData.MyCollisionAreaData.TopYPos - _myCollisionAreaData.BottomYPos);
            isGround = true;
        }

    }

    private void SortHigherPosColObjectResult()
    {
        for (int i = 0; i < _myCollisionObjects.Count; i++)
        {
            if (_myCollisionObjects[i].IsCollisionBottom)
            {
                isSorted = true;
                if (i == 0) { continue; }

                if(_myCollisionObjects[0].CollisionObjectData.MyCollisionAreaData.TopYPos
                    <= _myCollisionObjects[i].CollisionObjectData.MyCollisionAreaData.TopYPos)
                {
                    //���X�g���g���܂킷���Ƃ��Ȃ������Ȃ�P���ɍ폜�������Ă�����
                    _myCollisionObjects.Add(_myCollisionObjects[0]);
                    _myCollisionObjects[0] = _myCollisionObjects[i];
                    _myCollisionObjects.RemoveAt(i);
                }
            }
        }

    }


    private void OnDrawGizmos()
    {
        if (isDebugColliderVisible)
        {
            Gizmos.DrawWireCube(transform.position + _myCollisionAreaData.Offset, _myCollisionAreaData.HalfAreaSize * 2);
        }
    }
}
